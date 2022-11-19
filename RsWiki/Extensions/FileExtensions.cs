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
    }
}
