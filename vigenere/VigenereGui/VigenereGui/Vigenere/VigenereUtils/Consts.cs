namespace VigenereGui.Vigenere.VigenereUtils
{
    using System.Collections.Generic;

    /// <summary>
    /// The consts.
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// Index of Coincidence in English.
        /// </summary>
        public static double EnglishIC = 0.0667;

        /// <summary>
        /// The max key length, makes work easier with shorter texts.
        /// </summary>
        public static int MaxKeyLength { get; } = 30;

        /// <summary>
        /// Number of letters in english alphabet.
        /// </summary>
        public static int NumOfLetters { get; } = 26;

        /// <summary>
        /// ASCII code of 'A' letter.
        /// </summary>
        public static int FirstLetterAscii { get; } = 65;

        /// <summary>
        /// ASCII code of 'Z' letter.
        /// </summary>
        public static int LastLetterAscii { get; } = 90;

        /// <summary>
        /// Index of letter 'E' in english alphabet counting from 0.
        /// </summary>
        public static int LetterEIndex { get; } = 4;

        /// <summary>
        /// Frequency of letters in english alphabet.
        /// </summary>
        public static Dictionary<char, double> LetterFrequencyInLanguage { get; } = 
          new Dictionary<char, double>
      {
            {'A', 0.082},
            {'B', 0.015},
            {'C', 0.028},
            {'D', 0.043},
            {'E', 0.127},
            {'F', 0.022},
            {'G', 0.020},
            {'H', 0.061},
            {'I', 0.070},
            {'J', 0.002},
            {'K', 0.008},
            {'L', 0.040},
            {'M', 0.024},
            {'N', 0.067},
            {'O', 0.075},
            {'P', 0.019},
            {'Q', 0.001},
            {'R', 0.060},
            {'S', 0.063},
            {'T', 0.091},
            {'U', 0.028},
            {'V', 0.010},
            {'W', 0.023},
            {'X', 0.001},
            {'Y', 0.020},
            {'Z', 0.001}
      };
    }
}