namespace RsWiki.Farming.StageParsers
{
    internal class NonWaterParser : DefaultParser
    {
        public NonWaterParser(string crop, int stages) 
            : base(crop, stages)
        {
        }

        protected override bool StageExists(GrowthStages state, int stage)
        {
            if (state == GrowthStages.Watered)
                return false;

            return base.StageExists(state, stage);
        }
    }
}
