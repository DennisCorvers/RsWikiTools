namespace RsWiki.Farming.StageParsers
{
    internal class ParserFactory
    {
        public static DefaultParser CreateParser(string patchType, ParserConfig config)
        {
            switch (patchType)
            {
                case "default": return new DefaultParser(config);
                case "allotment": return new DefaultParser(config);
                case "bush": return new ProduceParser(config);
                case "cactus": return new NonWaterParser(config);
                case "calquat": return new NonWaterParser(config);
                case "crystal tree": return new NonWaterParser(config);
                case "elder tree": return new NonWaterParser(config);
                case "evil turnip": return new DefaultParser(config);
                case "flower": return new FlowerParser(config);
                case "fruit tree": return new ProduceParser(config);
                case "herb": return new NonWaterParser(config);
                case "hops": return new DefaultParser(config);
                case "jade vine": return new DefaultParser(config);
                case "kelda hops": return new DefaultParser(config);
                case "money tree": return new NonWaterParser(config);
                case "mushroom": return new NonWaterParser(config);
                case "nightshade": return new DefaultParser(config);
                case "potato": return new DefaultParser(config);
                case "spirit tree": return new NonWaterParser(config);
                case "tree": return new NonWaterParser(config);
                case "vine bush": return new DefaultParser(config);
                case "vine flower": return new DefaultParser(config);
                case "vine herb": return new NonWaterParser(config);
                default: return new DefaultParser(config);
            }
        }
    }
}
