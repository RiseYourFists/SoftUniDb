namespace LineNumbers
{
    using System;
    using System.IO;
    using System.Linq;

    public class LineNumbers
    {
        static void Main(string[] args)
        {
            string inputPath = @"..\..\..\text.txt";
            string outputPath = @"..\..\..\output.txt";

            ProcessLines(inputPath, outputPath);
        }

        public static void ProcessLines(string inputFilePath, string outputFilePath)
        {
            using (var reader = new StreamReader(inputFilePath))
            using (var writer = new StreamWriter(outputFilePath))
            {
                string line;
                var counter = 1;
                while((line = reader.ReadLine()) != null)
                {
                    var letters = GetLetterCount(line);
                    var punctuation = GetPunctuationCount(line);
                    writer.WriteLine($"Line {counter}: {line} ({letters})({punctuation})");
                    counter++;
                }
            }
        }

        private static int GetPunctuationCount(string line)
        {
            var result = 0;
            var charArray = line.ToCharArray();
            var punctuation = new char[] { '-', ',', '.', '!', '?', '\'' };
            foreach (var ch in charArray)
            {
                if (punctuation.Contains(ch))
                {
                    result++;
                }
            }
            return result;
        }

        private static object GetLetterCount(string line)
        {
            
            var result = 0;
            var charArray = line.ToCharArray();
            foreach (var ch in charArray)
            {
                if (char.IsLetter(ch))
                {
                    result++;
                }
            }
            return result;
        }
    }
}
