using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Calculation_1._0
{
    public partial class MainWindow : System.Windows.Window
    {
        decimal MemoryStore = 0;
        bool equal = false;
        string input = "";

        public MainWindow()
        {
            InitializeComponent();

            int memory = 0;

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
                    if(textUp.Text != "")
                    {
                        while (i >= 1)
                        {
                            if (textUp.Text[i] == '+' || textUp.Text[i] == '-' || textUp.Text[i] == '/' || textUp.Text[i] == '*' || textUp.Text[i] == '%')
                            {
                                break;
                            }
                            i--;
                        }

                        textUp.Text = textUp.Text.Substring(0, i);
                    }
                    
                    break;

                case "1/x":
                    if(textUp.Text != "" && textUp.Text != "0")
                    {
                        string value_ = new DataTable().Compute("1.0/"+textUp.Text, null).ToString();
                        textUp.Text = value_.Replace(",", ".");
                    }
                    break;

                case "+/-":
                    if (textUp.Text != "")
                    {
                        if(textUp.Text[0] != '-')
                            textUp.Text = "-" + textUp.Text;
                        else
                            textUp.Text = textUp.Text.Substring(1, textUp.Text.Length-1);                  
                    }
                    break;

                case "√x":
                    if (textUp.Text != "")
                    {
                        if (new DataTable().Compute(textUp.Text, null).ToString().Contains("-"))
                            textUp.Text = "Нельзя взять корень";
                        else
                        {
                            textUp.Text = Math.Sqrt(Convert.ToDouble(new DataTable().Compute(textUp.Text, null).ToString())).ToString().Replace(",", "."); 
                        }   
                    }
                    break;

                case "%":
                        int count = textUp.Text.Length - 1;
                        string rightStr = "";
                    if(textUp.Text.Contains("+") || textUp.Text.Contains("-") || textUp.Text.Contains("*") || textUp.Text.Contains("/"))
                    {
                        while (textUp.Text[count] != '+' && textUp.Text[count] != '-' && textUp.Text[count] != '/' && textUp.Text[count] != '*')
                        {
                            rightStr = textUp.Text[count] + rightStr;
                            count--;
                        }
                        if(rightStr == "")
                        {
                            break;
                        }

                        double leftVal = Convert.ToDouble(textUp.Text.Substring(0, count).Replace(".", ","));

                        double valProc = Convert.ToDouble(rightStr.Replace(",", ".")) / 100.0 * leftVal;
                        textUp.Text = (textUp.Text.Substring(0, count + 1).Replace(",", ".") + valProc).Replace(",", ".");
                    }
                    break;


                case ",":
                    textUp.Text += ".";
                    break;

                case "÷":
                    input = textUp.Text+ "/" ;
                    textUp.Text = "";
                    break;

                case "✕":
                    input = textUp.Text + "*";
                    textUp.Text = "";
                    break;

                case "+":
                    input = textUp.Text + "+";
                    textUp.Text = "";
                    break;

                case "-":
                    input = textUp.Text + "-";
                    textUp.Text = "";
                    break;

                case "x²":
                    if (textUp.Text != "")
                    {
                        textUp.Text = new DataTable().Compute(new DataTable().Compute(textUp.Text.Replace(",", "."), null).ToString().Replace(",", ".") + "*" + new DataTable().Compute(textUp.Text.Replace(",", "."), null).ToString().Replace(",", "."), null).ToString().Replace(",", ".");
                    }
                        break;

                case "⌫":
                    if(textUp.Text.Length!= 0)
                    {
                        textUp.Text = textUp.Text.Remove(textUp.Text.Length - 1);
                        input = input.Remove(input.Length - 1);
                    }
                    break;

                case "=": 
                    string value = new DataTable().Compute(input, null).ToString();
                    value = value.Replace(",", ".");
                    textUp.Text = value;
                    equal = true;
                    break;

                case "MS":
                    MemoryStore = (decimal)Convert.ToDouble(new DataTable().Compute(textUp.Text, null).ToString());
                    break;

                case "MC":
                    MemoryStore = 0;
                    break;

                case "MR":
                    if(MemoryStore != 0)
                        textUp.Text = MemoryStore.ToString().Replace(",", ".");
                    break;

                case "M+":
                    if (MemoryStore != 0 && textUp.Text != "")
                        textUp.Text = (Convert.ToDecimal(new DataTable().Compute(textUp.Text, null)) + MemoryStore).ToString().Replace(",", ".");
                    break;

                case "M-":
                    if (MemoryStore != 0 && textUp.Text != "")
                        textUp.Text = (Convert.ToDecimal(new DataTable().Compute(textUp.Text, null)) - MemoryStore).ToString().Replace(",", ".");
                    break;

                

                default:
                    if(textUp.Text == "0")
                    {
                        textUp.Text = textPlane;
                        input = textPlane;
                    }

                    else
                    {
                        if (equal == true)
                        {
                            textUp.Text = textPlane;
                            input = textUp.Text;
                            equal = false;
                        }
                        else
                        {
                            textUp.Text += textPlane;
                            input += textUp.Text;
                        }
                    }
                       
                        

                    break;
            }

           
        }
    }
}
