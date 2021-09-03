using LazZiya.ImageResize;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
    /// Interaction logic for AddMenuFoodWindow.xaml
    /// </summary>
    public partial class AddMenuFoodWindow : Window
    {
        public AddMenuFoodWindow()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
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
    }
}
