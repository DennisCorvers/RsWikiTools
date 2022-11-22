using RsWiki.Farming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GrowthStageTemplates
{
    internal class CropFileFormatter
    {
        private readonly DirectoryInfo m_directory;
        private readonly string m_outputFileFormat;

        public CropFileFormatter(DirectoryInfo directory, string outputFileFormat)
        {
            m_directory = directory;
            m_outputFileFormat = outputFileFormat;
        }

        public void Renamefiles(CropFileInfo cropFileInfo)
        {
            var files = m_directory.EnumerateFiles($"*.{m_outputFileFormat}", SearchOption.TopDirectoryOnly)
                .OrderBy(x => x.CreationTime)
                .GetEnumerator();

            ICollection<CropStageInfo> orderedCrops = new List<CropStageInfo>();
            cropFileInfo.CropStages
                .AppendCollection(orderedCrops, GrowthStages.Healthy)
                .AppendCollection(orderedCrops, GrowthStages.Produce)
                .AppendCollection(orderedCrops, GrowthStages.Grown)
                .AppendCollection(orderedCrops, GrowthStages.Watered)
                .AppendCollection(orderedCrops, GrowthStages.Diseased)
                .AppendCollection(orderedCrops, GrowthStages.Dead);

            var crops = orderedCrops.GetEnumerator();

            while (files.MoveNext() && crops.MoveNext())
            {
                var currFile = files.Current;
                var targetName = $"{crops.Current.CropInfo}.{m_outputFileFormat}";
                var targetDir = Path.Combine(currFile.Directory.FullName, targetName);

                if (File.Exists(targetDir))
                    throw new InvalidOperationException($"Unable to rename file {currFile.Name}. A file with target name {targetName} already exists.");

                currFile.MoveTo(targetDir);
            }
        }
    }
}
