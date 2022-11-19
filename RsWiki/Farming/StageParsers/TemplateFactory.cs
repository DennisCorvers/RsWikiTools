using RsWiki.Extensions;
using System;
using System.Collections.Generic;

namespace RsWiki.Farming.StageParsers
{
    public class TemplateFactory
    {
        public string Crop { get; }

        public int Stages { get; }

        public string PatchType { get; }

        public GrowthStages SupportedStages { get; }

        private readonly DefaultParser m_parser;

        public TemplateFactory(string crop, int stages, string patchType, GrowthStages supportedStages)
        {
            Crop = crop.ToLower();
            Stages = stages;
            PatchType = patchType.ToLower();
            SupportedStages = supportedStages;

            m_parser = ParserFactory.CreateParser(PatchType, stages);
        }

        public ICollection<CropStageInfo> Build()
        {
            var stages = Enum.GetValues<GrowthStages>();
            var cropInfo = new List<CropStageInfo>();

            foreach (var state in stages)
            {
                for (int i = 1; i <= Stages; i++)
                {
                    if (SupportedStages.HasFlag(state))
                    {
                        var template = m_parser.GetCropInfo(state, i, Crop);
                        if (template != null)
                        {
                            template = template.CapitaliseFirstLetter();
                            cropInfo.Add(new CropStageInfo(state, i, template));
                        }
                    }
                }
            }

            return cropInfo;
        }
    }
}
