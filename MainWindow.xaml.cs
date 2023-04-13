namespace Calculation_1._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
        }

        private void ButtonClick(object sender, RoutedEventArgs text)
        {
            string textPlane = (string)((Button)text.OriginalSource).Content;

            switch (textPlane)
            {
                
                case "AC":
                    textUp.Text = string.Empty;
                    break;

                case "=":
                    string value = new DataTable().Compute(textUp.Text, null).ToString();
                    textUp.Text = value;
                    break;

                default:
                    textUp.Text += textPlane;
                    break;
            }

           
        }
    }
}
