using System.Windows.Controls;

namespace AdminKafe.Windows.PageMenu
{
    /// <summary>
    /// Логика взаимодействия для AllReport.xaml
    /// </summary>
    public partial class AllReport : Page
    {
        public static int RadioCheck = 0;
        public static int ComboChek = 0;
        public static int RadioCheckStatus = 0;
        public static int StatusTrue = 0;

        public AllReport()
        {
            InitializeComponent();
        }

        private void RadioDay_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (RadioDay.IsChecked == true)
            {
                RadioCheck = 1;
            }
            else if (RadioDays.IsChecked == true)
            {
                RadioCheck = 2;
            }
            else
            {
                RadioCheck = 0;
            }

        }

        private void RadioDays_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // OneDate.SelectedDate = null;

            if (RadioDay.IsChecked == true)
            {
                RadioCheck = 1;
            }
            else if (RadioDays.IsChecked == true)
            {
                RadioCheck = 2;
            }
            else
            {
                RadioCheck = 0;
            }
        }

        private void CheckOfissiant_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

            if (CheckOfissiant.IsChecked == true)
            {
                ComboChek = 1;
            }
            else
            {
                ComboChek = 0;
            }
        }

        private void RadioButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RadioCheckStatus = 4;
        }

        private void RadioButton_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            RadioCheckStatus = 3;
        }

        private void RadioButton_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            RadioCheckStatus = 2;
        }

        private void CheckStatus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CheckStatus.IsChecked == true)
            {
                StatusTrue = 1;
            }
            else
            {
                StatusTrue = 0;
            }
        }
    }
}
