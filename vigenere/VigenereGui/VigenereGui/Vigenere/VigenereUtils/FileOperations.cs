namespace VigenereGui.Vigenere.VigenereUtils
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// The file operations.
    /// Responsible for all operations with files.
    /// </summary>
    public class FileOperations
    {  
        /// <summary>
        /// The read from file.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ReadFromFile(string filePath)
        {
            var allText = new StringBuilder();
            using (var streamReader = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    allText.AppendLine(line);
                }
            }

            return allText.ToString();
        }

        /// <summary>
        /// The write to file.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        public void WriteToFile(string text, string filePath)
        {
            using (var streamWriter = new StreamWriter(File.Create(filePath)))
            {
                streamWriter.WriteLine(text);
            }
        }
    }
}