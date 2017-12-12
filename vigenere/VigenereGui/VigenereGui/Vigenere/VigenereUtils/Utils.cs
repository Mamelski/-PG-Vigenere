namespace VigenereGui.Vigenere.VigenereUtils
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The text utils.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// The only letters.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string OnlyLetters(string text)
        {
            return new string(text.Where(char.IsLetter).ToArray());
        }

        /// <summary>
        /// The prepare encrypted file path.
        /// </summary>
        /// <param name="baseFile">
        /// The base file.
        /// </param>
        /// <param name="sufix">
        /// The sufix.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string PrepareFileName(string baseFile, FileSufix sufix)
        {
            string sufixString;

            switch (sufix)
            {
                case FileSufix.DecryptedAlg1:
                    sufixString = "_Decrypted_MostFrequentLetterAlgorithm";
                    break;
                case FileSufix.DecryptedAlg2:
                    sufixString = "_Decrypted_CoefficientAlgorithm";
                    break;
                case FileSufix.Encryped:
                    sufixString = "_encrypted";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sufix), sufix, null);
            }

            var directoryName = Path.GetDirectoryName(baseFile);
            var fileName = Path.GetFileNameWithoutExtension(baseFile);
            var newFilePath = directoryName + "\\" + fileName + sufixString + ".txt";

            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }

            return newFilePath;
        }

        /// <summary>
        /// Converts string to string with letters positions in english alphabet.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string StringToAsciiNumbersString(string text)
        {
            var stringBuilder = new StringBuilder();

            foreach (var letter in text)
            {
                stringBuilder.Append(letter - Consts.FirstLetterAscii);
                stringBuilder.Append(',');
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }
    }
}