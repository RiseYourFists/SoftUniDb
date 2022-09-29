namespace DirectoryTraversal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class DirectoryTraversal
    {
        static void Main(string[] args)
        {
            string path = Console.ReadLine();
            string reportFileName = @"../../../report.txt";

            string reportContent = TraverseDirectory(path);
            Console.WriteLine(reportContent);

            WriteReportToDesktop(reportContent, reportFileName);
        }

        public static string TraverseDirectory(string inputFolderPath)
        {
            var sortedData = new Dictionary<string, Dictionary<string, long>>();
            var dir = new DirectoryInfo(inputFolderPath);
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                var extention = file.Extension;
                if(!sortedData.ContainsKey(extention))
                {
                    sortedData.Add(extention, new Dictionary<string, long>());
                }
                sortedData[extention].Add(file.Name, file.Length);
            }

            var sb = FormatText(sortedData);
            return sb.ToString();
        }

        private static StringBuilder FormatText(Dictionary<string, Dictionary<string, long>> sortedData)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var extention in sortedData)
            {
                sb.AppendLine(extention.Key);
                foreach (var file in extention.Value)
                {
                    var kb = (decimal)file.Value / 1024;
                    sb.AppendLine($"--{file.Key} - {kb:f3}kb");
                }
            }
            return sb;
        }

        public static void WriteReportToDesktop(string textContent, string reportFileName)
        {
            using(var output = new StreamWriter(reportFileName))
            {
                output.Write(textContent);
            }
        }

    }
}
