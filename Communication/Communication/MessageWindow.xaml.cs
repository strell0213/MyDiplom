using Communication.Connect;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Communication
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        AppContext AC;
        System.Windows.Threading.DispatcherTimer timer;
        public string SendingFilePath = string.Empty;
        private Send send;
        private readonly Connect.Receive receive;
        public MessageWindow()
        {
            InitializeComponent();
            
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timertick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            Connect.StoredConnections.RemoveConnection("ABC");
            AC = new AppContext();

            var w = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
            if (w.RoleID == 1) { 
                BlockButton.Visibility = Visibility.Hidden;
            }

            var r = AC.Users.Where(c => c.ID == NowClass.poslanID).FirstOrDefault();
            MainNickNameLabel.Content = "Диалог с "+r.login;
            send = new Send(this);
            receive = new Connect.Receive(this);

            var r1 = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();

        }

        private void timertick(object sender, EventArgs e)
        {
            updateList();
        }
        
        public void updateList()
        {
            try
            {
                var r1 = AC.Users.Where(c => c.ID == NowClass.poslanID).FirstOrDefault();
                var r2 = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
                var r = AC.Messages.Where(c => (c.nick1 == r1.login && c.Nick2 == r2.ID) || (c.nick1 == r2.login && c.Nick2 == r1.ID)).ToList();
                MessageList.ItemsSource = r;
            }
            catch { 
            }
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Border border = (Border)VisualTreeHelper.GetChild(MessageList, 0);
            ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
            scrollViewer.ScrollToBottom();
        }

        public void AddConnect() {
            //add new connection
            string name = "ABC";
            string ip = "localhost";


            Connection connection = new Connection(name, ip);

            StoredConnections.AddConnection(name, ip);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var w = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
            if (w.RoleID == 1)
            {
                NowClass.poslanID = 0;
                CommunicationWindow communicationWindow = new CommunicationWindow();
                receive.CloseConnection();
                communicationWindow.Show();
                this.Close();
            }
            else { 
                NowClass.poslanID = 0;
                AllProfileWindow allProfileWindow = new AllProfileWindow();
                allProfileWindow.Show();
                receive.CloseConnection();
                this.Close();
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var r = AC.Users.Where(c => c.ID == NowClass.poslanID).FirstOrDefault();
                var r1 = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
                Message message = new Message()
                {
                    title = NowClass.TitleMes,
                    nick1 = r1.login,
                    Nick2 = r.ID,
                    mes = MessageText.Text,
                    imMes = SendingFilePath,
                };
                AC.Messages.Add(message);
                AC.SaveChanges();
                MessageText.Text = "";
                FilesList.Items.Clear();
                SendingFilePath = "";
                ((INotifyCollectionChanged)MessageList.Items).CollectionChanged += Items_CollectionChanged;
            }
            catch {
                this.Close();
                MessageBox.Show("Вы заблокированы!", "Communication");
                Application.Current.Shutdown();
            }
        }


        private void IMButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.Filter = "All Files (*.*)|*.*";
            Dlg.CheckFileExists = true;
            Dlg.Title = "Выберите файл";
            Dlg.InitialDirectory = @"C:\";
            if (Dlg.ShowDialog() == true)
            {
                SendingFilePath = Dlg.FileName;
                FilesList.Items.Add(Dlg.FileName);
            }

           
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var buttonprof = (Button)sender;
                var mess = (Message)buttonprof.DataContext;
                if (mess.imMes == null)
                {
                    MessageBox.Show("Не обнаружен файл", "Warning");
                }
                else
                {
                    AddConnect();

                    var button = (Button)sender;
                    var mes = (Message)button.DataContext;

                    SendingFilePath = mes.imMes;

                    if (SendingFilePath != string.Empty)
                    {
                        send.SendTCP(SendingFilePath, StoredConnections.GetConnection(0).IP, 12345);
                    }
                    else
                    {
                        MessageBox.Show("Выбери файл", "Warning");
                    }
                }
            }
            catch
            {
                this.Close();
                MessageBox.Show("Вы заблокированы!", "Communication");
                Application.Current.Shutdown();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void BlockButton_Click(object sender, RoutedEventArgs e)
        {
            var r = AC.Users.Where(c => c.ID == NowClass.poslanID).FirstOrDefault();
            AC.Users.Remove(r);
            AC.SaveChanges();
            MessageBox.Show("Заблокирован", "Communication");
            var w = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
            if (w.RoleID == 1)
            {
                NowClass.poslanID = 0;
                CommunicationWindow communicationWindow = new CommunicationWindow();
                receive.CloseConnection();
                communicationWindow.Show();
                this.Close();
            }
            else
            {
                NowClass.poslanID = 0;
                AllProfileWindow allProfileWindow = new AllProfileWindow();
                allProfileWindow.Show();
                receive.CloseConnection();
                this.Close();
            }
        }

        private void DownloadButton_Loaded(object sender, RoutedEventArgs e)
        {
            var buttonprof = (Button)sender;
            var mess = (Message)buttonprof.DataContext;
            if (mess.imMes == "")
            {
                buttonprof.Visibility = Visibility.Hidden;
            }
            else { 
                buttonprof.Visibility = Visibility.Visible;
            }
        }

        private void MessageText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (MessageText.Text != "")
                {
                    try
                    {
                        var r = AC.Users.Where(c => c.ID == NowClass.poslanID).FirstOrDefault();
                        var r1 = AC.Users.Where(c => c.login == NowClass.NOW).FirstOrDefault();
                        Message message = new Message()
                        {
                            title = NowClass.TitleMes,
                            nick1 = r1.login,
                            Nick2 = r.ID,
                            mes = MessageText.Text,
                            imMes = SendingFilePath,
                        };
                        AC.Messages.Add(message);
                        AC.SaveChanges();
                        ((INotifyCollectionChanged)MessageList.Items).CollectionChanged += Items_CollectionChanged;
                        MessageText.Text = "";
                        FilesList.Items.Clear();
                        SendingFilePath = "";
                    }
                    catch
                    {
                        this.Close();
                        MessageBox.Show("Вы заблокированы!", "Communication");
                        Application.Current.Shutdown();
                    }
                }
                else
                {
                    
                }
            }
        }
    }
}
