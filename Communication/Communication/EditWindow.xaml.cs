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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        AppContext AC;
        public EditWindow()
        {
            InitializeComponent();
            AC = new AppContext();
            
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordText.Text == RePasswordText.Text)
            {
                var r = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
                r.login = LoginText.Text;
                r.password = PasswordText.Text;
                AC.SaveChanges();
                MessageBox.Show("Успешно", "Communication", MessageBoxButton.OK, MessageBoxImage.Information);
                var r1 = AC.Users.Where(c => c.login == LoginText.Text).FirstOrDefault();
                NowClass.NOW = r.login;
                ProfileWindow profileWindow = new ProfileWindow();
                profileWindow.Show();
                this.Close();
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }
    }
}
