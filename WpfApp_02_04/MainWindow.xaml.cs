using System.IO;
using System.Linq;
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

        // Перегляд вмісту файлу
        private void ViewFileButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                FileContentTextBox.Text = fileContent;
            }
            else
            {
                MessageBox.Show("Файл не існує!");
            }
        }

        // Збереження масиву у файл
        private void SaveArrayButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string arrayContent = ArrayTextBox.Text;

            // Збереження масиву у файл
            File.WriteAllText(filePath, arrayContent);
            MessageBox.Show("Масив збережено у файл!");
        }

        // Завантаження масиву із файлу
        private void LoadArrayButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            if (File.Exists(filePath))
            {
                string arrayContent = File.ReadAllText(filePath);
                ArrayTextBox.Text = arrayContent;
            }
            else
            {
                MessageBox.Show("Файл не існує!");
            }
        }

        // Пошук по файлу
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string searchText = SearchTextBox.Text;

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                int occurrences = Regex.Matches(fileContent, searchText).Count;

                MessageBox.Show($"Слово '{searchText}' знайдено {occurrences} разів у файлі.");
            }
            else
            {
                MessageBox.Show("Файл не існує!");
            }
        }

        // Статистична інформація про файл
        private void GetFileStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);

                int sentenceCount = fileContent.Split('.').Length;
                int uppercaseCount = fileContent.Count(char.IsUpper);
                int lowercaseCount = fileContent.Count(char.IsLower);
                int vowelCount = fileContent.Count(c => "aeiouAEIOUаеєиіїоуюяАЕЄИІЇОУЮЯ".Contains(c));
                int consonantCount = fileContent.Count(c => "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZбвгджзклмнпрстфхцчшщБВГДЖЗКЛМНПРСТФХЦЧШЩ".Contains(c));
                int digitCount = fileContent.Count(char.IsDigit);

                MessageBox.Show($"Кількість речень: {sentenceCount}\n" +
                                $"Кількість великих літер: {uppercaseCount}\n" +
                                $"Кількість маленьких літер: {lowercaseCount}\n" +
                                $"Кількість голосних літер: {vowelCount}\n" +
                                $"Кількість приголосних літер: {consonantCount}\n" +
                                $"Кількість цифр: {digitCount}");
            }
            else
            {
                MessageBox.Show("Файл не існує!");
            }
        }
    }
}