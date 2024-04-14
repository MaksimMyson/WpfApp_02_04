using System;
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

        private void AnalyzeFile_Click(object sender, RoutedEventArgs e)
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

            int positiveCount = 0;
            int negativeCount = 0;
            int twoDigitCount = 0;
            int fiveDigitCount = 0;

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                int number;
                if (int.TryParse(line, out number))
                {
                    if (number > 0)
                    {
                        positiveCount++;
                        SaveNumberToFile("PositiveNumbers.txt", number);
                    }
                    else if (number < 0)
                    {
                        negativeCount++;
                        SaveNumberToFile("NegativeNumbers.txt", number);
                    }

                    if (line.Length == 2)
                    {
                        twoDigitCount++;
                        SaveNumberToFile("TwoDigitNumbers.txt", number);
                    }
                    else if (line.Length == 5)
                    {
                        fiveDigitCount++;
                        SaveNumberToFile("FiveDigitNumbers.txt", number);
                    }
                }
            }

            ResultTextBox.Text = $"Кількість додатних чисел: {positiveCount}\n" +
                                  $"Кількість від'ємних чисел: {negativeCount}\n" +
                                  $"Кількість двозначних чисел: {twoDigitCount}\n" +
                                  $"Кількість п'ятизначних чисел: {fiveDigitCount}";
        }

        private void SaveNumberToFile(string fileName, int number)
        {
            string filePath = $"{Environment.CurrentDirectory}\\{fileName}";
            File.AppendAllText(filePath, $"{number}\n");
        }
    }
}
