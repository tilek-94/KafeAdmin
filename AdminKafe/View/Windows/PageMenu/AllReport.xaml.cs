using System.Windows.Controls;
using Syncfusion.UI.Xaml.Grid.Converter;
using Microsoft.Win32;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace AdminKafe.Windows.PageMenu
{
    /// <summary>
    /// Логика взаимодействия для AllReport.xaml
    /// </summary>
    public partial class AllReport
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

        private void BtnSave(object sender, System.Windows.RoutedEventArgs e)
        {
            PdfExportingOptions options = new PdfExportingOptions();
            options.RepeatHeaders = true;
            options.ExportFormat = true;
            options.FitAllColumnsInOnePage = true;
            options.ExportAllPages = true;
            var document = SfDataGridSave.ExportToPdf(options);
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files(*.pdf)|*.pdf"
            };
            if (sfd.ShowDialog() == true)
            {
                using (Stream stream = sfd.OpenFile())
                {
                    document.Save(stream);
                }
            }
            /*if (sfd.ShowDialog() == true)
            {
                switch (sfd.FilterIndex)
                {
                    case 1:
                        PDFSave();
                        break;

                    case 2:
                        ExelSave();
                        break;
                }
            }*/
        }
       
        /*private void PDFSave()
        {
            PdfExportingOptions options = new PdfExportingOptions();
            options.RepeatHeaders = true;
            options.ExportFormat = true;
            options.FitAllColumnsInOnePage = true;
            options.ExportAllPages = true;
            var document = SfDataGridSave.ExportToPdf(options);

            using (Stream stream = sfd.OpenFile())
            {
                document.Save(stream);
            }
        }
        private void ExelSave()
        {

            var options = new ExcelExportingOptions();
            options.ExportAllPages = false;
            options.ExcelVersion = ExcelVersion.Excel2013;
            var excelEngine = SfDataGridSave.ExportToExcel(SfDataGridSave.View, options);
            var workBook = excelEngine.Excel.Workbooks[0];
            using (Stream stream = sfd.OpenFile())
            {
                workBook.SaveAs(stream);
                Workbook book = excelApp.Workbooks.Open("filePathHere");
                string[] sheetsToDelete = { "s1", "s2" };
                excelApp.DisplayAlerts = false;
            }
        }*/
    }
}
