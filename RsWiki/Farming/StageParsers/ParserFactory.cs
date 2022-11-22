namespace RsWiki.Farming.StageParsers
{
    internal class ParserFactory
    {
        public static DefaultParser CreateParser(string patchType, int stages, string crop)
        {
            switch (patchType)
            {
                case "default": return new DefaultParser(crop, stages);
                case "allotment": return new DefaultParser(crop, stages);
                case "bush": return new ProduceParser(crop, stages, 4);
                case "cactus": return new NonWaterParser(crop, stages);
                case "calquat": return new NonWaterParser(crop, stages);
                case "crystal tree": return new NonWaterParser(crop, stages);
                case "elder tree": return new NonWaterParser(crop, stages);
                case "evil turnip": return new DefaultParser(crop, stages);
                case "flower": return new FlowerParser(crop, stages);
                case "fruit tree": return new NonWaterParser(crop, stages);
                case "herb": return new NonWaterParser(crop, stages);
                case "hops": return new DefaultParser(crop, stages);
                case "jade vine": return new DefaultParser(crop, stages);
                case "kelda hops": return new DefaultParser(crop, stages);
                case "money tree": return new NonWaterParser(crop, stages);
                case "mushroom": return new NonWaterParser(crop, stages);
                case "nightshade": return new DefaultParser(crop, stages);
                case "potato": return new DefaultParser(crop, stages);
                case "spirit tree": return new NonWaterParser(crop, stages);
                case "tree": return new NonWaterParser(crop, stages);
                case "vine bush": return new DefaultParser(crop, stages);
                case "vine flower": return new DefaultParser(crop, stages);
                case "vine herb": return new NonWaterParser(crop, stages);
                default: return new DefaultParser(crop, stages);
            }
        }
    }
}
