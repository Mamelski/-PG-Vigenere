namespace VigenereGui.Vigenere.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using VigenereGui.Vigenere.VigenereUtils;

    /// <summary>
    ///     Class responsible for finding key lengh for text decrypted using Vigenere Cipher.
    /// </summary>
    public class KeyLengthFinder
    {
        /// <summary>
        /// The average Indexes of Coincidence. Index in array is key length.
        /// </summary>
        private readonly double[] AverageICs;

        /// <summary>
        /// The log.
        /// </summary>
        private readonly Logger log;

        /// <summary>
        /// Encrypted text.
        /// </summary>
        private readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyLengthFinder"/> class.
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        public KeyLengthFinder(Logger log, string text)
        {
            this.text = text;
            this.log = log;
            this.AverageICs = new double[Consts.MaxKeyLength];
        }

        /// <summary>
        /// Finds length of key.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int FindKeyLength()
        {
            for (var possibleKeyLength = 1; possibleKeyLength <= Consts.MaxKeyLength; ++possibleKeyLength)
            {
                this.CalculateAverageIC(possibleKeyLength);
            }

            // This method can find multiplicity of key
            //var minimalDiff = this.AverageICs.Min(IC => Math.Abs(IC - Consts.EnglishIC));
            //var keyLength = 0;
            //for (var i = 0; i < Consts.MaxKeyLength; i++)
            //{
            //    if (Math.Abs(this.AverageICs[i] - Consts.EnglishIC) == minimalDiff)
            //    {
            //        keyLength = i;
            //        break;
            //    }
            //}

            //// We were counting from 0
            //++keyLength;

            var keyLength = 0;
            for (int i = 0; i < Consts.MaxKeyLength; ++i)
            {
                if (this.AverageICs[i] > 0.06)
                {
                    keyLength = i+1;
                    break;
                }
            }

            this.LogResults(keyLength);

            return keyLength;
        }

        /// <summary>
        /// Calculates average Index of Coincidence for given possible key length.
        /// </summary>
        /// <param name="possibleKeyLength">
        /// The possible key length.
        /// </param>
        private void CalculateAverageIC(int possibleKeyLength)
        {
            var singleICsForGivenKey = new List<double>();

            for (var keyPart = 0; keyPart < possibleKeyLength; ++keyPart)
            {
                var partialText = new StringBuilder();
                for (var letterIndex = keyPart; letterIndex < this.text.Length; letterIndex += possibleKeyLength)
                {
                    partialText.Append(this.text[letterIndex]);
                }

                var partialIC = this.CalculateSingleIC(partialText.ToString());
                singleICsForGivenKey.Add(partialIC);
            }

            this.AverageICs[possibleKeyLength - 1] = singleICsForGivenKey.Average();
        }

        /// <summary>
        /// The calculates Index of Coincidence of given text.
        /// </summary>
        /// <param name="textPart">
        /// The text Part.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        private double CalculateSingleIC(string textPart)
        {
            var letterOccurences = new int[Consts.NumOfLetters];

            foreach (var letter in textPart)
            {
                ++letterOccurences[letter - Consts.FirstLetterAscii];
            }

            var nominator = 0;
            var denominator = textPart.Length * (textPart.Length - 1);

            for (var i = 0; i < Consts.NumOfLetters; ++i)
            {
                nominator += letterOccurences[i] * (letterOccurences[i] - 1);
            }

            var indexOfCoincidence = (double)nominator / denominator;

            return indexOfCoincidence;
        }

        /// <summary>
        /// Log results to Logger.
        /// </summary>
        /// <param name="keyLength">
        /// The key length.
        /// </param>
        private void LogResults(int keyLength)
        {
            var logMessage = new StringBuilder();
            logMessage.AppendLine("Obliczone indeksy koincydencji dla poszczególnych długości klucza:");
            for (var i = 0; i < Consts.MaxKeyLength; ++i)
            {
                logMessage.AppendLine($"długość klucza: {i + 1}         Średni IC: {this.AverageICs[i]}");
            }

            logMessage.AppendLine($"\nIndeks koincydencji dla tekstu w języku angielskim: {Consts.EnglishIC}");
            logMessage.AppendLine($"Przypuszczalna długość klucza: {keyLength}");

            this.log.AddMessage(logMessage.ToString());
        }
    }
}