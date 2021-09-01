using AdminKafe.Models;
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
using System.Windows.Shapes;

namespace AdminKafe.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddProductParameter.xaml
    /// </summary>
    public partial class AddProductParameter : Window
    {
        public AddProductParameter()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            object item = dbGrid.SelectedItem; //probably you find this object
            DateWorker.AllId = Convert.ToInt32((dbGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            Element1.Text = (dbGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            Element2.Text = (dbGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
            Element3.Text = (dbGrid.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;

        }
    }
}
