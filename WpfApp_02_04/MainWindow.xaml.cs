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

        private void ReplaceWords_Click(object sender, RoutedEventArgs e)
        {
            string searchWord = SearchTextBox.Text.Trim();
            string replaceWord = ReplaceTextBox.Text.Trim();

            if (string.IsNullOrEmpty(searchWord) || string.IsNullOrEmpty(replaceWord))
            {
                MessageBox.Show("Будь ласка, введіть слово для пошуку та слово для заміни.");
                return;
            }

            string filePath = "C:\\Users\\user\\source\\repos\\WpfApp_02_04\\wpf_02_04.txt"; 
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не знайдено.");
                return;
            }

            string fileContent = File.ReadAllText(filePath);

            int replacementsCount = ReplaceWords(ref fileContent, searchWord, replaceWord);

            File.WriteAllText(filePath, fileContent);

            ResultTextBox.Text = $"Кількість замін: {replacementsCount}";
        }

        private int ReplaceWords(ref string text, string searchWord, string replaceWord)
        {
            int count = Regex.Matches(text, searchWord).Count;
            text = text.Replace(searchWord, replaceWord);
            return count;
        }
    }
}
