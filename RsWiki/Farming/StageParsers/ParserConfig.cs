namespace RsWiki.Farming.StageParsers
{
    public class ParserConfig
    {
        public ParserConfig(string crop, int stages)
        {
            Crop = crop.ToLower();
            Stages = stages;
        }

        public string Crop { get; }

        public int Stages { get; }
    }

    public class ProduceParserConfig : ParserConfig
    {
        public ProduceParserConfig(string crop, int stages, int produce) 
            : base(crop, stages)
        {
            Produce = produce;
        }

        public int Produce { get; }
    }
}
