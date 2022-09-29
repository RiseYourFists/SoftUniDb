using System.IO;

namespace CopyBinaryFile
{
    public class CopyBinaryFile
    {
        static void Main(string[] args)
        {
            string inputPath = @"..\..\..\copyMe.png";
            string outputPath = @"..\..\..\copyMe-copy.png";

            CopyFile(inputPath, outputPath);
        }

        public static void CopyFile(string inputFilePath, string outputFilePath)
        {
            using (var fileToCopy = new FileStream(inputFilePath, FileMode.Open))
            using (var output = new FileStream(outputFilePath, FileMode.Create))
            {
                var data = new byte[fileToCopy.Length];
                fileToCopy.Read(data, 0, data.Length);
                output.Write(data, 0, data.Length);
            }
        }
    }
}
