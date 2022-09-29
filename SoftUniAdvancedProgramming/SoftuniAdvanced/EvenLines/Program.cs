namespace EvenLines
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class EvenLines
    {
        static void Main(string[] args)
        {
            string inputFilePath = @"..\..\..\text.txt";

            Console.WriteLine(ProcessLines(inputFilePath));
        }

        public static string ProcessLines(string inputFilePath)
        {
            var sb = new StringBuilder();

            using (var fileReader = new StreamReader(inputFilePath))
            {
                string line;
                var counter = 0;
                while ((line = fileReader.ReadLine()) != null)
                {
                    if (counter % 2 == 0)
                    {
                        line = ReplaceSymbols(line);
                        line = ReverseWords(line);
                        sb.AppendLine(line);
                    }
                    counter++;
                }
            }

            return sb.ToString();
        }
        private static string ReverseWords(string replacedSymbols)
        {
            var line = replacedSymbols.Split(' ').ToArray();
            Array.Reverse(line);
            return String.Join(' ', line);
        }

        private static string ReplaceSymbols(string line)
        {
            var tempString = line;

            var charArray = new string[] { "-", ",", ".", "!", "?" };

            foreach (var ch in charArray)
            {
                tempString = tempString.Replace(ch, "@");
            }

            return tempString;
        }
    }

}
