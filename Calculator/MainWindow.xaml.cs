using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string ZeroValue = "0";
        public Input Input { get; set; } = new Input() { Value = ZeroValue };
        private Actions currentAction = Actions.None;
        private double oldValue = float.Parse(ZeroValue);
        private bool actual = false;
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Input;
        }

        private void buttonNumber_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string num = button.Content.ToString();
            if (Input.Value == ZeroValue)
            {
                Input.Value = num;
                return;
            }
            
            if (!actual)
            {
                Input.Value += num;
                return;
            }
            Input.Value = num;
            actual = false;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            Input.Value = ZeroValue;
            currentAction = Actions.None;
            oldValue = double.Parse(ZeroValue);
            actual = false;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string value = Input.Value;
            if (value == ZeroValue)
                return;

            if (value.Length == 1)
            {
                Input.Value = ZeroValue;
                return;
            }

            Input.Value = value.Remove(value.Length - 1, 1);
        }

        private void sqrtButton_Click(object sender, RoutedEventArgs e)
        {
            Input.Value = Math.Sqrt(double.Parse(Input.Value)).ToString();
        }

        private void comma_Click(object sender, RoutedEventArgs e)
        {
            if (Input.Value.Contains(","))
                return;

            Input.Value += ",";
        }

        private void buttonAction_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (currentAction != Actions.None)
            {
                double result = Solve();
                oldValue = result;
            }
            else
            {
                double.TryParse(Input.Value, out double value);
                oldValue = value;
            }

            Input.Value = ZeroValue;
            string action = button.Content.ToString();

            switch (action)
            {
                case "+":
                    currentAction = Actions.Plus;
                    break;
                case "-":
                    currentAction= Actions.Minus;
                    break;
                case "*":
                    currentAction = Actions.Multiply;
                    break;
                case "/":
                    currentAction = Actions.Divide;
                    break;
                case "^":
                    currentAction = Actions.Power;
                    break;
            }
        }

        private void equalsButton_Click(object sender, RoutedEventArgs e)
        {
            double result = Solve();

            currentAction = Actions.None;
            actual = true;
            Input.Value = result.ToString();
        }

        private double Solve()
        {
            double newValue = float.Parse(Input.Value);
            double result;
            switch (currentAction)
            {
                case Actions.Plus:
                    result = oldValue + newValue;
                    break;
                case Actions.Minus:
                    result = oldValue - newValue;
                    break;
                case Actions.Multiply:
                    result = oldValue * newValue;
                    break;
                case Actions.Divide:
                    result = newValue != 0 ? oldValue / newValue : 0;
                    break;
                case Actions.Power:
                    result = Math.Pow(oldValue, newValue);
                    break;
                default:
                    result = 0;
                    break;
            }

            return result;
        }
    }
}
