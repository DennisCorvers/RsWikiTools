using System;

namespace RsWiki.Farming.StageParsers
{
    internal class ProduceParser : DefaultParser
    {
        protected int ProduceStages { get; }
        protected int EmptyStage { get; }

        /// <summary>
        /// Creates a new parser that includes crop produce stages.
        /// </summary>
        /// <param name="crop">The name of the crop</param>
        /// <param name="stages">The total amount of stages.</param>
        /// <param name="produceStages">The total amount of "produce" stages.</param>
        public ProduceParser(ParserConfig config)
            : base(config)
        {
            var produceConfig = config as ProduceParserConfig ?? throw new InvalidOperationException("Invalid config");

            ProduceStages = produceConfig.Produce;
            // Empty stage is the stage before the first produce stage
            EmptyStage = config.Stages - produceConfig.Produce - 1;
        }

        public override GrowthStages MutateState(GrowthStages state, int stage)
        {
            // Converts healthy crop that fall within the "produce" state to said state.
            return state == GrowthStages.Healthy && stage >= (Stages - ProduceStages) && stage != Stages
                ? GrowthStages.Produce
                : base.MutateState(state, stage);
        }

        protected override string GetConcreteTemplate(GrowthStages state, int stage)
        {
            if (state == GrowthStages.Produce)
            {
                return $"{{0}} ({stage - EmptyStage} produce)";
            }

            // Special case for empty crops.
            else if (stage == EmptyStage)
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
            if (state == GrowthStages.Watered && (stage == Stages || stage + ProduceStages >= Stages))
                return false;

            // Seedlings or produce-stage crops cannot be diseased 
            if (state == GrowthStages.Diseased && (stage == 1 || stage + ProduceStages >= Stages))
                return false;

            // Seedlings or produce-stage crops cannot be dead
            if (state == GrowthStages.Dead && (stage == 1 || stage + ProduceStages >= Stages))
                return false;

            return true;
        }
    }
}
