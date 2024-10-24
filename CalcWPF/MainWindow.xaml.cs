using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalcWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void System1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            TB1.Clear();
        }
        private void System2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TB2.Clear();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string selectedSystem = (sender == TB1) ? System1.Text : System2.Text;
            string systemPattern = "";

            switch (selectedSystem)
            {
                case "Десяткова":
                    systemPattern = "^[0-9]+$";
                    break;
                case "Двійкова":
                    systemPattern = "^[01]+$";
                    break;
                case "Вісімкова":
                    systemPattern = "^[0-7]+$";
                    break;
                case "Шістнадцяткова":
                    systemPattern = "^[0-9A-F]+$";
                    break;
                default:
                    e.Handled = true;
                    break;
            }

            Regex regex = new Regex(systemPattern);
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
            if (e.Text == " ")
            {
                e.Handled = true;
                return;
            }
        }
        private int Convertor(string number, string numberSystem)
        {
            switch (numberSystem)
            {
                case "Десяткова":
                    return int.Parse(number);
                case "Двійкова":
                    return Convert.ToInt32(number, 2);
                case "Вісімкова":
                    return Convert.ToInt32(number, 8);
                case "Шістнадцяткова":
                    return Convert.ToInt32(number, 16);
                default:
                    throw new InvalidOperationException("Wrong number system");
            }   
        }

        private void doButton_Click(object sender, RoutedEventArgs e)
        {
            int firstNumber = Convertor(TB1.Text, System1.Text);
            int secondNumber = Convertor(TB2.Text, System2.Text);
            int result = 0;
            switch (actionBox.Text)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    if (secondNumber != 0)
                    {
                        result = firstNumber / secondNumber;
                    }
                    else
                    {
                        MessageBox.Show("На 0 ділити не можна");
                    }
                    break;
            }
            result10.Text = result.ToString();
            result16.Text = Convert.ToString(result, 16).ToUpper();
            result2.Text = Convert.ToString(result, 2);
            result8.Text = Convert.ToString(result, 8);
        }

    }
}
