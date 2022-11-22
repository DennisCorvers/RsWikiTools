using System;
using System.Collections.Generic;
using System.Linq;

namespace RsWiki.Farming
{
    public class CropStageCollection
    {
        private Dictionary<GrowthStages, List<CropStageInfo>> m_cropStages;

        public CropStageCollection(ICollection<CropStageInfo> cropStages)
        {
            m_cropStages = new Dictionary<GrowthStages, List<CropStageInfo>>();
            foreach (var cropInfo in cropStages)
            {
                AddOrCreate(cropInfo);
            }
        }

        private void AddOrCreate(CropStageInfo cropStage)
        {
            if (m_cropStages.TryGetValue(cropStage.GrowthStage, out var collection))
            {
                collection.Add(cropStage);
            }
            else
            {
                m_cropStages.Add(cropStage.GrowthStage, new List<CropStageInfo>() { cropStage });
            }
        }

        public IReadOnlyCollection<CropStageInfo> GetCropStages(GrowthStages growthStage)
        {
            if (m_cropStages.TryGetValue(growthStage, out var collection))
            {
                return collection;
            }

            return Array.Empty<CropStageInfo>();
        }

        public List<CropStageInfo> ToCollection()
        {
            var ret = new List<CropStageInfo>();
            foreach (var cropStages in m_cropStages.Values)
                ret.AddRange(cropStages);

            return ret;
        }

        public CropStageCollection AppendCollection(ICollection<CropStageInfo> cropStages, GrowthStages growthStage)
        {
            foreach(var stages in GetCropStages(growthStage))
                cropStages.Add(stages);

            return this;
        }
    }
}
