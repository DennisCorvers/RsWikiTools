using RsWiki.Farming;
using RsWiki.Farming.StageParsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsWiki.Templates
{
    public class GrowthStagesNew : IWikiModule
    {
        private const string Header = @"{{Growth stages new";
        private const string Footer = @"}}<noinclude>[[Category:Farming]]</noinclude>";
        private const string Cell = @"|{0}{1} = {2}.{3}";
        private const string ImageFormat = "png";

        public IEnumerable<string> GetInfo() 
        {
            return new string[2]
            {
                "Enter data in the following format: <Crop name>, <Stage count>, <Patch type>",
                "For example: Marigold, 5, flower"
            };
        }

        public string Process(string inputLine)
        {
            if (string.IsNullOrWhiteSpace(inputLine))
                return null;

            var prms = inputLine.Split(',');
            if (prms.Length < 2)
                throw new ArgumentException("Input requires at least a crop name (string) and an amount of stages (number)");

            var cropName = prms[0].Trim();
            var stages = int.Parse(prms[1]);
            var patch = prms.Length > 2 ? prms[2].Trim().ToLower() : string.Empty;

            // Default growth stages.
            var supportedStages = GrowthStages.Healthy | GrowthStages.Watered | GrowthStages.Diseased | GrowthStages.Dead;

            var templateFactory = new TemplateFactory(cropName, stages, patch, supportedStages);

            return Create(templateFactory.Build());
        }

        private string Create(ICollection<CropStageInfo> crops)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Header);

            foreach (var crop in crops)
            {
                // Adds, for example:
                // |stage3 = Herbs (stage 3).png
                sb.AppendLine(string.Format(Cell,
                    MapStage(crop.GrowthStage),
                    crop.StageNo,
                    crop.CropInfo,
                    ImageFormat));
            }

            sb.AppendLine(Footer);

            return sb.ToString();
        }

        private string MapStage(GrowthStages stage)
        {
            return stage switch
            {
                GrowthStages.Healthy => "stage",
                GrowthStages.Watered => "watered",
                GrowthStages.Diseased => "diseased",
                GrowthStages.Dead => "dead",
                _ => throw new System.ArgumentOutOfRangeException($"No mapping available for stage: {stage}."),
            };
        }
    }
}
