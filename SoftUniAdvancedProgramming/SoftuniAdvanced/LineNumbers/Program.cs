namespace LineNumbers
{
    using System;
    using System.IO;

    public class LineNumbers
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\Files\input.txt";
            string outputFilePath = @"..\..\..\Files\output.txt";

            RewriteFileWithLineNumbers(inputFilePath, outputFilePath);
        }

        public static void RewriteFileWithLineNumbers(string inputFilePath, string outputFilePath)
        {
            using (var streamReader = new StreamReader(inputFilePath))
            {
                var line = streamReader.ReadLine();
                var counter = 1;

                using (var streamWriter = new StreamWriter(outputFilePath))
                {
                    while (line != null)
                    {
                        streamWriter.WriteLine($"{counter}. {line}");
                        counter++;
                        line = streamReader.ReadLine();
                    }
                }
            }
        }
    }

}
