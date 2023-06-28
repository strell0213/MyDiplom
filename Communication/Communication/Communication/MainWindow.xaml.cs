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
            var r = AC.Users.Where(c => c.login == LoginText.Text).FirstOrDefault();
            if (r.password == PasswordText.Text) {
                NowClass.NOW = r.login;
                CommunicationWindow communicationWindow = new CommunicationWindow();
                communicationWindow.Show();
            }
             
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrWindow registrWindow = new RegistrWindow();
            registrWindow.Show();
            this.Close();
        }
    }
}
