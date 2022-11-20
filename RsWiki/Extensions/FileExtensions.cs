using System;
using System.IO;

namespace RsWiki.Extensions
{
    public static class FileExtensions
    {
        public static string CreateOutputFilePath(string inputFile)
        {
            var fileName = Path.GetFileNameWithoutExtension(inputFile);
            var date = DateTime.Now.ToString("yyyyMMddTHHmmss");
            return $"{Path.GetDirectoryName(inputFile)}\\{fileName}-output {date}.txt";
        }

        public static Stream OpenOrCreate(string filePath)
        {
            if (!File.Exists(filePath))
                return File.Create(filePath);
            else
                return File.OpenRead(filePath);
        }

        public static DirectoryInfo CreateOrGetDir(string folderName)
        {
            var herbDir = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            return Directory.CreateDirectory(herbDir);
        }
    }
}
