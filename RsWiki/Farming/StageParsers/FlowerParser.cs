namespace RsWiki.Farming.StageParsers
{
    internal class FlowerParser : DefaultParser
    {
        public FlowerParser(ParserConfig config) 
            : base(config)
        {
        }

        protected override bool StageExists(GrowthStages state, int stage)
        {
            if (state == GrowthStages.Watered && stage == Stages)
                return false;

            if (state == GrowthStages.Diseased && (stage == 1 || stage == Stages))
                return false;

            if (state == GrowthStages.Dead && stage == 1)
                return false;

            return true;
        }
    }
}
