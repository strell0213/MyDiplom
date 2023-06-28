using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.IO;

namespace Communication.Connect
{
    class Receive
    {
        public Thread T;
        private const int BUFFER_SIZE = 1024;//packet size for TCP 
        private TcpListener Listener;
        private TcpClient Client;
        private MessageWindow window;
        private readonly Dispatcher disp = Dispatcher.CurrentDispatcher;

        public Receive(MessageWindow window)
        {
            this.window = window;
            //initialise the thread used to handle the connections
            //only 1 connection can be handled at one time
            T = new Thread(new ThreadStart(StartConnection));
            T.SetApartmentState(ApartmentState.STA);
            T.Start();
        }

        private void StartConnection()
        {
            //the port number is stored in a configuration file
            ReceiveFileOverTcp(12345);
        }


       


        private void ReceiveFileOverTcp(int portNumber)
        {
            try
            {
                Listener = new TcpListener(IPAddress.Any, portNumber);
                Listener.Start();
                while (true)
                {
                    AcceptClient();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AcceptClient()
        {
            try
            {
                Client = Listener.AcceptTcpClient();
               
                string clientIP = ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString();
                var netstream = Client.GetStream();
                Connection clientConnection = StoredConnections.GetConnectionbyIp(clientIP);
                if (clientConnection == null)//new client
                {
                    
                        ChooseFile(netstream);
                   
                }
                else if (MessageBox.Show("Accept the Incoming File from" + clientConnection.Name, "Connection", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ChooseFile(netstream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /*
         * read 4 bytes(when a Client connects, he sends the name of the file first
           but before sending the name of the file, the size of the name(as string)
           will be sent
         */
        private string ReceiveTitle(NetworkStream netstream)
        {
            var data = new byte[4]; //allocate data for the 4 bytes integer
            if (netstream.Read(data, 0, 4) == 4)
            {
                int restored = BitConverter.ToInt32(data, 0);
                data = new byte[restored]; //allocate space for the name of the file to be received
                if (netstream.Read(data, 0, restored) == restored) //read name(and extension) of file
                {
                    string extension = System.Text.Encoding.UTF8.GetString(data);
                    return extension;
                }
            }
            MessageBox.Show("Something went wrong!", "Warning", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return string.Empty;

        }

        private int ReceiveFileSize(NetworkStream netstream)
        {
            var data = new byte[4];
            if (netstream.Read(data, 0, 4) == 4)
            {
                var fileSize = BitConverter.ToInt32(data, 0);
                return fileSize;
            }
            MessageBox.Show("Something went wrong!", "Warning", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return -1;
        }

        private int ReceiveFile(NetworkStream netstream, string fileName)
        {
            //read size of file to know if everything went right and all the file 
            //was received
            var fileSize = ReceiveFileSize(netstream);
            WriteFile file = new WriteFile(fileName, fileSize);
            if (file.Size > 0)
            {
                
                var data = new byte[BUFFER_SIZE];
                int receivedBytes, howOftenToUpdate = file.Size / 15, updateReceivedBytes = 0, totalReceivedBytes = 0;
                while ((receivedBytes = netstream.Read(data, 0, data.Length)) > 0)
                {
                    file.WriteData(data, receivedBytes);
                    updateReceivedBytes += receivedBytes;
                    if (updateReceivedBytes > howOftenToUpdate)
                    {
                        totalReceivedBytes += updateReceivedBytes;
                        
                        updateReceivedBytes = 0;
                    }
                }
                totalReceivedBytes += updateReceivedBytes;
               
                file.Close();
                if (file.Size == totalReceivedBytes)
                    return 1;
            }
            MessageBox.Show("File was not received. Connection was interrupted!",
                "Warning", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            return -1;
        }


        private void ChooseFile(NetworkStream netstream)
        {
            var extension = "";
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "All files (*.*)|*.*",
                RestoreDirectory = true,
                Title = "Where do you want to save the file?",
                FileName = ReceiveTitle(netstream)
                //DialogSave.InitialDirectory = @"C:/";
            };
            extension = Path.GetExtension(dialog.FileName);

            var fileName = string.Empty;
            if (dialog.ShowDialog() == true)
                fileName = dialog.FileName;

            if (fileName != string.Empty)
            {
                //check if extension was not changed
                if (!fileName.Contains(extension))
                    fileName += extension;
                ReceiveFile(netstream, fileName);
            }
            netstream.Close();
            Client.Close();
        }

        public void CloseConnection()
        {
            Listener.Server.Close();
            this.T.Abort();
        }
    }
}
