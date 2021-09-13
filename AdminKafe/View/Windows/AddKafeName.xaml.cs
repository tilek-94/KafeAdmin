using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
using System.Windows.Shapes;

namespace AdminKafe.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddKafeName.xaml
    /// </summary>
    public partial class AddKafeName : Window
    {
        public AddKafeName()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                if (pkInstalledPrinters != null)
                {
                    Combo1.Items.Add(pkInstalledPrinters);
                    Combo2.Items.Add(pkInstalledPrinters);
                }
            }
        }
    }
}
