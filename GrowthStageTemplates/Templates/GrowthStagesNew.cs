using RsWiki.Farming;
using System;
using System.Text;

namespace GrowthStageTemplates.Templates
{
    public class GrowthStagesNew : ITemplate
    {
        private const string Header = @"{{Growth stages new";
        private const string Footer = @"}}<noinclude>[[Category:Farming]]</noinclude>";
        private const string Cell = @"|{0}{1} = {2}.{3}";

        string ITemplate.Process(CropFileInfo cropFileInfo)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Header);

            foreach (var crop in cropFileInfo.CropStages)
            {
                // Adds, for example:
                // |stage3 = Herbs (stage 3).png
                sb.AppendLine(string.Format(Cell,
                    MapStage(crop.GrowthStage),
                    crop.StageNo,
                    crop.CropInfo,
                    cropFileInfo.ImageFormat));
            }

            sb.AppendLine(Footer);

            return sb.ToString();
        }

        private static string MapStage(GrowthStages stage)
        {
            return stage switch
            {
                GrowthStages.Healthy => "stage",
                GrowthStages.Watered => "watered",
                GrowthStages.Diseased => "diseased",
                GrowthStages.Dead => "dead",
                _ => throw new ArgumentOutOfRangeException($"No mapping available for stage: {stage}."),
            };
        }
    }
}
