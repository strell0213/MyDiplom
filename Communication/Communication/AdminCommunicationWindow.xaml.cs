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
    /// Логика взаимодействия для AdminCommunicationWindow.xaml
    /// </summary>
    public partial class AdminCommunicationWindow : Window
    {
        AppContext AC;
        public AdminCommunicationWindow()
        {
            InitializeComponent();
            AC = new AppContext();
            try
            {
                var v = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
                if (v.RoleID == 2)
                {
                    TextVIEW.Text = v.login + "\nЭксперт";
                }
                update();
            }
            catch { }


        }
        public void update() {
            try
            {
                QueView.Items.Clear();
                var v = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
                string[] vs = v.questionId.Split(',', ' ');
                int a;
                var t = AC.Users.Where(c => c.questionId.Contains("")).FirstOrDefault(); ;
                foreach (string s in vs)
                {
                    try
                    {
                        a = Convert.ToInt32(s);
                        t = AC.Users.Where(c => c.questionId.Contains(s) && c.RoleID == 1).FirstOrDefault();

                        var w = AC.Questions.Where(c => c.ID == a).FirstOrDefault();
                        if (w.Answer == "")
                        {
                            QueView.Items.Add(w.ID + ".\nВопрос от " + t.login + ": " + w.Questione);
                        }
                        else
                        {
                            QueView.Items.Add(w.ID + ".\nВопрос от " + t.login + ": " + w.Questione + "\nОтвеченно вами");
                        }

                    }
                    catch { }
                    a = 0;
                    t = null;
                }
            }
            catch { }
        }

        private void AllProfileButton_Click(object sender, RoutedEventArgs e)
        {
            AllProfileWindow allProfileWindow = new AllProfileWindow();
            allProfileWindow.Show();
            
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }
        private void QueView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (QueView.SelectedItem.ToString().Contains("Отвеченно вами"))
                {
                    MessageBox.Show("Этот вопрос уже отвечен", "Communication", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    NowClass.NowAnswer = "";
                    NowClass.nowIDQue = 0;

                    int d1;
                    if (QueView.SelectedItem.ToString()[1].ToString() == "." || QueView.SelectedItem.ToString()[1].ToString() == " ")
                    {
                        d1 = Convert.ToInt32(QueView.SelectedItem.ToString()[0].ToString());
                        NowClass.nowIDQue = d1;
                        AnswerWindow answerWindow = new AnswerWindow();
                        answerWindow.Show();
                    }
                    else
                    {
                        d1 = Convert.ToInt32(QueView.SelectedItem.ToString()[0].ToString() + QueView.SelectedItem.ToString()[1].ToString());
                        NowClass.nowIDQue = d1;
                        AnswerWindow answerWindow = new AnswerWindow();
                        answerWindow.Show();
                    }
                    
                }
            }
            catch { MessageBox.Show("Вы ничего не выбрали!", "Communication", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int d1;
                if (QueView.SelectedItem.ToString()[1].ToString() == "." || QueView.SelectedItem.ToString()[1].ToString() == " ")
                {
                    d1 = Convert.ToInt32(QueView.SelectedItem.ToString()[0].ToString());
                }
                else
                {
                    d1 = Convert.ToInt32(QueView.SelectedItem.ToString()[0].ToString() + QueView.SelectedItem.ToString()[1].ToString());
                }
                var r = AC.Questions.Where(c => c.ID == d1).FirstOrDefault();
                if (r != null)
                {
                    AC.Questions.Remove(r);
                    AC.SaveChanges();
                    update();
                    MessageBox.Show("Успешно удалено!", "Communication", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch { MessageBox.Show("Вы ничего не выбрали!", "Communication", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void updateButton_click(object sender, RoutedEventArgs e)
        {
            update();
        }

        // функции для юнит тестов
        public string answertest(string res, int NowNumber, string SelectedItemText) {
            if (SelectedItemText.Contains("Отвеченно вами"))
            {
                return res = "Ошибка";
            }
            else
            {
                if (SelectedItemText[1].ToString() == "." || SelectedItemText[1].ToString() == " ")
                {
                    NowNumber = Convert.ToInt32(SelectedItemText[0].ToString());
                    if (NowNumber >= 0)
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
                    NowNumber = Convert.ToInt32(SelectedItemText[0].ToString() + SelectedItemText[1].ToString());
                    if (NowNumber != 0)
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

        private void Window_Closed(object sender, EventArgs e)
        {


        }

    }
}
