using System;
using System.IO;
using System.Windows;

namespace WpfApp_02_04
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ReverseContent_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text.Trim();

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Будь ласка, введіть шлях до файлу.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не існує.");
                return;
            }

            string reversedContent = ReverseFileContent(filePath);

            string newFilePath = $"{Path.GetDirectoryName(filePath)}\\Reversed_{Path.GetFileName(filePath)}";

            try
            {
                File.WriteAllText(newFilePath, reversedContent);
                MessageBox.Show($"Вміст файлу успішно перевернуто і збережено у файлі:\n{newFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка при збереженні файлу: {ex.Message}");
            }
        }

        private string ReverseFileContent(string filePath)
        {
            string content = File.ReadAllText(filePath);
            char[] charArray = content.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
