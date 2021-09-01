using AdminKafe.View.Windows;
using LazZiya.ImageResize;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AdminKafe.Windows.PageMenu
{
    /// <summary>
    /// Логика взаимодействия для AddFood.xaml
    /// </summary>
    public partial class AddFood : Page
    {
        public AddFood()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif";

            if (dlg.ShowDialog() == true)
            {
                string FileName = dlg.FileName.ToString();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(FileName);
                bitmap.EndInit();
                //imgBox.Source = bitmap;

                var im = System.Drawing.Image.FromFile(FileName);
                var img = ImageResize.Scale(im, 300, 300);


                using (var ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    imgBox.Source = bitmapImage;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddMenuFoodWindow wn = new AddMenuFoodWindow();
            wn.ShowDialog();

        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("dfd");
        }
    }
}
