using AdminKafe.Date;
using AdminKafe.Models;
using AdminKafe.Windows;
using System.Windows;

namespace AdminKafe.View.Windows.PageMenu
{
    /// <summary>
    /// Логика взаимодействия для AddNewLogin.xaml
    /// </summary>
    public partial class AddNewLogin : Window
    {
        public static string Statuse;


        public AddNewLogin()
        {
            InitializeComponent();
        }

        private void checkCheck()
        {
            CheckboxC();
                if(Statuse.Length == 0)
                {
                MessageBox.Show("Выберите Разделы!!!");
                }
        }

        private void CreateMethod()
        {

            checkCheck();
            if (Name.Text != "" && Password.Text != "" && Password2.Text != "")
            {
                if (Password.Text == Password2.Text)
                {
                    AddDate(Name.Text, Password.Text, Statuse);
                    Name.Text = string.Empty;
                    Password.Text = string.Empty;
                    Password2.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Текстик поляны толтурунуз!");
                }

            }
            else
            {
                MessageBox.Show("Текстик поляны толтурунуз!");
            }
        }

        public static string AddDate(string Name, string Passwor, string Status)
        {
            using (ApplicationContext connetc = new ApplicationContext())
            {
                Login logins = new Login
                {
                    Name = Name,
                    Password = Passwor,
                    Status = Status,
                };
                connetc.login.Add(logins);
                connetc.SaveChanges();
                return "Успешно Добавлено";
            }
        }

        public void CheckboxC()
        {

            if (CheckBox1.IsChecked == true)
            {
                Statuse += "0";
            }
            if (CheckBox2.IsChecked == true)
            {
                Statuse += "1";
            }
            if (CheckBox3.IsChecked == true)
            {
                Statuse += "2";
            }
            if (CheckBox4.IsChecked == true)
            {
                Statuse += "3";
            }
            if (CheckBox5.IsChecked == true)
            {
                Statuse += "4";
            }
            if (CheckBox6.IsChecked == true)
            {
                Statuse += "5";
            }
            if (CheckBox7.IsChecked == true)
            {
                Statuse += "6";
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            CreateMethod();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow log = new LoginWindow();
            log.Show();
            this.Close();
        }
    }
}
