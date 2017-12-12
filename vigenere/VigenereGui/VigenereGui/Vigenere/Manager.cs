namespace VigenereGui.Vigenere
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VigenereGui.Vigenere.Algorithms;
    using VigenereGui.Vigenere.VigenereUtils;

    /// <summary>
    /// The manager.
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// The file operations.
        /// </summary>
        private readonly FileOperations fileOperations = new FileOperations();

        /// <summary>
        /// The log.
        /// </summary>
        private readonly Logger log;

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        /// <param name="log">
        /// The log.
        /// </param>
        public Manager(Logger log)
        {
            this.log = log;
        }

        /// <summary>
        /// The encrypts text from file using given key.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="keyToEncrypt">
        /// The key as numbers.
        /// </param>
        public void Encrypt(string filePath, List<int> keyToEncrypt)
        {
            var logMessage = new StringBuilder();
            logMessage.AppendLine($"Wybrano plik do zaszyfrowania: \"{filePath}\"");
            var originalText = this.fileOperations.ReadFromFile(filePath);
            originalText = originalText.ToUpper();

            var keyIndex = 0;
            var encryptedText = new StringBuilder();

            foreach (var letter in originalText)
            {
                if (letter < Consts.FirstLetterAscii || letter > Consts.LastLetterAscii)
                {
                    encryptedText.Append(letter);
                    continue;
                }

                var baseCharAsInt = letter - Consts.FirstLetterAscii;
                var encyptedCharAsInt = (baseCharAsInt + keyToEncrypt[keyIndex]) % Consts.NumOfLetters;
                encryptedText.Append((char)(encyptedCharAsInt + Consts.FirstLetterAscii));
                keyIndex = (keyIndex + 1) % keyToEncrypt.Count;
            }

            var encryptedFilePath = Utils.PrepareFileName(filePath,FileSufix.Encryped);
            this.fileOperations.WriteToFile(encryptedText.ToString(), encryptedFilePath);

            logMessage.AppendLine($"Zapisano zaszyfrowany plik: \"{encryptedFilePath}\"");
            this.log.AddMessage(logMessage.ToString());
        }

        /// <summary>
        /// The decrypts text from file using given key.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="keyToDecrypt">
        /// The key as numbers.
        /// </param>
        /// <param name="sufix">
        /// The sufix.
        /// </param>
        public void Decrypt(string filePath, List<int> keyToDecrypt, FileSufix sufix)
        {
            var logMessage = new StringBuilder();
            logMessage.AppendLine($"Wybrano plik do odszyfrowania: \"{filePath}\"");
            var encryptedText = this.fileOperations.ReadFromFile(filePath);
            encryptedText = encryptedText.ToUpper();

            var keyIndex = 0;
            var decryptedText = new StringBuilder();

            foreach (var letter in encryptedText)
            {
                if (letter < Consts.FirstLetterAscii || letter > Consts.LastLetterAscii)
                {
                    decryptedText.Append(letter);
                    continue;
                }

                var baseCharAsInt = letter - Consts.FirstLetterAscii;
                var decryptedCharAsInt = (baseCharAsInt + Consts.NumOfLetters - keyToDecrypt[keyIndex]) % Consts.NumOfLetters;

                decryptedText.Append((char)(decryptedCharAsInt + Consts.FirstLetterAscii));
                keyIndex = (keyIndex + 1) % keyToDecrypt.Count;
            }

            var decryptedFilePath = Utils.PrepareFileName(filePath, sufix);
            this.fileOperations.WriteToFile(decryptedText.ToString(), decryptedFilePath);

            logMessage.AppendLine($"Zapisano odszyfrowany plik: \"{decryptedFilePath}\"");
            this.log.AddMessage(logMessage.ToString());
        }

        /// <summary>
        /// The find key length.
        /// </summary>
        /// <param name="filePath">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int FindKeyLength(string filePath)
        {
            this.log.AddMessage($"Szukam długości klucza w pliku: \"{filePath}\"");

            var encryptedText = this.fileOperations.ReadFromFile(filePath);

            var keyLengthFinder = new KeyLengthFinder(this.log, Utils.OnlyLetters(encryptedText));

            return keyLengthFinder.FindKeyLength();
        }

        /// <summary>
        /// The find key value.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="keyLength">
        /// The key length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string FindKeyValueAlg<T>(string filePath, int keyLength) where T : IAlgorithm, new()
        {
            var alg = new T();
            var text = fileOperations.ReadFromFile(filePath);
            var key = alg.FindKey(keyLength, Utils.OnlyLetters(text));
            this.log.AddMessage($"{alg.GetType().Name}: Znaleziono klucz. Wartość: {key}");
            return key;
        }
    }
}