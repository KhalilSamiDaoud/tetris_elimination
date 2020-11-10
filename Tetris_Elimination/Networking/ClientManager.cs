using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tetris_Elimination.Networking
{
    public sealed class ClientManager
    {
        public TCP tcp;

        private static int dataBufferSize          = 4096;
        private static ClientManager instance      = null;
        private static readonly object padlock     = new object();

        private delegate void PacketHandler(Packet packet);
        private static Dictionary<int, PacketHandler> packetHandlers;

        private ClientManager()
        {
            dataBufferSize = 4096;
            Port           = 26950;
            tcp            = new TCP();
        }

        public static ClientManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ClientManager();
                    }
                    return instance;
                }
            }
        }

        public string IP { get; private set; }

        public int Port { get; private set; }

        public int MyID { get; set; }

        public void ConnectToServer(string ip, string port)
        {
            InitializeClientData();

            int tempPort;
            Int32.TryParse(port, out tempPort);

            IP = ip;
            Port = tempPort;

            tcp.Connect();
        }

        private void InitializeClientData()
        {
            packetHandlers = new Dictionary<int, PacketHandler>()
                {
                    { (int)ServerPackets.welcome, ClientHandle.Welcome }
                };
        }

        public class TCP
        {
            private TcpClient socket;

            private NetworkStream byteStream;
            private Packet dataIn;
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

            public void SendData(Packet packet)
            {
                try
                {
                    if (socket != null)
                    {
                        byteStream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("In TCP -> SendData: " + ex);
                }
            }

            private void ConnectCallBack(IAsyncResult result)
            {
                socket.EndConnect(result);

                if(!socket.Connected)
                {
                    return;
                }

                byteStream   = socket.GetStream();
                dataIn = new Packet();
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

                    dataIn.Reset(HandleData(receivedData));
                    byteStream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallBack, null);


                }
                catch (Exception e)
                {
                    Console.WriteLine("error: receive callback" + e);
                }
            }

            private bool HandleData(Byte[] receivedData)
            {
                int packetLength = 0;

                dataIn.SetBytes(receivedData);

                if (dataIn.UnreadLength() >= 4)
                {
                    packetLength = dataIn.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }

                while (packetLength > 0 && packetLength <= dataIn.UnreadLength())
                {
                   Byte[] packetBytes = dataIn.ReadBytes(packetLength);

                    using (Packet packet = new Packet(packetBytes))
                    {
                        int packetID = packet.ReadInt();
                        packetHandlers[packetID](packet);
                    }

                    packetLength = 0;

                    if (dataIn.UnreadLength() >= 4)
                    {
                        packetLength = dataIn.ReadInt();
                        if (packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if(packetLength <= 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
 }
