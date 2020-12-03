using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Diagnostics;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using Tetris_Elimination.Events;

namespace Tetris_Elimination.Networking
{
    public sealed class ClientManager : Screen, IHandle<ServerPlayerListEvent>, IHandle<ServerPlayerCountEvent>, IHandle<ServerDisconnectEvent>
    {
        public TCP tcp;
        private EventAggregatorModel myEvents;
        public Dictionary<int, PlayerInstance> playersInSession;

        private static ClientManager instance  = null;
        private static readonly object padlock = new object();

        private delegate void PacketHandler(Packet packet);
        private static Dictionary<int, PacketHandler> packetHandlers;

        public string IP        { get; private set; }
        public int Port         { get; private set; }
        public int MyID         { get; set; }
        public bool IsConnected { get; set; }

        private ClientManager()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            IsConnected = false;
            Port        = 26950;
            tcp         = new TCP();
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            instance.Disconnect();
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

        public void ConnectToServer(string ip, string port)
        {
            InitializeClientData();

            int tempPort;

            Int32.TryParse(port, out tempPort);

            IP = ip;
            Port = tempPort;

            tcp.Connect();

            Properties.Settings.Default.LastConnected = IP + ":" + Port;
            Properties.Settings.Default.Save();
        }

        public void Disconnect()
        {
            IsConnected = false;

            if (tcp.socket != null)
            {
                tcp.socket.Close();
            }

            playersInSession.Clear();
            myEvents.getAggregator().PublishOnUIThread(new DisconnectEvent());
            myEvents.getAggregator().PublishOnUIThread(new ServerInformationEvent("OFFLINE-n/a"));
        }

        private void InitializeClientData()
        {
            playersInSession = new Dictionary<int, PlayerInstance>();

            packetHandlers = new Dictionary<int, PacketHandler>()
                {
                    { (int)ServerPackets.welcome, ClientHandle.Welcome },
                    { (int)ServerPackets.playerCountChange, ClientHandle.PlayerCountChange },
                    { (int)ServerPackets.playerReadyChange, ClientHandle.PlayerReadyChange },
                    { (int)ServerPackets.serverDisconnect, ClientHandle.ServerDisconnect },
                    { (int)ServerPackets.playerListToOne, ClientHandle.PlayerListToOne },
                    { (int)ServerPackets.playerListToAll, ClientHandle.PlayerListToAll },
                    { (int)ServerPackets.playerGameOver, ClientHandle.PlayerGameOver },
                    { (int)ServerPackets.playerGrids, ClientHandle.PlayerGrids },
                    { (int)ServerPackets.playerScore, ClientHandle.PlayerScore },
                    { (int)ServerPackets.startGame, ClientHandle.StartGame }
                };
        }

        public void Handle(ServerPlayerListEvent message)
        {
            if (!playersInSession.ContainsKey(message.GetID())) {
                playersInSession.Add(message.GetID(), new PlayerInstance(message.GetID(), message.GetUserName(), message.GetStatus()));
            }
        }

        public void Handle(ServerPlayerCountEvent message)
        {
            if (message.GetID() == MyID)
            {
                return;
            }
            if (playersInSession.ContainsKey(message.GetID()))
            {
                playersInSession.Remove(message.GetID());
            }
        }

        public void Handle(ServerDisconnectEvent message)
        {
            Disconnect();
        }

        public class TCP
        {
            public TcpClient socket;

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
                socket = new TcpClient
                {
                    ReceiveBufferSize = dataBufferSize,
                    SendBufferSize = dataBufferSize
                };

                receiveBuffer = new byte[dataBufferSize];

                try
                {
                    socket.BeginConnect(instance.IP, instance.Port, ConnectCallBack, socket);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Bad Address: " + ex);
                }
            }

            public void Disconnect()
            {
                instance.Disconnect();

                receiveBuffer = null;
                byteStream    = null;
                socket        = null;
                dataIn        = null;
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
                try
                {
                    socket.EndConnect(result);

                    if (!socket.Connected)
                    {
                        return;
                    }

                    byteStream = socket.GetStream();
                    dataIn = new Packet();
                    byteStream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallBack, null);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Server is not online or IP is not valid " + ex);
                }
            }

            private void ReceiveCallBack(IAsyncResult result)
            {
                try
                {
                    int byteLength = byteStream.EndRead(result);
                    if (byteLength <= 0)
                    {
                        instance.Disconnect();
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
                    Disconnect();
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

                    Execute.OnUIThread(() =>
                  {
                      using (Packet packet = new Packet(packetBytes))
                      {
                          int packetID = packet.ReadInt();
                          packetHandlers[packetID](packet);
                      }
                  });

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
