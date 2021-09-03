using AdminKafe.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace AdminKafe.Windows.PageMenu
{
    /// <summary>
    /// Логика взаимодействия для AddStol.xaml
    /// </summary>
    public partial class AddStol : Page
    {
        public AddStol()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void dbGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int columnIndex = dbGrid.CurrentColumn.DisplayIndex;
            if (columnIndex == 2)
            {
                //DataRowView dataRow = (DataRowView)dbGrid.SelectedItem;
                //ComboBox1.Text = dbGrid.selected
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            object item = dbGrid.SelectedItem;
            DateWorker.AllId = Convert.ToInt32((dbGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            ComboBox1.Text = (dbGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            Textbox1.Text = (dbGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
        }
    }
}
