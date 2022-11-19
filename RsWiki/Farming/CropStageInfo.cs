using System.Diagnostics;

namespace RsWiki.Farming
{
    [DebuggerDisplay("{CropInfo}")]
    public struct CropStageInfo
    {
        public GrowthStages GrowthStage { get; }

        public int StageNo { get; }

        public string CropInfo { get; }

        public CropStageInfo(GrowthStages growthStage, int stageNo, string cropInfo)
        {
            GrowthStage = growthStage;
            StageNo = stageNo;
            CropInfo = cropInfo;
        }
    }
}
