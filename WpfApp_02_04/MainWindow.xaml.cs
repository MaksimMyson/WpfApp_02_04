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

        private void DisplayFileInfo_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text.Trim();
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Будь ласка, введіть шлях до файлу.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не знайдено.");
                return;
            }

            string fileContent = File.ReadAllText(filePath);
            int sentenceCount = Regex.Matches(fileContent, @"[.!?]").Count;
            int uppercaseCount = Regex.Matches(fileContent, @"[A-Z]").Count;
            int lowercaseCount = Regex.Matches(fileContent, @"[a-z]").Count;
            int vowelCount = Regex.Matches(fileContent, @"[aeiouAEIOU]").Count;
            int consonantCount = Regex.Matches(fileContent, @"[bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ]").Count;
            int digitCount = Regex.Matches(fileContent, @"[0-9]").Count;

            ResultTextBox.Text = $"Кількість речень: {sentenceCount}\n" +
                                 $"Кількість великих літер: {uppercaseCount}\n" +
                                 $"Кількість маленьких літер: {lowercaseCount}\n" +
                                 $"Кількість голосних літер: {vowelCount}\n" +
                                 $"Кількість приголосних літер: {consonantCount}\n" +
                                 $"Кількість цифр: {digitCount}";
        }
    }
}
