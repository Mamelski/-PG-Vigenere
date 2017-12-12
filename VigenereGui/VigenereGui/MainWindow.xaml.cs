namespace VigenereGui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows;

    using Microsoft.Win32;

    using VigenereGui.Vigenere;
    using VigenereGui.Vigenere.VigenereUtils;

    using Vigenere.Algorithms;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        ///     The encryptor.
        /// </summary>
        private Manager encryptor;

        /// <summary>
        ///     The log.
        /// </summary>
        private Logger log;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.Prepare();
            // TODO poprawa GUI
            // TODO testy
        }

        /// <summary>
        /// The prepare.
        /// </summary>
        private void Prepare()
        {
            this.log = new Logger(this.logTextBlock);
            this.encryptor = new Manager(this.log);
            this.label.Content = $"Klucz (do {Consts.MaxKeyLength} liter):";
        }

        /// <summary>
        /// Encrypt file.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var givenKey = this.givenKeyTextBox.Text;
                if (!IsKeyValid(givenKey))
                {
                    this.log.AddMessage($"Klucz musi mieć od 1 do {Consts.MaxKeyLength} znaków (tylko litery)");
                    return;
                }

                var keyAsNumbers = this.ConvertStringKeyToNumbers(givenKey);

                var fileToEncrypt = this.GetFilePath();


                this.encryptor.Encrypt(fileToEncrypt, keyAsNumbers);
            }
            catch (Exception ex)
            {
                this.log.AddErrorMessage($"Wystąpił błąd {ex.Message}");
            }
        }

        /// <summary>
        /// Decrypt file using alg1.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DecryptButtonAlg1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var keyToDecrypt = this.KeyValueAlg1TextBox.Text;
                var keyAsNumbers = this.ConvertStringKeyToNumbers(keyToDecrypt);

                var fileToEncrypt = this.GetFilePath();
                this.encryptor.Decrypt(fileToEncrypt, keyAsNumbers, FileSufix.DecryptedAlg1);
            }
            catch (Exception ex)
            {
                this.log.AddErrorMessage($"Wystąpił błąd {ex.Message}");
            }
        }

        /// <summary>
        /// Decrypt file using alg2.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DecryptButtonAlg2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var keyToDecrypt = this.KeyValueAlg2TextBox.Text;
                var keyAsNumbers = this.ConvertStringKeyToNumbers(keyToDecrypt);

                var fileToEncrypt = this.GetFilePath();
                this.encryptor.Decrypt(fileToEncrypt, keyAsNumbers, FileSufix.DecryptedAlg2);
            }
            catch (Exception ex)
            {
                this.log.AddErrorMessage($"Wystąpił błąd {ex.Message}");
            }
        }

        /// <summary>
        /// Calculate key length.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CalculateKeyLengthButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filePath = this.GetFilePath();
                this.keyLengthTextBox.Text = this.encryptor.FindKeyLength(filePath).ToString();
            }
            catch (Exception ex)
            {
                this.log.AddErrorMessage($"Wystąpił błąd {ex.Message}");
            }
        }

        /// <summary>
        /// Calculate key value using alg1.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CalculateKeyValueAlg1Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var keyLengthString = this.keyLengthTextBox.Text;
                var keyLength = int.Parse(keyLengthString);

                var filePath = this.GetFilePath();

                var keyValue = this.encryptor.FindKeyValueAlg<MostFrequentLetterAlgorithm>(filePath, keyLength);
                this.KeyValueAlg1TextBox.Text = $"{keyValue}";
            }
            catch (FormatException)
            {
                this.log.AddErrorMessage("Nie znam długości klucza.");
            }
            catch (Exception ex)
            {
               this.log.AddErrorMessage($"Wystąpił błąd {ex.Message}"); 
            }
        }

        /// <summary>
        /// Calculate key value using alg2.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CalculateKeyValueAlg2Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var keyLengthString = this.keyLengthTextBox.Text;
                var keyLength = int.Parse(keyLengthString);

                var filePath = this.GetFilePath();

                var keyValue = this.encryptor.FindKeyValueAlg<CoefficientAlgorithm>(filePath, keyLength);
                this.KeyValueAlg2TextBox.Text = $"{keyValue}";
            }
            catch (FormatException)
            {
                this.log.AddErrorMessage("Nie znam długości klucza.");
            }
            catch (Exception ex)
            {
                this.log.AddErrorMessage($"Wystąpił błąd {ex.Message}");
            }
        }

        /// <summary>
        /// Opens File Dialog and return chosen file path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetFilePath()
        {
            var choseFile = new OpenFileDialog
                                {
                                    DefaultExt = ".txt", 
                                    Multiselect = false, 
                                    InitialDirectory =
                                        AppDomain.CurrentDomain.BaseDirectory + "TextFiles\\"
                                };

            var result = choseFile.ShowDialog();

            if (result == true)
            {
                var fileName = choseFile.FileName;
                return fileName;
            }

            throw new Exception("Nie udało się uzyskać ścieżki do wybranego pliku");
        }

        /// <summary>
        /// The is key valid.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsKeyValid(string key)
        {
            if (key.Length > Consts.MaxKeyLength || key.Length == 0)
            {
                return false;
            }

            return Regex.IsMatch(key, @"^[a-zA-Z]+$");
        }

        /// <summary>
        /// The convert string key to numbers.
        /// </summary>
        /// <param name="givenKey">
        /// The given key.
        /// </param>
        /// <returns>
        /// The <see cref="List{T}"/>.
        /// </returns>
        private List<int> ConvertStringKeyToNumbers(string givenKey)
        {
            givenKey = givenKey.ToUpper();

            var keyAsNumbers = givenKey.Select(keyPart => keyPart - Consts.FirstLetterAscii).ToList();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Podany klucz to {givenKey}");
            stringBuilder.Append("W formie liczbowej: ");

            foreach (var keyElement in keyAsNumbers)
            {
                stringBuilder.Append($"{keyElement},");
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            this.log.AddMessage(stringBuilder.ToString());

            return keyAsNumbers;
        }
    }
}