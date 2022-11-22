using GrowthStageTemplates.Templates;
using RsWiki.Extensions;
using RsWiki.Farming;
using RsWiki.Farming.StageParsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GrowthStageTemplates
{
    class Program
    {
        private const string InputFile = "GrowthStageTemplate.txt";
        private const string ImageFormat = "png";
        private static ICollection<ITemplate> m_templates = new List<ITemplate>
        {
            new GrowthStagesNew()
        };

        async static Task Main(string[] args)
        {
            try
            {
                using (var inputFile = FileExtensions.OpenOrCreate(InputFile))
                using (var sr = new StreamReader(inputFile))
                {
                    while (!sr.EndOfStream)
                    {
                        var cropInfo = ProcessLine(await sr.ReadLineAsync());
                        if (cropInfo == null)
                            continue;

                        var outputDir = FileExtensions.CreateOrGetDir(cropInfo.CropName);

                        Console.WriteLine("Renaming files...");
                        new CropFileFormatter(outputDir, ImageFormat).Renamefiles(cropInfo);

                        foreach (var template in m_templates)
                        {
                            var path = Path.Combine(outputDir.FullName, template.GetType().Name + ".txt");
                            File.WriteAllText(path, template.Process(cropInfo));

                            Console.WriteLine($"Wrote template data to file {path}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in parsing data: {e.Message}");
            }

            Console.WriteLine("Finished processing crop data.");
            Console.ReadLine();
        }

        private static CropFileInfo ProcessLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            var prms = line.Split(',');
            if (prms.Length < 3)
                throw new ArgumentException("Input requires at least a crop name (string), an amount of stages (number) and a patch type (string)");

            var cropName = prms[0].Trim();
            var stages = int.Parse(prms[1]);
            var patch = prms[2].Trim().ToLower();
            var produce = prms.Length > 3 ? int.Parse(prms[3]) : -1;

            // Default growth stages.
            var supportedStages =
                GrowthStages.Healthy |
                GrowthStages.Watered |
                GrowthStages.Diseased |
                GrowthStages.Dead;

            var config = prms.Length > 3
                ? new ProduceParserConfig(cropName, stages, produce)
                : new ParserConfig(cropName, stages);

            var cropInfoBuilder = new CropInfoBuilder(config, patch, supportedStages);
            return new CropFileInfo(ImageFormat, cropName, cropInfoBuilder.Build());
        }
    }
}
