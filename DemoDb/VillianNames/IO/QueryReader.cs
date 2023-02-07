using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillianNames.IO.Interfaces;

namespace VillianNames.IO
{
    public class QueryReader : IReader
    {

        public QueryReader(string fileName)
        {
            FileName = fileName;
            string currPath = Directory.GetCurrentDirectory();
            FilePath = Path.Combine(currPath, $"{currPath}..\\..\\..\\..\\Queries\\");
            FullPath = Path.Combine(FilePath, $"{FileName}.sql");
        }

        public QueryReader(string fileName, string filePath) 
            : this(fileName)
        {
            FilePath = filePath;
        }

        public string FileName { get; }
        public string FilePath { get; }

        public string FullPath { get; }

        public bool GetOrCreatePath()
        {
            var isPathExist = Directory.Exists(FilePath);
            if (!isPathExist)
            {
                Directory.CreateDirectory(FilePath);
                return false;
            }

            return true;
        }

        public bool GetOrCreateFile()
        {
            if (!File.Exists(FullPath))
            {
                using var streamWriter = new StreamWriter(FullPath);
                streamWriter.Close();
                return false;
            }
            return true;
        }
        public string ReadToEnd()
        {
            using var streamReader = new StreamReader(FullPath);
            var result = streamReader.ReadToEnd();
            return result;
        }
    }
}
