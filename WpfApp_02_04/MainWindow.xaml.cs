using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace WpfApp_02_04
{
    public partial class MainWindow : Window
    {
        private int[]? _array;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_array == null || _array.Length == 0)
            {
                MessageBox.Show("Масив порожній", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var filePath = FilePathTextBox.Text;

            try
            {
                File.WriteAllText(filePath, string.Join(",", _array));
                MessageBox.Show("Масив успішно збережено у файл", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = FilePathTextBox.Text;

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не існує", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var content = File.ReadAllText(filePath);
                _array = content.Split(',').Select(int.Parse).ToArray();
                ArrayContentTextBox.Text = string.Join(Environment.NewLine, _array);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(InputTextBox.Text, out int newValue))
            {
                MessageBox.Show("Неправильний формат числа", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_array == null)
            {
                _array = new int[] { newValue };
            }
            else
            {
                Array.Resize(ref _array, _array.Length + 1);
                _array[_array.Length - 1] = newValue;
            }

            ArrayContentTextBox.Text = string.Join(Environment.NewLine, _array);
            InputTextBox.Clear();
        }
    }
}
