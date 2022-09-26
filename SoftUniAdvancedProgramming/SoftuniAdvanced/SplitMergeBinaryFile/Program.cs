namespace SplitMergeBinaryFile
{
    using System;
    using System.IO;
    using System.Linq;

    public class SplitMergeBinaryFile
    {
        static void Main(string[] args)
        {
            string sourceFilePath = @"..\..\..\Files\example.png";
            string joinedFilePath = @"..\..\..\Files\example-joined.png";
            string partOnePath = @"..\..\..\Files\part-1.bin";
            string partTwoPath = @"..\..\..\Files\part-2.bin";

            SplitBinaryFile(sourceFilePath, partOnePath, partTwoPath);
            MergeBinaryFiles(partOnePath, partTwoPath, joinedFilePath);
        }

        public static void SplitBinaryFile(string sourceFilePath, string partOneFilePath, string partTwoFilePath)
        {
            using (var sourceFile = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
            using (var filePartOne = new FileStream(partOneFilePath, FileMode.Create))
            using (var filePartTwo = new FileStream(partTwoFilePath, FileMode.Create))
            {
                var size1 = (sourceFile.Length % 2 == 0) ? sourceFile.Length / 2 : (sourceFile.Length / 2) + 1;
                var size2 = sourceFile.Length / 2;

                var buffer1 = new byte[size1];
                var buffer2 = new byte[size2];

                sourceFile.Read(buffer1);
                sourceFile.Read(buffer2);

                filePartOne.Write(buffer1);
                filePartTwo.Write(buffer2);
            }
        }

        public static void MergeBinaryFiles(string partOneFilePath, string partTwoFilePath, string joinedFilePath)
        {
            using (var joinedFile = new FileStream(joinedFilePath, FileMode.Create))
            using (var filePartOne = new FileStream(partOneFilePath, FileMode.Open))
            using (var filePartTwo = new FileStream(partTwoFilePath, FileMode.Open))
            {
                var buffer1 = new byte[filePartOne.Length];
                filePartOne.Read(buffer1);

                var buffer2 = new byte[filePartTwo.Length];
                filePartTwo.Read(buffer2);

                joinedFile.Write(buffer1);
                joinedFile.Write(buffer2);
            }
        }
    }
}