using RsWiki.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RsWiki.Farming.StageParsers
{
    public class CropInfoBuilder
    {
        public int Stages { get; }

        public string PatchType { get; }

        public GrowthStages SupportedStages { get; }

        private readonly DefaultParser m_parser;

        public CropInfoBuilder(ParserConfig config, string patchType, GrowthStages supportedStages)
        {
            Stages = config.Stages;
            PatchType = patchType.ToLower();
            SupportedStages = supportedStages;

            m_parser = ParserFactory.CreateParser(PatchType, config);
        }

        public CropStageCollection Build()
        {
            var stages = Enum.GetValues<GrowthStages>();
            var cropInfo = new List<CropStageInfo>();

            foreach (var state in stages)
            {
                for (int i = 1; i <= Stages; i++)
                {
                    if (SupportedStages.HasFlag(state))
                    {
                        var mutatedState = m_parser.MutateState(state, i);
                        var template = m_parser.GetCropInfo(mutatedState, i);

                        if (template != null)
                        {
                            cropInfo.Add(new CropStageInfo(mutatedState, i, template.CapitaliseFirstLetter()));
                        }
                    }
                }
            }

            return new CropStageCollection(cropInfo);
        }
    }
}
