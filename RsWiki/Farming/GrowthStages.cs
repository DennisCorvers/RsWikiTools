using System;

namespace RsWiki.Farming
{
    [Flags]
    public enum GrowthStages
    {
        None = 0,
        Seed = 1,
        Healthy = 2,
        Watered = 4,
        Diseased = 8,
        Dead = 16,
        Grown = 32
    }
}
