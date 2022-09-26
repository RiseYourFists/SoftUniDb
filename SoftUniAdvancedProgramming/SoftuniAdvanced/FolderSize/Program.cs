namespace FolderSize
{
    using System;
    using System.IO;
    public class FolderSize
    {
        static void Main(string[] args)
        {
            string folderPath = @"..\..\..\Files\TestFolder";
            string outputPath = @"..\..\..\Files\output.txt";

            GetFolderSize(folderPath, outputPath);
        }

        public static void GetFolderSize(string folderPath, string outputFilePath)
        {
            var size = GetFolderSize(folderPath);
            decimal sizeInKB = (decimal)size / 1024;

            using(var output = new StreamWriter(outputFilePath))
            {
                output.Write($"{sizeInKB} KB");
            }
        }

        private static long GetFolderSize(string folderPath)
        {
            var bytesSize = 0L;

            var dirInfo = new DirectoryInfo(folderPath);
            var files = dirInfo.GetFiles();

            var subDirs = Directory.GetDirectories(folderPath);
            foreach (var subDir in subDirs)
            {
                bytesSize += GetFolderSize(subDir);
            }

            foreach (var file in files)
            {
                bytesSize += file.Length;
            }
            return bytesSize;
        }
    }
}
