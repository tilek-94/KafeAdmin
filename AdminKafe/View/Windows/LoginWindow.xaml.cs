using AdminKafe.Date;
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
        public static string stat = "";
        GlavWindow main;
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
                Sign_in(Name.Text, Password.Password);
                this.Close();
            }
            else
            {
                MessageBox.Show("Текстик поляны толтурунуз!");
            }
        }

        public void Sign_in(string login, string password)
        {
            using (ApplicationContext connetc = new ApplicationContext())
            {
                string show = "Неверный парол или логин";
                bool b = connetc.login.Any(y => y.Name == login && y.Password == password);
                if (b)
                {
                    stat = Name.Text;
                    main.Showmain();
                }
                else
                {
                    MessageBox.Show(show);
                }
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AddNewPassword pass = new AddNewPassword();
            pass.Show();
            this.Close();
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            AddNewLogin log = new AddNewLogin();
            log.Show();
            this.Close();
        }
    }
}
