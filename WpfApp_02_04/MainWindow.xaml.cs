using System;
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

        private void SearchWord_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Будь ласка, введіть слово для пошуку.");
                return;
            }

            string filePath = "path_to_your_file.txt"; // Замініть на шлях до вашого файлу
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не знайдено.");
                return;
            }

            string fileContent = File.ReadAllText(filePath);
            int wordCount = Regex.Matches(fileContent, searchText, RegexOptions.IgnoreCase).Count;
            int reverseWordCount = Regex.Matches(fileContent, ReverseString(searchText), RegexOptions.IgnoreCase).Count;

            ResultTextBox.Text = $"Кількість входжень слова \"{searchText}\": {wordCount}\n" +
                                 $"Кількість входжень слова \"{ReverseString(searchText)}\" у зворотному напрямку: {reverseWordCount}";
        }

        private string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
