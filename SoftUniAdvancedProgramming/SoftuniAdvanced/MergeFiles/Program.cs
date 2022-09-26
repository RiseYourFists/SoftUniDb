namespace MergeFiles
{
    using System;
    using System.IO;
    public class MergeFiles
    {
        static void Main(string[] args)
        {
            var firstInputFilePath = @"..\..\..\Files\input1.txt";
            var secondInputFilePath = @"..\..\..\Files\input2.txt";
            var outputFilePath = @"..\..\..\Files\output.txt";

            MergeTextFiles(firstInputFilePath, secondInputFilePath, outputFilePath);
        }

        public static void MergeTextFiles(string firstInputFilePath, string secondInputFilePath, string outputFilePath)
        {
            using(var file1Reader = new StreamReader(firstInputFilePath))
            {
                using(var file2Reader = new StreamReader(secondInputFilePath))
                {
                    using( var outputFile = new StreamWriter(outputFilePath))
                    {
                        while (true)
                        {
                            var line1 = file1Reader.ReadLine();
                            var line2 = file2Reader.ReadLine();

                            if (line2 == null && line1 == null) break;

                            outputFile.WriteLine(line1);
                            outputFile.WriteLine(line2);
                        }
                    }
                }
            }
        }
    }
}
