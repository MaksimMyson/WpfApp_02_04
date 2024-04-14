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

        private async void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = FilePathTextBox.Text;

            if (File.Exists(filePath))
            {
                try
                {
                    var fileContent = await File.ReadAllTextAsync(filePath);
                    FileContentTextBox.Text = fileContent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при зчитуванні файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Файл не існує", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}