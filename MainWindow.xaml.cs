using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Calculation_1._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach(UIElement element in MainRoot.Children)
            {
                if(element is Button)
                {
                    ((Button)element).Click += ButtonClick;
                }
            }

            foreach (UIElement el in OtherM.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += ButtonClick;
                }

            }
        }

        private void ButtonClick(object sender, RoutedEventArgs text)
        {
            string textPlane = (string)((Button)text.OriginalSource).Content;

            switch (textPlane)
            {
                
                case "C":
                    textUp.Text = string.Empty;
                    break;

                case "CE":
                    int i = textUp.Text.Length-1;
                    while (i >= 1)
                    {
                        if (textUp.Text[i] == '+' || textUp.Text[i] == '-' || textUp.Text[i] == '/' || textUp.Text[i] == '*' || textUp.Text[i] == '%')
                        {
                            break;
                        }
                        i--;
                    }

                    textUp.Text = textUp.Text.Substring(0, i);
                    break;

                case "=":
                    string value = new DataTable().Compute(textUp.Text, null).ToString();
                    value = value.Replace(",",".");
                    textUp.Text = value;
                    break;

                case ",":
                    textUp.Text += ".";
                    break;

                case "÷":
                    textUp.Text += "/";
                    break;

                case "✕":
                    textUp.Text += "*";
                    break;

                case "x²":
                    value = new DataTable().Compute(textUp.Text, null).ToString();
                    value = value.Replace(",", ".");
                    textUp.Text = value;

                    string val = Convert.ToString(Convert.ToDouble( textUp.Text) * Convert.ToDouble(textUp.Text));
                    val = val.Replace(",", ".");
                    textUp.Text = val;
                    break;

                case "⌫":
                    if(textUp.Text.Length!= 0)
                    {
                        textUp.Text = textUp.Text.Remove(textUp.Text.Length - 1);
                    }
                    break;

                default:
                    textUp.Text += textPlane;
                    break;
            }

           
        }
    }
}
