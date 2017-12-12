using System;
using System.Collections.Generic;
using System.Text;

namespace Vigenere.Core
{
    public class CipherKey
    {
        private List<muint> _letterShift;

        public muint this [int n]
        {
            get
            {
                return this._letterShift[n];
            }

            set
            {
                this._letterShift[n] = value;
            }
        }

        public int Length
        {
            get
            {
                return this._letterShift.Count;
            }

            set
            {
                if(this._letterShift == null)
                    this._letterShift = new List<muint>();

                this._letterShift.Clear();
                for (int i = 0; i < value; i++)
                    this._letterShift.Add(0);
            }
        }

        public CipherKey(List<int> shift)
        {
            this._letterShift = new List<muint>();
            for (int i = 0; i < shift.Count; i++)
                this._letterShift.Add(shift[i]);                
        }

        public CipherKey(params int[] shift)
        {
            this._letterShift = new List<muint>();
            for (int i = 0; i < shift.Length; i++)
                this._letterShift.Add(shift[i]);                
        }

        public CipherKey(int length = 0)
        {
            this.Length = length;
        }

        public string Encrypt(string plainText)
        {
            return this.ProcessText(plainText, false);
        }

        public string Decrypt(string cipherText)
        {
            return this.ProcessText(cipherText, true);
        }

        private string ProcessText(string inputText, bool subtract)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < inputText.Length; i++)
            {
                int j = i % this._letterShift.Count;
                muint character = inputText[i] - 'A';
                character = subtract ? character - this._letterShift[j] : character + this._letterShift[j];
                char newCharacter = Convert.ToChar((int)(character) + 'A');
                sb.Append(newCharacter);
            }

            return sb.ToString();
        }
    }
}

