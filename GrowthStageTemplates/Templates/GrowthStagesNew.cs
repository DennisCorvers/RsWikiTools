using RsWiki.Farming;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var builder = new TemplateBuilder(cropFileInfo);

            builder
                .AddCrop(GrowthStages.Healthy)
                .AddCompoundCrop(GrowthStages.Grown)
                .AddCrop(GrowthStages.Watered)
                .AddCrop(GrowthStages.Diseased)
                .AddCrop(GrowthStages.Dead);

            return builder.Build();
        }

        private static string MapState(GrowthStages state)
        {
            return state switch
            {
                GrowthStages.Grown => "stage",
                GrowthStages.Healthy => "stage",
                GrowthStages.Watered => "watered",
                GrowthStages.Diseased => "diseased",
                GrowthStages.Dead => "dead",
                _ => throw new ArgumentOutOfRangeException($"No mapping available for stage: {state}."),
            };
        }

        private class TemplateBuilder
        {
            private int m_lastStage;
            private StringBuilder m_sb;
            private CropFileInfo m_cropFileInfo;

            public TemplateBuilder(CropFileInfo cropFileInfo)
            {
                m_sb = new StringBuilder();
                m_sb.AppendLine(Header);
                m_cropFileInfo = cropFileInfo;
            }

            public TemplateBuilder AddCrop(GrowthStages growthStage)
            {
                m_lastStage = 0;

                foreach (var crop in GetCropStages(growthStage))
                {
                    AppendLine(crop, crop.StageNo);
                    m_lastStage = Math.Max(m_lastStage, crop.StageNo);
                }

                return this;
            }

            public TemplateBuilder AddCompoundCrop(GrowthStages growthStage)
            {
                foreach (var crop in GetCropStages(growthStage))
                    AppendLine(crop, ++m_lastStage);

                return this;
            }

            public string Build()
            {
                m_sb.AppendLine(Footer);
                return m_sb.ToString();
            }

            private IEnumerable<CropStageInfo> GetCropStages(GrowthStages growthStage)
            {
                return m_cropFileInfo.CropStages.GetCropStages(growthStage)
                    .OrderBy(x => x.StageNo);
            }

            private void AppendLine(CropStageInfo crop, int cropStage)
            {
                m_sb.AppendLine(string.Format(Cell,
                    MapState(crop.GrowthStage),
                    cropStage,
                    crop.CropInfo,
                    m_cropFileInfo.ImageFormat));
            }
        }
    }
}
