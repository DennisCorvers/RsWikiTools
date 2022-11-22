using System;

namespace RsWiki.Farming.StageParsers
{
    internal class ProduceParser : DefaultParser
    {
        private readonly int m_produceStages;
        private readonly int m_emptyStage;

        /// <summary>
        /// Creates a new parser that includes crop produce stages.
        /// </summary>
        /// <param name="crop">The name of the crop</param>
        /// <param name="stages">The total amount of stages.</param>
        /// <param name="produceStages">The total amount of "produce" stages.</param>
        public ProduceParser(string crop, int stages, int produceStages)
            : base(crop, stages)
        {
            m_produceStages = produceStages;
            // Empty stage is the stage before the first produce stage
            m_emptyStage = stages - produceStages - 1;
        }

        public override GrowthStages MutateState(GrowthStages state, int stage)
        {
            // Converts healthy crop that fall within the "produce" state to said state.
            return state == GrowthStages.Healthy && stage >= (Stages - m_produceStages) && stage != Stages
                ? GrowthStages.Produce
                : base.MutateState(state, stage);
        }

        protected override string GetConcreteTemplate(GrowthStages state, int stage)
        {
            if (state == GrowthStages.Produce)
            {
                return $"{{0}} ({stage - m_emptyStage} produce)";
            }

            // Special case for empty crops.
            else if (stage == m_emptyStage)
            {
                return state switch
                {
                    GrowthStages.Healthy => "{0} (empty)",
                    GrowthStages.Watered => "{0} (watered, empty)",
                    GrowthStages.Diseased => "Diseased {0} (empty)",
                    GrowthStages.Dead => "Dead {0} (empty)",
                    _ => throw new InvalidOperationException("Invalid growthstate/stage combination"),
                };
            }

            return base.GetConcreteTemplate(state, stage);
        }

        protected override bool StageExists(GrowthStages state, int stage)
        {
            // Grown crops cannot have a WATERED state
            if (state == GrowthStages.Watered && (stage == Stages || stage + m_produceStages >= Stages))
                return false;

            // Seedlings or produce-stage crops cannot be diseased 
            if (state == GrowthStages.Diseased && (stage == 1 || stage + m_produceStages >= Stages))
                return false;

            // Seedlings or produce-stage crops cannot be dead
            if (state == GrowthStages.Dead && (stage == 1 || stage + m_produceStages >= Stages))
                return false;

            return true;
        }
    }
}
