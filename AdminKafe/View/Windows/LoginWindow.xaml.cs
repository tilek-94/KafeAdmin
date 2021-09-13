
using AdminKafe.Date;
using AdminKafe.Models;
using AdminKafe.View.Windows;
using AdminKafe.View.Windows.PageMenu;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AdminKafe.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static int stat = 0;
        GlavWindow main;
        AddNewPassword pass;
        MessageWindowOk messageWindowOk;
        Login login1;
        public LoginWindow()
        {
            InitializeComponent();
            main = new GlavWindow();
            main.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Saves(object sender, RoutedEventArgs e)
        {
            Mainshow();
        }

        public void Mainshow()
        {

            if (Name.Text != "" && Password.Password != "")
            {                
                if (Sign_in(Name.Text, Password.Password))
                {
                    this.Close();
                }
            }
            else
            {
                messageWindowOk = new MessageWindowOk("Заполните текстовое поле!");
                messageWindowOk.ShowDialog();
            }
        }

        public bool Sign_in(string login, string password)
        {
            bool result = false;
            using (ApplicationContext connetc = new ApplicationContext())
            {
                string show = "Неверный парол или логин!";
                bool b = connetc.login.Any(y => y.Name == login && y.Password == password);
                if (b)
                {
                    login1 = connetc.login.FirstOrDefault(y => y.Name == login && y.Password == password);
                    stat = login1.Id;
                    main.Showmain();
                    result = true;
                }
                else
                {
                    messageWindowOk = new MessageWindowOk(show);
                    messageWindowOk.ShowDialog();
                }
            }
            return result;
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if (Name.Text != "" && Password.Password != "")
            {
                if (Sign_in(Name.Text, Password.Password))
                {
                    pass = new AddNewPassword(login1);
                    pass.Show();
                    this.Close();
                }
                else
                {
                    messageWindowOk = new MessageWindowOk("Логин паролунузду туура жазыныз!");
                    messageWindowOk.ShowDialog();
                }
            }
            else
            {
                messageWindowOk = new MessageWindowOk("Кечиресиз биринчи логин паролунузду жазыныз!");
                messageWindowOk.ShowDialog();
            }
        }
    }
}
