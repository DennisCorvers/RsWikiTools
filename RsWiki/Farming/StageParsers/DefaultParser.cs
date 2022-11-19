using System;

namespace RsWiki.Farming.StageParsers
{
    internal class DefaultParser
    {
        protected int Stages { get; }

        public DefaultParser(int stages)
        {
            Stages = stages;
        }

        public string GetCropInfo(GrowthStages state, int stage, string crop)
        {
            var template = GetTemplate(state, stage);
            if (template == null)
                return null;

            return string.Format(template, crop, stage);
        }

        private string GetTemplate(GrowthStages state, int stage)
        {
            if (!StageExists(state, stage))
                return null;

            return GetConcreteTemplate(state, stage);
        }

        protected virtual bool StageExists(GrowthStages state, int stage)
        {
            // Grown crops cannot have a WATERED state
            if (state == GrowthStages.Watered && stage == Stages)
                return false;

            // Seedlings or grown crops cannot have a DISEASED state
            if (state == GrowthStages.Diseased && (stage == 1 || stage == Stages))
                return false;

            // Seelings cannot have a DEAD state
            if (state == GrowthStages.Dead && (stage == 1 || stage == Stages))
                return false;

            return true;
        }

        protected virtual string GetConcreteTemplate(GrowthStages state, int stage)
        {
            switch (state)
            {
                case GrowthStages.Healthy:
                    return stage == Stages
                        ? "{0} (grown)"
                        : "{0} (stage {1})";
                case GrowthStages.Watered:
                    return "{0} (watered, stage {1})";
                case GrowthStages.Diseased:
                    return "Diseased {0} (stage {1})";
                case GrowthStages.Dead:
                    return "Dead {0} (stage {1})";
                case GrowthStages.Grown:
                    return "{0} (grown)";
                default:
                    break;
            }

            return null;
        }
    }
}
