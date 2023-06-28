using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Communication.Connect
{
    class Send
    {
        private const int BufferSize = 1024;
        private MessageWindow window;
        private readonly Dispatcher disp = Dispatcher.CurrentDispatcher;

        public Send(MessageWindow window)
        {
            this.window = window;
        }



        public void SendTCP(string sendingFilePath, string ip, Int32 portNumber)
        {
            new Thread(() =>
            {

                using (TcpClient client = new TcpClient(ip, portNumber))
                using (NetworkStream netstream = client.GetStream())
                {

                    FileStream fs = new FileStream(sendingFilePath, FileMode.Open, FileAccess.Read);
                    int nrPackets =
                        Convert.ToInt32(Math.Ceiling(Convert.ToDouble(fs.Length) / Convert.ToDouble(BufferSize)));
                    int totalLength = (int)fs.Length;

                    string ext = Path.GetFileName(sendingFilePath);
                    var buffer = BitConverter.GetBytes(ext.Length);
                    netstream.Write(buffer, 0, (int)buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(ext);
                    netstream.Write(buffer, 0, (int)buffer.Length);
                    //send size of file
                    buffer = BitConverter.GetBytes(totalLength);
                    netstream.Write(buffer, 0, (int)buffer.Length);
                    buffer = new byte[BufferSize];
                    int db = 0, howOftenToUpdate = nrPackets / 15;

                    for (int i = 1; i < nrPackets; i++)
                    {
                        fs.Read(buffer, 0, BufferSize);
                        netstream.Write(buffer, 0, BufferSize);

                        db++;
                        if (db > howOftenToUpdate)
                        {
                            db = 0;
                        }
                    }

                    var sizeRemaining = totalLength % BufferSize;//remaining data size to send
                    fs.Read(buffer, 0, sizeRemaining);
                    netstream.Write(buffer, 0, sizeRemaining);
                    fs.Close();

                }
            }).Start();
        }
    }
}
