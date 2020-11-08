using System;
using System.Net.Sockets;
using System.Net;

namespace Tetris_Elimination.Models
{
    public sealed class ClientManagerModel
    {
        private TCP tcp;

        private static int dataBufferSize          = 4096;
        private static ClientManagerModel instance = null;
        private static readonly object padlock     = new object();
        

        private ClientManagerModel()
        {
            dataBufferSize = 4096;
            Port           = 26950;
            tcp            = new TCP();
        }

        public static ClientManagerModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ClientManagerModel();
                    }
                    return instance;
                }
            }
        }

        public string IP { get; private set; }

        public int Port { get; private set; }

        public int MyID { get; private set; }

        public void ConnectToServer(string ip, string port)
        {
            int tempPort;
            Int32.TryParse(port, out tempPort);

            IP = ip;
            Port = tempPort;

            tcp.Connect();
        }

        public class TCP
        {
            private TcpClient socket;

            private NetworkStream byteStream;
            private byte[] receiveBuffer;
            private int dataBufferSize;

            public TCP()
            {
                dataBufferSize = 4096;
            }

            public void Connect()
            {
                socket        = new TcpClient
                {
                    ReceiveBufferSize = dataBufferSize,
                    SendBufferSize = dataBufferSize
                };

                receiveBuffer = new byte[dataBufferSize];
                socket.BeginConnect(instance.IP, instance.Port, ConnectCallBack, socket);
            }

            private void ConnectCallBack(IAsyncResult result)
            {
                socket.EndConnect(result);

                if(!socket.Connected)
                {
                    return;
                }

                byteStream = socket.GetStream();
                byteStream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallBack, null);

            }

            private void ReceiveCallBack(IAsyncResult result)
            {
                try
                {
                    int byteLength = byteStream.EndRead(result);
                    if (byteLength <= 0)
                    {
                        return;
                    }

                    byte[] receivedData = new byte[byteLength];
                    Array.Copy(receiveBuffer, receivedData, byteLength);

                    byteStream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallBack, null);


                }
                catch (Exception e)
                {
                    Console.WriteLine("error: receive callback" + e);
                }
            }
        }

    }
 }
