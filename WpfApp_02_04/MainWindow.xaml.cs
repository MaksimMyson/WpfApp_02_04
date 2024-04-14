using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApp_02_04
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Moderate_Click(object sender, RoutedEventArgs e)
        {
            string textFilePath = TextFilePathTextBox.Text.Trim();
            string moderationFilePath = ModerationFilePathTextBox.Text.Trim();

            if (string.IsNullOrEmpty(textFilePath) || string.IsNullOrEmpty(moderationFilePath))
            {
                MessageBox.Show("Будь ласка, введіть шлях до файлу з текстом та шлях до файлу зі словами для модерації.");
                return;
            }

            if (!File.Exists(textFilePath) || !File.Exists(moderationFilePath))
            {
                MessageBox.Show("Один з вказаних файлів не існує.");
                return;
            }

            string[] moderationWords = File.ReadAllLines(moderationFilePath);

            string text = File.ReadAllText(textFilePath);

            string moderatedText = ModerateText(text, moderationWords);

            ResultTextBox.Text = moderatedText;
        }

        private string ModerateText(string text, string[] moderationWords)
        {
            foreach (string word in moderationWords)
            {
                string pattern = $@"\b{Regex.Escape(word)}\b";
                text = Regex.Replace(text, pattern, new string('*', word.Length));
            }
            return text;
        }
    }
}
