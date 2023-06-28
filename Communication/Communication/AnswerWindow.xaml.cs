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
    /// Логика взаимодействия для AnswerWindow.xaml
    /// </summary>
    public partial class AnswerWindow : Window
    {
        AppContext AC;
        public AnswerWindow()
        {
            InitializeComponent();
            AC = new AppContext();
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            NowClass.NowAnswer = AnswerText.Text;
            var r = AC.Questions.Where(c => c.ID == NowClass.nowIDQue).FirstOrDefault();
            if (r != null)
            {
                r.Answer = NowClass.NowAnswer;
                AC.SaveChanges();
                NowClass.NowAnswer = "";
            }
            this.Close();
        }
    }
}
