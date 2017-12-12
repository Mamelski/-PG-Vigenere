using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vigenere.Core
{
    public class CrackingAlgorithm : AbstractCrackingAlgorithm
    {
        public CrackingAlgorithm(string input, int numberOfKey)
            : base(input, numberOfKey)
        {
        }

        public void Crack()
        {
            for (int i = 0; i < this.Key.Length; i++)
                this.Key[i] = this.GetKeyShift(i);

            this.OutputText = this.Key.Decrypt(this.InputText);
        }

        private int GetKeyShift(int shift)
        {
            var coefficients = new List<double>();
            string subtext = this.GetSubtext(shift, this.Key.Length);
            var freq = this.CountLetterFrequency(subtext);
            for(int i = 0; i < LettersInAlphabet; i++)
                coefficients.Add(this.CountCoefficient(freq, i));

            return coefficients.IndexOf(coefficients.Max());
        }

        private string GetSubtext(int start = 0, int step = 1)
        {
            var sb = new StringBuilder();
            for (int i = start; i < this.InputText.Length; i += step)
                sb.Append(this.InputText[i]);

            return sb.ToString();
        }

        private Dictionary<char, double> CountLetterFrequency(string text)
        {
            Dictionary<char, double> result = new Dictionary<char, double>();
            int textLength = text.Length;
            for(int i = 0; i < LettersInAlphabet; i++)
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
            for(int i = 0; i < LettersInAlphabet; i++)
            {
                char freqLetter = (char)('A' + i);
                char alphabetLetter = (char)('A' + i - shift);
                if (alphabetLetter < 'A')
                    alphabetLetter += (char)LettersInAlphabet;
                
                result += freq[freqLetter] * LetterFrequencyInLanguage[alphabetLetter];
            }

            return result;
        }
    }
}

