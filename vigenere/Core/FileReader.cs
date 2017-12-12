using System;
using System.IO;
using System.Text;

namespace Vigenere.Core
{
    public class FileReader
    {
        public static string ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Nie ma takiego pliku: " + fileName);

            var sb = new StringBuilder();
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        char character = Convert.ToChar(sr.Read());
                        if (Char.IsLetter(character))
                            sb.Append(Char.ToUpper(character));
                    }
                }
            }
                
            return sb.ToString();
        }
    }
}

