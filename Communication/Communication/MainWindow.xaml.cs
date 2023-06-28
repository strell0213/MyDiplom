using System;
using System.Collections.Generic;
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

namespace Communication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext AC;
        public MainWindow()
        {
            InitializeComponent();
            AC = new AppContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LoginText.Text != null || PasswordText.Password != null)
                {
                    var r = AC.Users.Where(c => c.login == LoginText.Text).FirstOrDefault();
                    if (r.password == PasswordText.Password)
                    {
                        if (r.RoleID == 1)
                        {
                            NowClass.NOW = r.login;
                            r.OnlineID = 1;
                            AC.SaveChanges();
                            CommunicationWindow communicationWindow = new CommunicationWindow();
                            communicationWindow.Show();
                            this.Close();
                        }
                        else if (r.RoleID == 2)
                        {
                            NowClass.NOW = r.login;
                            r.OnlineID = 1;
                            AC.SaveChanges();
                            AdminCommunicationWindow adminCommunicationWindow = new AdminCommunicationWindow();
                            adminCommunicationWindow.Show();
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Вы ничего не ввели", "Communication", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Не верный логин или пароль!", "Communication", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

    private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrWindow registrWindow = new RegistrWindow();
            registrWindow.Show();
            this.Close();
        }

        //функции для юнит тестов
        public string testlog(string res, string log, string pass)
        {
            if (log != null || pass != null)
            {
                return res = "Успешно!";
            }
            else
            {
                return res = "Ошибка";
            }
        }
    }

}
