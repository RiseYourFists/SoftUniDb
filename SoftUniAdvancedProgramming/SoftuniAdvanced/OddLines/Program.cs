using System;
using System.IO;

namespace OddLines
{
    internal class OddLines
    {
        static void Main(string[] args)
        {
            var fileInput = @"../../../input.txt";
            var fileOutput = @"../../../output.txt";

            ExtractOddLines(fileInput, fileOutput);
        }

        private static void ExtractOddLines(string fileInput, string fileOutput)
        {
            using (var fileStream = new StreamReader(fileInput))
            {
                var text = fileStream.ReadLine();
                int line = 1;

                using (var writer = new StreamWriter(fileOutput))
                {
                    while (text != null)
                    {
                        if (line % 2 == 1)
                        {
                            writer.WriteLine($"{line}. {text}");
                        }
                        line++;

                        text = fileStream.ReadLine();
                    }
                }
            }
        }
    }
}
