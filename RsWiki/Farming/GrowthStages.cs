using System;

namespace RsWiki.Farming
{
    [Flags]
    public enum GrowthStages
    {
        Healthy = 1,
        Watered = 2,
        Diseased = 4,
        Dead = 8,
        Grown = 16,
        Produce = 32
    }
}
