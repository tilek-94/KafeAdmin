using AdminKafe.Date;
using AdminKafe.Models;
using AdminKafe.Windows;
using System;
using System.Windows;

namespace AdminKafe.View.Windows.PageMenu
{
    /// <summary>
    /// Логика взаимодействия для AddNewLogin.xaml
    /// </summary>
    public partial class AddNewLogin : Window
    {
        public static string Statuse=String.Empty;
        MessageWindowOk MessageWindowOk;

        public AddNewLogin()
        {
            InitializeComponent();
        }

        private void checkCheck()
        {
            CheckboxC();
        }

        private void CreateMethod()
        {
            checkCheck();
            if (Statuse != "")
            {
                AddDate(Name.Text, Password.Text, Password2.Text, Statuse);
            }
            else
            {
                AddDate(Name.Text, Password.Text, Password2.Text, "Null");
            }
        }

        public string AddDate(string name, string password, string password2, string Status)
        {
            using (ApplicationContext connetc = new ApplicationContext())
            {
                if (name != "" && password != "" && password2 != "")
                {
                    if (password == password2)
                    {
                        if (Statuse != String.Empty)
                        {
                            Login logins = new Login
                            {
                                Name = name,
                                Password = password,
                                Status = Status,
                            };
                            connetc.login.Add(logins);
                            connetc.SaveChanges();
                            MessageWindowOk = new MessageWindowOk("Ийгиликтуу сакталды!");
                            MessageWindowOk.ShowDialog();
                            Clear();

                        }
                        else
                        {
                            Message("Выберите Разделы!!!");
                        }
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
                return "";
            }
        }
        private void Clear()
        {
            Name.Text = "";
            Password.Text = "";
            Password2.Text = "";
            Statuse = "";
        }
        private void Message(string mess)
        {
            MessageWindowOk = new MessageWindowOk(mess);
            MessageWindowOk.ShowDialog(); ;
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
            if (CheckBox8.IsChecked == true)
            {
                Statuse += "7";
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            CreateMethod();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
