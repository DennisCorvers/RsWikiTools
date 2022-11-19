namespace RsWiki.Farming.StageParsers
{
    internal class ParserFactory
    {
        public static DefaultParser CreateParser(string patchType, int stages)
        {
            switch (patchType)
            {
                case "default": return new DefaultParser(stages);
                case "allotment": return new DefaultParser(stages);
                case "bush": return new NonWaterParser(stages);
                case "cactus": return new NonWaterParser(stages);
                case "calquat": return new NonWaterParser(stages);
                case "crystal tree": return new NonWaterParser(stages);
                case "elder tree": return new NonWaterParser(stages);
                case "evil turnip": return new DefaultParser(stages);
                case "flower": return new FlowerParser(stages);
                case "fruit tree": return new NonWaterParser(stages);
                case "herb": return new NonWaterParser(stages);
                case "hops": return new DefaultParser(stages);
                case "jade vine": return new DefaultParser(stages);
                case "kelda hops": return new DefaultParser(stages);
                case "money tree": return new NonWaterParser(stages);
                case "mushroom": return new NonWaterParser(stages);
                case "nightshade": return new DefaultParser(stages);
                case "potato": return new DefaultParser(stages);
                case "spirit tree": return new NonWaterParser(stages);
                case "tree": return new NonWaterParser(stages);
                case "vine bush": return new DefaultParser(stages);
                case "vine flower": return new DefaultParser(stages);
                case "vine herb": return new NonWaterParser(stages);
                default: return new DefaultParser(stages);
            }
        }
    }
}
