using RsWiki.Extensions;
using RsWiki.Farming.StageParsers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RsWiki
{
    public class Program
    {
        async static Task Main(string[] args)
        {
            // Logic to select module
            // Skipped for now
            IWikiModule module = new Templates.GrowthStagesNew();
            var moduleLoader = new ModuleLoader(module);

            await moduleLoader.Run();

            Console.ReadLine();
        }


    }
}
