using AdminKafe.Date;
using AdminKafe.Windows;
using System.Linq;
using System.Windows;
using AdminKafe.Models;

namespace AdminKafe.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewPassword.xaml
    /// </summary>
    public partial class AddNewPassword : Window
    {
        public AddNewPassword()
        {
            InitializeComponent();
        }

        private void Saves(object sender, RoutedEventArgs e)
        {
            EditMethod();
        }
        private void EditMethod()
        {
            string result = EditDate(Name.Text, Password.Text, Password2.Text);
        }

        public static string EditDate(string Name, string Password, string Password2)
        {
            using (ApplicationContext connetc = new ApplicationContext())
            {
                Login us = connetc.login.FirstOrDefault(t => t.Name == Name);
                us.Name = Name;
                us.Password = Password;
                connetc.SaveChanges();
                return "Успешно Добавлено";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow log = new LoginWindow();
            log.Show();
            this.Close();
        }
    }
}
