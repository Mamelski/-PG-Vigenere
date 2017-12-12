using System;
using System.Collections.Generic;
using System.IO;
using Vigenere.Core;

namespace Vigenere.CLI
{
    public class Parser
    {
        private const string EncryptCommand = "szyfruj";
        private const string DecryptCommand = "deszyfruj";
        private const string ConvertCommand = "konwertuj";

        private string[] _parameters;
        private CipherKey _underlyingKey;

        public string Command
        {
            get
            {
                return this._parameters[0];
            }
        }

        public string FileName
        {
            get
            {
                return this._parameters[1];
            }
        }

        public int NumberOfKey
        {
            get
            {
                return Int32.Parse(this._parameters[2]);
            }
        }

        public CipherKey Key
        {
            get
            {
                if (this._underlyingKey == null)
                {
                    this._underlyingKey = new CipherKey(this._parameters.Length - 2);
                    for (int i = 2; i < this._parameters.Length; i++)
                    {
                        int result = 0;
                        if (!Int32.TryParse(this._parameters[i], out result))
                            throw new ArgumentException("Nieprawidłowa wartość klucza: " + this._parameters[i]);
                        
                        this._underlyingKey[i - 2] = result;
                    }
                }

                return this._underlyingKey;
            }
        }

        public string InputText { get; private set; }

        public string OutputText { get; private set; }

        public Parser(string[] parameters)
        {
            if (parameters.Length < 3)
                throw new ArgumentException("Potrzeba co najmniej trzech parametrów: polecenie nazwa_pliku przesunięcie");
            
            this._parameters = parameters;
        }

        public void Parse()
        {
            switch (this.Command)
            {
                case EncryptCommand:
                    this.Encrypt();
                    break;
                case DecryptCommand:
                    this.Decrypt();
                    break;
                case ConvertCommand:
                    this.Convert();
                    break;
                default:
                    throw new ArgumentException("Nieznane polecenie: " + Command);
            }
        }

        private void Encrypt()
        {
            this.InputText = FileReader.ReadFromFile(this.FileName);
            this.OutputText = this.Key.Encrypt(this.InputText);
        }

        private void Decrypt()
        {
            this.InputText = FileReader.ReadFromFile(this.FileName);
            //this.OutputText = this.Key.Decrypt(this.InputText);
            var crackAlgorithm = new CrackingAlgorithm(this.InputText, this.NumberOfKey);
            crackAlgorithm.Crack();
            this.OutputText = crackAlgorithm.OutputText;
        }

        private void Convert()
        {
            this.InputText = FileReader.ReadFromFile(this.FileName);
            this.OutputText = this.InputText;
        }
    }
}

