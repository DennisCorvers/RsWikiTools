namespace RsWiki.Farming.StageParsers
{
    internal class NonWaterProduceParser : ProduceParser
    {
        public NonWaterProduceParser(ParserConfig config) 
            : base(config)
        {
        }

        protected override bool StageExists(GrowthStages state, int stage) 
            => state == GrowthStages.Watered ? false : base.StageExists(state, stage);
    }
}
