namespace VigenereGui.Vigenere
{
    using System.Windows.Controls;

    /// <summary>
    /// The logger.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// The log text block.
        /// </summary>
        private readonly TextBlock logTextBlock;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logTextBox">
        /// The log text box.
        /// </param>
        public Logger(TextBlock logTextBox)
        {
            this.logTextBlock = logTextBox;
        }

        /// <summary>
        /// The add message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void AddMessage(string message)
        {
            this.logTextBlock.Text += message + "\n\n";
        }

        /// <summary>
        /// The add error message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void AddErrorMessage(string message)
        {
            this.logTextBlock.Text += "[Error]" + message + "\n\n";
        }
    }
}
