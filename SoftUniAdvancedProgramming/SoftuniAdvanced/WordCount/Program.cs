namespace WordCount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    public class WordCount
    {
        static void Main(string[] args)
        {
            string wordPath = @"..\..\..\Files\words.txt";
            string textPath = @"..\..\..\Files\text.txt";
            string outputPath = @"..\..\..\Files\output.txt";

            CalculateWordCounts(wordPath, textPath, outputPath);
        }

        public static void CalculateWordCounts(string wordsFilePath, string textFilePath, string outputFilePath)
        {
            var words = new Dictionary<string, int>();

            using (var readFilter = new StreamReader(wordsFilePath))
            {
                var input = readFilter.ReadToEnd().Split().Select(x => x.ToLower());

                foreach (var item in input)
                {
                    words.Add(item, 0);
                }
            }

            using (var textReader = new StreamReader(textFilePath))
            {
                while (true)
                {
                    var line = textReader.ReadLine();

                    if (line == null) break;

                    var text = line.Split(new char[] {'-', ' ', '…', ',', '?', '!', '.' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower());

                    foreach (var item in text)
                    {
                        if (words.ContainsKey(item))
                        {
                            words[item]++;
                        }
                    }
                }
            }

            var result = words.OrderByDescending(x => x.Value);

            using(var writer = new StreamWriter(outputFilePath))
            {
                foreach (var item in result)
                {
                    writer.WriteLine($"{item.Key} - {item.Value}");
                }
            }
        }
    }
}
