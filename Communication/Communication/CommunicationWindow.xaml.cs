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
    /// Логика взаимодействия для CommunicationWindow.xaml
    /// </summary>
    public partial class CommunicationWindow : Window
    {
        AppContext AC;
        public CommunicationWindow()
        {
            InitializeComponent();
            AC = new AppContext();
            
            var v = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
            if (v.RoleID == 1) {
                TextVIEW.Text = v.login + "\nПользователь";
            }
            try
            {
                string[] vs = v.questionId.Split(',', ' ');
                int a;
                foreach (string s in vs)
                {

                    try
                    {
                        a = Convert.ToInt32(s);
                        var w = AC.Questions.Where(c => c.ID == a).FirstOrDefault();
                        QueView.Items.Add(w.ID + "\nВаш вопрос: " + w.Questione + "\nОтвет: \n" + w.Answer);
                        a = 0;
                    }
                    catch { }

                }
            }
            catch { }
            var r = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddQuestion add = new AddQuestion();
            add.Show();
            this.Close();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }

        private void DiaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DiaGrid.Visibility == Visibility.Hidden)
            {
                DiaGrid.Visibility = Visibility.Visible;
                var r = AC.Users.Where(c => c.RoleID == 2).ToList();
                DiaBox.ItemsSource = r;   
            }
            else
            {
                DiaGrid.Visibility = Visibility.Hidden;
            }
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

        private void Window_Closed(object sender, EventArgs e)
        {
            var r = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
            r.OnlineID = 2;
            AC.SaveChanges();
        }
    }
}
