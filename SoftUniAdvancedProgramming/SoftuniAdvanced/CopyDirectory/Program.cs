namespace CopyDirectory
{
    using System;
    using System.IO;

    public class CopyDirectory
    {
        static void Main(string[] args)
        {
            string inputPath = Console.ReadLine();
            string outputPath = Console.ReadLine();

            CopyAllFiles(inputPath, outputPath);
        }

        public static void CopyAllFiles(string inputPath, string outputPath)
        {
            var dirInfo = new DirectoryInfo(inputPath);
            var files = dirInfo.GetFiles();

            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }

            Directory.CreateDirectory(outputPath);

            foreach (var file in files)
            {
                var outputDir = $"{outputPath}\\{file.Name}";
                using (var writer = new FileStream(outputDir, FileMode.Create, FileAccess.Write))
                using (var reader = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    var data = new byte[reader.Length];
                    reader.Read(data, 0, data.Length);
                    writer.Write(data);
                }
            }
        }
    }
}
