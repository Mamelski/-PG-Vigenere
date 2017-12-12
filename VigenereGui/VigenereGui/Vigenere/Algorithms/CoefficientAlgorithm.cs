namespace VigenereGui.Vigenere.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using VigenereGui.Vigenere.VigenereUtils;

    public class CoefficientAlgorithm : IAlgorithm
    {
        protected const int LettersInAlphabet = 26;

        private int[] _key;

        public string FindKey(int keyLength, string text)
        {
            _key = new int[keyLength];
            for (int i = 0; i < _key.Length; i++)
                _key[i] = GetKeyShift(i, text);

            var sb = new StringBuilder();
            for (int i = 0; i < _key.Length; i++)
                sb.Append((char)(_key[i] + 'A'));

            return sb.ToString();
        }

        private int GetKeyShift(int shift, string text)
        {
            var coefficients = new List<double>();
            string subtext = this.GetSubtext(text, shift, _key.Length);
            var freq = CountLetterFrequency(subtext);
            for (int i = 0; i < LettersInAlphabet; i++)
                coefficients.Add(CountCoefficient(freq, i));

            return coefficients.IndexOf(coefficients.Max());
        }

        private Dictionary<char, double> CountLetterFrequency(string text)
        {
            Dictionary<char, double> result = new Dictionary<char, double>();
            int textLength = text.Length;
            for (int i = 0; i < LettersInAlphabet; i++)
            {
                char letter = (char)('A' + i);
                int occurences = text.Count(x => x == letter);
                result.Add(letter, (double)occurences / (double)textLength);
            }

            return result;
        }

        private double CountCoefficient(Dictionary<char, double> freq, int shift)
        {
            double result = 0.0;
            for (int i = 0; i < LettersInAlphabet; i++)
            {
                char freqLetter = (char)('A' + i);
                char alphabetLetter = (char)('A' + i - shift);
                if (alphabetLetter < 'A')
                    alphabetLetter += (char)LettersInAlphabet;

                result += freq[freqLetter] * Consts.LetterFrequencyInLanguage[alphabetLetter];
            }

            return result;
        }

        private string GetSubtext(string text, int start = 0, int step = 1)
        {
            var sb = new StringBuilder();
            for (int i = start; i < text.Length; i += step)
                sb.Append(text[i]);

            return sb.ToString();
        }
    }
}