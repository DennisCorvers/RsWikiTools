using RsWiki.Extensions;
using System;
using System.Collections.Generic;

namespace RsWiki.Farming.StageParsers
{
    public class CropInfoBuilder
    {
        public string Crop { get; }

        public int Stages { get; }

        public string PatchType { get; }

        public GrowthStages SupportedStages { get; }

        private readonly DefaultParser m_parser;

        public CropInfoBuilder(string crop, int stages, string patchType, GrowthStages supportedStages)
        {
            Crop = crop.CapitaliseFirstLetter();
            Stages = stages;
            PatchType = patchType.ToLower();
            SupportedStages = supportedStages;

            m_parser = ParserFactory.CreateParser(PatchType, stages);
        }

        public IReadOnlyCollection<CropStageInfo> Build()
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
                            cropInfo.Add(new CropStageInfo(state, i, template));
                        }
                    }
                }
            }

            return cropInfo;
        }
    }
}
