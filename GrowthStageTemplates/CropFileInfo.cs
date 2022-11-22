using RsWiki.Farming;
using System.Collections.Generic;

namespace GrowthStageTemplates
{
    internal class CropFileInfo
    {
        public string ImageFormat { get; }

        public string CropName { get; }

        public CropStageCollection CropStages { get; }

        public CropFileInfo(string imageFormat, string cropName, CropStageCollection cropStages)
        {
            ImageFormat = imageFormat;
            CropName = cropName;
            CropStages = cropStages;
        }
    }
}
