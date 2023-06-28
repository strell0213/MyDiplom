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
    /// Логика взаимодействия для NowProfileWindow.xaml
    /// </summary>
    public partial class NowProfileWindow : Window
    {
        AppContext AC;
        public NowProfileWindow()
        {
            InitializeComponent();
            AC = new AppContext();
            var r = AC.Users.Where(c => c.login == NowClass.newprof).FirstOrDefault();
            if (r.RoleID == 1) {
                LoginBlock.Text = r.login + "\nПользователь";
            }
            else if (r.RoleID == 2)
            {
                LoginBlock.Text = r.login + "\nЭксперт";
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.Users.Where(c => c.login == NowClass.newprof).FirstOrDefault();
            if (r.RoleID == 1)
            {
                r.RoleID = 2;
                AC.SaveChanges(); MessageBox.Show("Успешно!", "Communication");
            }
            else if (r.RoleID == 2)
            {
                MessageBox.Show("Этот пользователь уже является экспертом", "Communication");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.Users.Where(c => c.login == NowClass.newprof).FirstOrDefault();
            AC.Users.Remove(r);
            AC.SaveChanges();
            NowClass.newprof = null;
            MessageBox.Show("Успешно!", "Communication");
            AllProfileWindow allProfileWindow = new AllProfileWindow();
            allProfileWindow.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AllProfileWindow allProfileWindow = new AllProfileWindow();
            allProfileWindow.Show();
            this.Close();
        }
    }
}
