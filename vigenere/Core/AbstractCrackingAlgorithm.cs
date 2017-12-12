using System.Collections.Generic;

namespace Vigenere.Core
{
    public class AbstractCrackingAlgorithm
    {
        protected const int LettersInAlphabet = 26;
        protected const double CrossCorellationCoefficient = 0.065;
        protected const double DeltaAcceptance = 0.005;

        protected readonly Dictionary<char, double> LetterFrequencyInLanguage =
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

        public string InputText { get; protected set; }

        public string OutputText { get; protected set; }

        public CipherKey Key { get; private set; }

        public AbstractCrackingAlgorithm(string input, int numberOfKey)
        {
            this.InputText = input;
            this.Key = new CipherKey(numberOfKey);
        }
    }
}

