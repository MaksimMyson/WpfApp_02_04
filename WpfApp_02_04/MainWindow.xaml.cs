using System;
using System.IO;
using System.Text;
using System.Windows;

namespace WpfApp_02_04
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateNumbers_Click(object sender, RoutedEventArgs e)
        {
        
            int[] numbers = GenerateNumbers(100);

            
            SavePrimesAndFibonacci(numbers);

          
            DisplayStatistics(numbers);
        }

        private int[] GenerateNumbers(int count)
        {
            Random random = new Random();
            int[] numbers = new int[count];
            for (int i = 0; i < count; i++)
            {
                numbers[i] = random.Next(1, 1000); 
            }
            return numbers;
        }

        private void SavePrimesAndFibonacci(int[] numbers)
        {
            StringBuilder primesBuilder = new StringBuilder();
            StringBuilder fibonacciBuilder = new StringBuilder();

            foreach (int number in numbers)
            {
                if (IsPrime(number))
                {
                    primesBuilder.AppendLine(number.ToString());
                }

                if (IsFibonacci(number))
                {
                    fibonacciBuilder.AppendLine(number.ToString());
                }
            }

            File.WriteAllText("Primes.txt", primesBuilder.ToString());
            File.WriteAllText("Fibonacci.txt", fibonacciBuilder.ToString());
        }

        private bool IsPrime(int number)
        {
            if (number <= 1)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsFibonacci(int number)
        {
            int a = 0, b = 1;
            while (b < number)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }

            return b == number;
        }

        private void DisplayStatistics(int[] numbers)
        {
            int primeCount = 0;
            int fibonacciCount = 0;

            foreach (int number in numbers)
            {
                if (IsPrime(number))
                {
                    primeCount++;
                }

                if (IsFibonacci(number))
                {
                    fibonacciCount++;
                }
            }

            ResultTextBox.Text = $"Загальна кількість чисел: {numbers.Length}\n" +
                                 $"Кількість простих чисел: {primeCount}\n" +
                                 $"Кількість чисел Фібоначчі: {fibonacciCount}";
        }
    }
}
