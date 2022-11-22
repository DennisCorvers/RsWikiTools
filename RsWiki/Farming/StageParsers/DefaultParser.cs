using System;

namespace RsWiki.Farming.StageParsers
{
    internal class DefaultParser
    {
        protected int Stages { get; }

        protected string Crop { get; }

        public DefaultParser(string crop, int stages)
        {
            Stages = stages;
            Crop = crop;
        }

        public string GetCropInfo(GrowthStages state, int stage)
        {
            var template = GetTemplate(state, stage);
            if (template == null)
                return null;

            return string.Format(template, Crop, stage);
        }

        /// <summary>
        /// Mutates a general growth state to a specific one.
        /// Ie. the highest stage of "healthy" to "grown"
        /// </summary>
        public virtual GrowthStages MutateState(GrowthStages state, int stage)
        {
            switch (state)
            {
                case GrowthStages.Healthy:
                    if (stage == Stages)
                        return GrowthStages.Grown;
                    break;
            }

            return state;
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
            return state switch
            {
                GrowthStages.Healthy => "{0} (stage {1})",
                GrowthStages.Watered => "{0} (watered, stage {1})",
                GrowthStages.Diseased => "Diseased {0} (stage {1})",
                GrowthStages.Dead => "Dead {0} (stage {1})",
                GrowthStages.Grown => "{0} (grown)",
                _ => null,
            };
        }
    }
}
