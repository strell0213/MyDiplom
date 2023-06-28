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
            if (PasswordText.Text == RePasswordText.Text)
            {
                User user = new User(LoginText.Text, PasswordText.Text, 0, 1);
                AC.Users.Add(user);
                AC.SaveChanges();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else {
                MessageBox.Show("Пароли не совпадают","Communication", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
