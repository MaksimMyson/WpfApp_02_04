using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_02_04
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateNumbersAndSaveToFile("numbers.txt", 100);
            MessageBox.Show("Numbers generated and saved to file.");

            DisplayStatistics("numbers.txt");

            SeparateNumbersAndSaveToFile("numbers.txt", "primes.txt", "fibonacci.txt");
            MessageBox.Show("Numbers separated and saved to files.");
        }

        private void GenerateNumbersAndSaveToFile(string filename, int count)
        {
            Random random = new Random();
            using (StreamWriter writer = new StreamWriter(filename))
            {
                for (int i = 0; i < count; i++)
                {
                    int number = random.Next(-99999, 99999);
                    writer.WriteLine(number);
                }
            }
        }

        private void DisplayStatistics(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int[] numbers = lines.Select(int.Parse).ToArray();

            int positiveCount = numbers.Count(n => n > 0);
            int negativeCount = numbers.Count(n => n < 0);
            int twoDigitCount = numbers.Count(n => n >= 10 && n <= 99);
            int fiveDigitCount = numbers.Count(n => n >= 10000 && n <= 99999);

            string statistics = $"Positive: {positiveCount}\n" +
                                $"Negative: {negativeCount}\n" +
                                $"Two Digit: {twoDigitCount}\n" +
                                $"Five Digit: {fiveDigitCount}";

            MessageBox.Show(statistics);
        }

        private void SeparateNumbersAndSaveToFile(string inputFilename, string primesFilename, string fibonacciFilename)
        {
            string[] lines = File.ReadAllLines(inputFilename);
            int[] numbers = lines.Select(int.Parse).ToArray();

            var primes = numbers.Where(IsPrime).Select(n => n.ToString());
            var fibonacci = numbers.Where(IsFibonacci).Select(n => n.ToString());

            File.WriteAllLines(primesFilename, primes);
            File.WriteAllLines(fibonacciFilename, fibonacci);
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (int i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        private bool IsFibonacci(int number)
        {
            double sqrt5 = Math.Sqrt(5);
            double phi = (1 + sqrt5) / 2;
            double a = phi * number;
            return number == 0 || Math.Abs(Math.Round(a) - a) < 1.0 / number;
        }

        private void SearchAndReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            string searchWord = SearchTextBox.Text;
            string replaceWord = ReplaceTextBox.Text;

            SearchAndReplaceInFile("input.txt", "output.txt", searchWord, replaceWord);
            MessageBox.Show("Search and replace completed.");
        }

        private void SearchAndReplaceInFile(string inputFilename, string outputFilename, string searchWord, string replaceWord)
        {
            string[] lines = File.ReadAllLines(inputFilename);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace(searchWord, replaceWord);
            }
            File.WriteAllLines(outputFilename, lines);
        }

        private void ModeratorButton_Click(object sender, RoutedEventArgs e)
        {
            string textFilePath = TextFilePathTextBox.Text;
            string moderationFilePath = ModerationFilePathTextBox.Text;

            if (File.Exists(textFilePath) && File.Exists(moderationFilePath))
            {
                string[] moderationWords = File.ReadAllLines(moderationFilePath);
                string[] textLines = File.ReadAllLines(textFilePath);

                foreach (var moderationWord in moderationWords)
                {
                    for (int i = 0; i < textLines.Length; i++)
                    {
                        textLines[i] = textLines[i].Replace(moderationWord, "*");
                    }
                }

                File.WriteAllLines(textFilePath, textLines);
                MessageBox.Show("Moderation completed.");
            }
            else
            {
                MessageBox.Show("One or both files do not exist.");
            }
        }

        private void ReverseFileButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = ReverseFilePathTextBox.Text;

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                Array.Reverse(lines);
                File.WriteAllLines(filePath, lines);
                MessageBox.Show("File reversed and saved.");
            }
            else
            {
                MessageBox.Show("File does not exist.");
            }
        }

        private void AnalyzeFileButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = NumberFilePathTextBox.Text;

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                int[] numbers = lines.Select(int.Parse).ToArray();

                int positiveCount = numbers.Count(n => n > 0);
                int negativeCount = numbers.Count(n => n < 0);
                int twoDigitCount = numbers.Count(n => n >= 10 && n <= 99);
                int fiveDigitCount = numbers.Count(n => n >= 10000 && n <= 99999);

                PositiveCountTextBlock.Text = positiveCount.ToString();
                NegativeCountTextBlock.Text = negativeCount.ToString();
                TwoDigitCountTextBlock.Text = twoDigitCount.ToString();
                FiveDigitCountTextBlock.Text = fiveDigitCount.ToString();

                File.WriteAllLines("positive_numbers.txt", numbers.Where(n => n > 0).Select(n => n.ToString()));
                File.WriteAllLines("negative_numbers.txt", numbers.Where(n => n < 0).Select(n => n.ToString()));
                File.WriteAllLines("two_digit_numbers.txt", numbers.Where(n => n >= 10 && n <= 99).Select(n => n.ToString()));
                File.WriteAllLines("five_digit_numbers.txt", numbers.Where(n => n >= 10000 && n <= 99999).Select(n => n.ToString()));

                MessageBox.Show("File analyzed and statistics displayed.");
            }
            else
            {
                MessageBox.Show("File does not exist.");
            }
        }
    }
}