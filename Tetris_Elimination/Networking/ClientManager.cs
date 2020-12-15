using System.Collections.Generic;
using Tetris_Elimination.Models;
using Tetris_Elimination.Events;
using System.Net.Sockets;
using System.Diagnostics;
using Caliburn.Micro;
using System;

namespace Tetris_Elimination.Networking
{
    /// <summary>
    /// The ClientManager class is used ot handle all network related events and contains the classes used to sending and receiving packets.
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.ServerPlayerListEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.ServerLobbyListEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.ServerPlayerCountEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.ServerDisconnectEvent}" />
    public sealed class ClientManager : Screen, IHandle<ServerPlayerListEvent>, IHandle<ServerLobbyListEvent>, IHandle<ServerPlayerCountEvent>, IHandle<ServerDisconnectEvent>
    {
        public TCP tcp;
        private EventAggregatorModel myEvents;
        public Dictionary<int, PlayerInstance> playersInSession;
        public Dictionary<int, LobbyInstance> LobbiesInSession;

        private static ClientManager instance  = null;
        private static readonly object padlock = new object();

        private delegate void PacketHandler(Packet packet);
        private static Dictionary<int, PacketHandler> packetHandlers;

        /// <summary>Gets the ip.</summary>
        /// <value>The ip.</value>
        public string IP        { get; private set; }
        /// <summary>Gets the port.</summary>
        /// <value>The port.</value>
        public int Port         { get; private set; }
        /// <summary>Gets or sets my identifier.</summary>
        /// <value>My identifier.</value>
        public int MyID         { get; set; }
        /// <summary>Gets or sets a value indicating whether this instance is connected.</summary>
        /// <value>
        ///   <c>true</c> if this instance is connected; otherwise, <c>false</c>.</value>
        public bool IsConnected { get; set; }

        /// <summary>Prevents a default instance of the <see cref="ClientManager" /> class from being created.</summary>
        private ClientManager()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            IsConnected = false;
            Port        = 26950;
            tcp         = new TCP();
        }

        /// <summary>Called when [process exit].</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        static void OnProcessExit(object sender, EventArgs e)
        {
            instance.Disconnect();
        }

        /// <summary>Gets the instance.</summary>
        /// <value>The instance.</value>
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

        /// <summary>Connects to server. Attemps to parse the IP and port before connecting, on successfull
        /// connecty save the IP and port to user settings.</summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public void ConnectToServer(string ip, string port)
        {
            InitializeClientData();

            int tempPort;

            Int32.TryParse(port, out tempPort);

            IP   = ip;
            Port = tempPort;

            tcp.Connect();

            Properties.Settings.Default.LastConnected = IP + ":" + Port;
            Properties.Settings.Default.Save();
        }

        /// <summary>Disconnects this instance. If the socket is not null, close it. Clear the players in session list
        /// and then send disconnect and server information events.</summary>
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

        /// <summary>Initializes the client data. Adds all the delegates to to the packet handler dictionary
        /// and initializes playersInSession and lobbiesInSession.</summary>
        private void InitializeClientData()
        {
            playersInSession = new Dictionary<int, PlayerInstance>();
            LobbiesInSession = new Dictionary<int, LobbyInstance>();

            packetHandlers = new Dictionary<int, PacketHandler>()
                {
                    { (int)ServerPackets.welcome, ClientHandle.Welcome },
                    { (int)ServerPackets.playerCountChange, ClientHandle.PlayerCountChange },
                    { (int)ServerPackets.playerReadyChange, ClientHandle.PlayerReadyChange },
                    { (int)ServerPackets.serverDisconnect, ClientHandle.ServerDisconnect },
                    { (int)ServerPackets.playerGameOver, ClientHandle.PlayerGameOver },
                    { (int)ServerPackets.playerGrids, ClientHandle.PlayerGrids },
                    { (int)ServerPackets.playerScore, ClientHandle.PlayerScore },
                    { (int)ServerPackets.playerList, ClientHandle.PlayerList },
                    { (int)ServerPackets.lobbyList, ClientHandle.LobbyList },
                    { (int)ServerPackets.startGame, ClientHandle.StartGame }
                };
        }

        /// <summary>Handles the ServerPlayerListEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ServerPlayerListEvent message)
        {
            if (!playersInSession.ContainsKey(message.GetID())) {
                playersInSession.Add(message.GetID(), new PlayerInstance(message.GetID(), message.GetUserName(), message.GetStatus()));

                Debug.WriteLine("players added to list");
            }
        }

        /// <summary>Handles the ServerLobbyListEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ServerLobbyListEvent message)
        {
            var lobbies = LobbiesInSession;

            if (!lobbies.ContainsKey(message.GetID())) {
                 lobbies.Add(message.GetID(), new LobbyInstance(message.GetID(), message.GetName(), message.GetFull(), message.GetCount(), message.GetMax()));
            }
            if (message.GetCount() == 0)
            {
                lobbies.Remove(message.GetID());
            }
            else
            {
                lobbies[message.GetID()].IsFull      = message.GetFull();
                lobbies[message.GetID()].PlayerCount = message.GetCount();
            }
        }

        /// <summary>Handles the ServerPlayerCountEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ServerPlayerCountEvent message)
        {
            if (message.GetID() == MyID)
            {
                return;
            }
            if (playersInSession.ContainsKey(message.GetID()))
            {
                Debug.WriteLine("players removed from list");
                playersInSession.Remove(message.GetID());
            }
        }

        /// <summary>Handles the ServerDisconnectEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ServerDisconnectEvent message)
        {
            Disconnect();
        }

        /// <summary>The TCP class is used to initilize tcp connections.</summary>
        public class TCP
        {
            public TcpClient socket;

            private NetworkStream byteStream;
            private Packet dataIn;
            private byte[] receiveBuffer;
            private int dataBufferSize;

            /// <summary>Initializes a new instance of the <see cref="TCP" /> class.</summary>
            public TCP()
            {
                dataBufferSize = 4096;
            }

            /// <summary>Connects this instance.</summary>
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

            /// <summary>Disconnects this instance.</summary>
            public void Disconnect()
            {
                instance.Disconnect();

                receiveBuffer = null;
                byteStream    = null;
                socket        = null;
                dataIn        = null;
            }

            /// <summary>Writes the packet to the bytestream and sends it to the client.</summary>
            /// <param name="packet">The packet to send.</param>
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

            /// <summary>Attemps to verify a clients connection attemp. If the connectedClients list is not full of initialized TCP instances,
            /// then add the client and initialize their TCP instance. If it is full, reject the clients connection.</summary>
            /// <param name="result">The result of the Async operation.</param>
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

            /// <summary>Receives the call back.</summary>
            /// <param name="result">The result.</param>
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

            /// <summary>Puts received bytes in the dataIn array. If the packet length is greater than 4, then there is still unread data.
            /// if it is less than or equal to 4, then the packet has reached its end.</summary>
            /// <param name="receivedData">The received data.</param>
            /// <returns>True if the packet is fully read</returns>
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
