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
        private static int ID {get;set;}
        MessageWindowOk MessageWindowOk;
        public AddNewPassword(Login login)
        {
            InitializeComponent();
            ID = login.Id;
        }

        private void Saves(object sender, RoutedEventArgs e)
        {
            EditMethod();
        }
        private void EditMethod()
        {
            string result = EditDate(NameT.Text, PasswordT.Text, Password2T.Text);
        }

        public string EditDate(string Name, string Password, string Password2)
        {
            using (ApplicationContext connetc = new ApplicationContext())
            {
                string result = "Ийгиликтуу сакталган жок";
                if ( Name !="" &&  Password != "" &&  Password2 !="" )
                {
                    if (Password == Password2)
                    {
                        Login us = connetc.login.FirstOrDefault(t => t.Id == ID);
                        us.Name = Name;
                        us.Password = Password;
                        connetc.SaveChanges();
                        result= "Успешно Добавлено";
                        MessageWindowOk = new MessageWindowOk(result);
                        MessageWindowOk.ShowDialog();
                        Clear();
                    }
                    else
                    {
                        MessageWindowOk = new MessageWindowOk("Пароль бири бирине туура келбей жата!");
                        MessageWindowOk.ShowDialog();
                    }
                }
                else
                {
                    MessageWindowOk = new MessageWindowOk("Баардык жолчолорду толтурунуз!");
                    MessageWindowOk.ShowDialog();
                }

                return result;
            }

        }
        private void Clear()
        {
            NameT.Text = "";
            PasswordT.Text = "";
            Password2T.Text = "";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow log = new LoginWindow();
            log.Show();
            this.Close();
        }
    }
}
