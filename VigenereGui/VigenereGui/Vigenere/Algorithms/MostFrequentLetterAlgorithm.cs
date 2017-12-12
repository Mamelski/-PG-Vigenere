namespace VigenereGui.Vigenere.Algorithms
{
    using System.Linq;
    using System.Text;

    using VigenereGui.Vigenere.VigenereUtils;

    /// <summary>
    /// The most frequent letter algorithm.
    /// </summary>
    public class MostFrequentLetterAlgorithm : IAlgorithm
    {
        /// <summary>
        /// The finds which was used to encrypt given text based on fact that 'E' is most frequent letter in english alphabet.
        /// </summary>
        /// <param name="keyLength">
        /// The key length.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string FindKey(int keyLength, string text)
        {
            var keyValues = new char[keyLength];
            for (var keyPart = 0; keyPart < keyLength; ++keyPart)
            {
                var partialText = new StringBuilder();
                for (var letterIndex = keyPart; letterIndex < text.Length; letterIndex += keyLength)
                {
                    partialText.Append(text[letterIndex]);
                }

                keyValues[keyPart] = this.FindKeyPartValue(partialText.ToString());
            }

            var keyValuesString = new string(keyValues);
            return $"{keyValuesString}";
        }

        /// <summary>
        /// The find value of key part for partial text that was encrypted with this key.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="char"/>.
        /// </returns>
        private char FindKeyPartValue(string text)
        {
            var letterOccurences = new int[Consts.NumOfLetters];

            foreach (var letter in text)
            {
                ++letterOccurences[letter - Consts.FirstLetterAscii];
            }

            // We assume that this is letter 'E', because it is most frequent letter in english
            var mostFrequentLetterIndex = letterOccurences.ToList().IndexOf(letterOccurences.Max());

            //return (char)((Consts.NumOfLetters + mostFrequentLetterIndex - Consts.LetterEIndex) % Consts.NumOfLetters);

            if (mostFrequentLetterIndex == Consts.LetterEIndex)
            {
                return 'A';
            }

            if (mostFrequentLetterIndex < Consts.LetterEIndex)
            {
                return (char)(Consts.NumOfLetters - (Consts.LetterEIndex - mostFrequentLetterIndex) + Consts.FirstLetterAscii);
            }

            return (char)((mostFrequentLetterIndex - Consts.LetterEIndex) + Consts.FirstLetterAscii);
        }
    }
}