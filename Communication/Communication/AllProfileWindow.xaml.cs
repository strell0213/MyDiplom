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
    /// Логика взаимодействия для AllProfileWindow.xaml
    /// </summary>
    public partial class AllProfileWindow : Window
    {
        AppContext AC;
        System.Windows.Threading.DispatcherTimer timer;
        public AllProfileWindow()
        {
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timertick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            AC = new AppContext();
            updatelist();
        }

        private void timertick(object sender, EventArgs e)
        {

        }

        public void updatelist() {
            var List = AC.Users.ToList();
            ListProf.ItemsSource = List;
        }

        private void DeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            var buttondel = (Button)sender;
            var user = (User)buttondel.DataContext;
            AC.Users.Remove(user);
            AC.SaveChanges();
            MessageBox.Show("Успешно удалено!", "Communication", MessageBoxButton.OK, MessageBoxImage.Information);
            updatelist();
        }

        private void FullProfile_Click(object sender, RoutedEventArgs e)
        {
            var buttonprof = (Button)sender;
            var user = (User)buttonprof.DataContext;
            NowClass.newprof = user.login;
            NowProfileWindow nowProfileWindow = new NowProfileWindow();
            nowProfileWindow.Show();
            this.Close();
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            var buttonprof = (Button)sender;
            var user = (User)buttonprof.DataContext;
            NowClass.poslanID = user.ID;
            MessageWindow messageWindow = new MessageWindow();
            messageWindow.Show();
            this.Close();
        }

        private void SearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                if (SearchText.Text != "")
                {
                    var List = AC.Users.Where(c => c.login.Contains(SearchText.Text)).ToList();
                    ListProf.ItemsSource = List;
                }
                else {
                    updatelist();
                }
            }
            
        }
    }
}
