using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExtractSpecialBytes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputPath = @"../../../Files/example.png";
            var bytesTarget = @"../../../Files/bytes.txt";
            var output = @"../../../Files/output.bin";

            ExtractSpecialBytes(inputPath, bytesTarget, output);
        }

        private static void ExtractSpecialBytes(string inputPath, string bytesTarget, string outputStream)
        {
            using (var targetReader = new StreamReader(bytesTarget))
            {
                var targetInput = new List<string>();
                string input;
                while ((input = targetReader.ReadLine()) != null)
                {
                    targetInput.Add(input);
                }

                var bytes = targetInput.Select(int.Parse).ToArray();

                using (var inputReader = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
                {
                    inputReader.Seek(0, SeekOrigin.Begin);
                    var data = new byte[inputReader.Length];
                    inputReader.Read(data, 0, data.Length);

                    using (var output = new FileStream(outputStream, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        foreach (var item in data)
                        {
                            var @byte = int.Parse(item.ToString());

                            if (bytes.Contains(@byte))
                            {
                                output.WriteByte(item);
                            }
                        }

                    }

                }
            }
        }
    }
}
