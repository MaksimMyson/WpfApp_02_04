using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace WpfApp_02_04
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateAndSaveNumbers_Click(object sender, RoutedEventArgs e)
        {
            
            Random random = new Random();
            int[] numbers = Enumerable.Range(0, 10000).Select(i => random.Next(-10000, 10001)).ToArray();

            
            var evenNumbers = numbers.Where(num => num % 2 == 0).ToArray();
            var oddNumbers = numbers.Where(num => num % 2 != 0).ToArray();

            
            string evenFilePath = "even_numbers.txt";
            string oddFilePath = "odd_numbers.txt";
            File.WriteAllLines(evenFilePath, evenNumbers.Select(num => num.ToString()));
            File.WriteAllLines(oddFilePath, oddNumbers.Select(num => num.ToString()));

           
            int totalNumbers = numbers.Length;
            int evenCount = evenNumbers.Length;
            int oddCount = oddNumbers.Length;
            double evenPercentage = (double)evenCount / totalNumbers * 100;
            double oddPercentage = (double)oddCount / totalNumbers * 100;

            string statisticsText = $"Загальна кількість чисел: {totalNumbers}\n" +
                                    $"Парних чисел: {evenCount} ({evenPercentage:F2}%)\n" +
                                    $"Непарних чисел: {oddCount} ({oddPercentage:F2}%)";

            StatisticsTextBox.Text = statisticsText;
        }
    }
}
