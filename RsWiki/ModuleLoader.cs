using RsWiki.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsWiki
{
    public class ModuleLoader
    {
        private readonly IWikiModule m_module;

        public ModuleLoader(IWikiModule module)
        {
            m_module = module;
        }

        public async Task Run()
        {
            PreRun();

            Console.WriteLine("Enter input file location:");
            var inputPath = Console.ReadLine();
            var outputPath = FileExtensions.CreateOutputFilePath(inputPath);

            FileStream inFile = null;
            FileStream outFile = null;

            try
            {
                inFile = File.OpenRead(inputPath);
                outFile = File.OpenWrite(outputPath);

                using (var sr = new StreamReader(inFile))
                using (var sw = new StreamWriter(outFile))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = await sr.ReadLineAsync();
                        var handlerOutput = m_module.Process(line);

                        Console.WriteLine("Writing output to file.");
                        await sw.WriteLineAsync(handlerOutput);
                        await sw.WriteLineAsync();
                    }
                }

                Console.WriteLine($"Finished writing to output file: {outputPath}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in parsing data: {e.Message}");
            }
            finally
            {
                inFile?.Dispose();
                outFile?.Dispose();
            }
        }

        private void PreRun()
        {
            foreach (var str in m_module.GetInfo())
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }
    }
}
