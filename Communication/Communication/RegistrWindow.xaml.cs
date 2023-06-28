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
using System.Windows.Shapes;

namespace Communication
{
    /// <summary>
    /// Логика взаимодействия для RegistrWindow.xaml
    /// </summary>
    public partial class RegistrWindow : Window
    {
        AppContext AC;
        public RegistrWindow()
        {
            InitializeComponent();
            AC = new AppContext();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginText.Text != null || PasswordText.Password != null)
            {
                if (!LoginText.Text.Contains("@#$%&*!?"))
                {
                    if (PasswordText.Password == RePasswordText.Password)
                    {
                        User user = new User(LoginText.Text, PasswordText.Password, " ", 1, 0, 2);
                        AC.Users.Add(user);
                        AC.SaveChanges();
                        MessageBox.Show("Успешно!", "Communication", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают", "Communication", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Логин имеет недопустимые знаки", "Communication", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы ничего не ввели", "Communication", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        //функции для юнит тестов
        public string reg(string res, string log, string pass, string repass) {
            if (log != null || pass != null)
            {
                if (!log.Contains("@#$%&*!?"))
                {
                    if (pass == repass)
                    {
                        return res = "Успешно!";
                    }
                    else
                    {
                        return res = "Ошибка";
                    }
                }
                else
                {
                    return res = "Ошибка";
                }
            }
            else
            {
                return res = "Ошибка";
            }
        }
    }
}
