using System.Collections.ObjectModel;
using Tetris_Elimination.Networking;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using System.Windows;
using System.Timers;

namespace Tetris_Elimination.ViewModels
{
    /// <summary>The ServerViewodel is used to display all server information, as well as other players and lobbies on the server.</summary>
    public class ServerViewModel : Screen, IHandle<ClientConnectedEvent>, IHandle<ServerInformationEvent>, IHandle<ServerPlayerCountEvent>, IHandle<ServerPlayerReadyEvent>
    {
        /// <summary>The ButtonState class is used to hold button visibility information.</summary>
        public class ButtonState
        {
            /// <summary>Gets or sets the BTN visible.</summary>
            /// <value>The BTN visible.</value>
            public Visibility btnVisible { get; set; }
            /// <summary>Gets or sets a value indicating whether [BTN enabled].</summary>
            /// <value>
            ///   <c>true</c> if [BTN enabled]; otherwise, <c>false</c>.</value>
            public bool btnEnabled       { get; set; }

            /// <summary>Initializes a new instance of the <see cref="ButtonState" /> class.</summary>
            public ButtonState()
            {
                btnVisible = Visibility.Hidden;
                btnEnabled = false;
            }
        }

        private EventAggregatorModel myEvents;
        private Timer eventTimer;
        private string _windowVisibility;
        private string _serverVisibility;
        private string _lobbyVisibility;
        private string _serverAddress;
        private string _createEnabled;
        private string _readyEnabled;
        private string _statusColor;
        private string _numPlayers;
        private string _status;

        /// <summary>Gets the players.</summary>
        /// <value>The players.</value>
        public ObservableCollection<PlayerInstance> Players   { get; private set; }

        /// <summary>Gets the lobbies.</summary>
        /// <value>The lobbies.</value>
        public ObservableCollection<LobbyInstance> Lobbies    { get; private set; }

        /// <summary>Gets the button states.</summary>
        /// <value>The button states.</value>
        public ObservableCollection<ButtonState> ButtonStates { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="ServerViewModel" /> class.</summary>
        public ServerViewModel()
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            ServerAddress = "???";
            Status        = "OFFLINE";
            StatusColor   = "Red";
            NumPlayers    = "n/a";
            ReadyEnabled  = "True";
            CreateEnabled = "True";

            WindowVisibility = ConstantsModel.HIDDEN;
            ServerVisibility = ConstantsModel.HIDDEN;
            LobbyVisibility  = ConstantsModel.HIDDEN;

            eventTimer           = new Timer();
            eventTimer.Elapsed  += new ElapsedEventHandler(UpdateLists);
            eventTimer.Interval  = 150;
            eventTimer.Start();

            InitializeLists();
        }

        /// <summary>Updates the lists every 150ms.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        private void UpdateLists(object sender, ElapsedEventArgs e)
        {
            OnUIThread(() =>
            {
                if (ClientManager.Instance.playersInSession != null)
                {
                    UpdatePlayers();
                    UpdateLobbies();
                    UpdateButtons();
                }
            });
        }

        /// <summary>Updates the players list.</summary>
        private void UpdatePlayers()
        {
            Players.Clear();
            Players = new ObservableCollection<PlayerInstance>(ClientManager.Instance.playersInSession.Values);

            NotifyOfPropertyChange(() => Players);
        }

        /// <summary>Updates the lobbies list.</summary>
        private void UpdateLobbies()
        {
            Lobbies.Clear();
            Lobbies = new ObservableCollection<LobbyInstance>(ClientManager.Instance.LobbiesInSession.Values);

            if (Lobbies.Count == 4)
            {
                CreateEnabled = "false";
            }
            else
            {
                CreateEnabled = "true";
            }

            NotifyOfPropertyChange(() => Lobbies);
        }

        /// <summary>Updates the buttons list.</summary>
        public void UpdateButtons()
        {
            for (int i = 0; i < 4; i++)
            {
                if (i < Lobbies.Count)
                {
                    ButtonStates[i].btnVisible = Visibility.Visible;
                    if (Lobbies[i].IsFull)
                    {
                        ButtonStates[i].btnEnabled = false;
                    }
                    else
                    {
                        ButtonStates[i].btnEnabled = true;
                    }
                }
                else
                {
                    ButtonStates[i].btnVisible = Visibility.Hidden;
                    ButtonStates[i].btnEnabled = false;
                }

                NotifyOfPropertyChange(() => ButtonStates);
            }
        }

        /// <summary>Initializes the list memebers in the class.</summary>
        private void InitializeLists()
        {
            Players      = new ObservableCollection<PlayerInstance>();
            Lobbies      = new ObservableCollection<LobbyInstance>();
            ButtonStates = new ObservableCollection<ButtonState>();

            for (int i=0; i<4; i++)
            {
                ButtonStates.Add(new ButtonState());
            }
        }

        /// <summary>Creates a lobby and sends a lobby create request to the server.</summary>
        public void CreateLobby()
        {
            if (Lobbies.Count < 4)
            {
                PacketSend.ClientLobbyCreate();

                ServerVisibility = ConstantsModel.HIDDEN;
                LobbyVisibility = ConstantsModel.VISIBLE;
            }
        }

        /// <summary>Joins the lobby and sends a lobby join request to the server</summary>
        /// <param name="lobbyindex">The lobby index.</param>
        public void JoinLobby(int lobbyindex)
        {
            ServerVisibility = ConstantsModel.HIDDEN;
            LobbyVisibility = ConstantsModel.VISIBLE;

            PacketSend.ClientLobbyJoin(Lobbies[lobbyindex].UID);
        }

        /// <summary>Sets the ready.</summary>
        public void SetReady()
        {
            ClientManager.Instance.playersInSession[ClientManager.Instance.MyID].Status = 1;
            ReadyEnabled = "False";

            PacketSend.ClientStatus(1);
        }

        /// <summary>Parses the server information.</summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>The input string parsed</returns>
        private string[] ParseServerInformation(string msg)
        {
            return msg.Split('-');
        }

        /// <summary>Gets or sets the server address.</summary>
        /// <value>The server address.</value>
        public string ServerAddress
        {
            get { return _serverAddress; }
            set
            {
                _serverAddress = value;
                NotifyOfPropertyChange(() => ServerAddress);
            }
        }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        /// <summary>Gets or sets the color of the status.</summary>
        /// <value>The color of the status.</value>
        public string StatusColor
        {
            get { return _statusColor; }
            set
            {
                _statusColor = value;
                NotifyOfPropertyChange(() => StatusColor);
            }
        }

        /// <summary>Gets or sets the number players.</summary>
        /// <value>The number players.</value>
        public string NumPlayers
        {
            get { return _numPlayers; }
            set
            {
                _numPlayers = value;
                NotifyOfPropertyChange(() => NumPlayers);
            }
        }

        /// <summary>Gets or sets the window visibility.</summary>
        /// <value>The window visibility.</value>
        public string WindowVisibility
        {
            get { return _windowVisibility; }
            set
            {
                _windowVisibility = value;
                NotifyOfPropertyChange(() => WindowVisibility);
            }
        }

        /// <summary>Gets or sets the server visibility.</summary>
        /// <value>The server visibility.</value>
        public string ServerVisibility
        {
            get { return _serverVisibility; }
            set
            {
                _serverVisibility = value;
                NotifyOfPropertyChange(() => ServerVisibility);
            }
        }

        /// <summary>Gets or sets the lobby visibility.</summary>
        /// <value>The lobby visibility.</value>
        public string LobbyVisibility
        {
            get { return _lobbyVisibility; }
            set
            {
                _lobbyVisibility = value;
                NotifyOfPropertyChange(() => LobbyVisibility);
            }
        }

        /// <summary>Gets or sets the ready enabled value.</summary>
        /// <value>The ready enabled.</value>
        public string ReadyEnabled
        {
            get { return _readyEnabled; }
            set
            {
                _readyEnabled = value;
                NotifyOfPropertyChange(() => ReadyEnabled);
            }
        }

        /// <summary>Gets or sets the create enabled value.</summary>
        /// <value>The create enabled.</value>
        public string CreateEnabled
        {
            get { return _createEnabled; }
            set
            {
                _createEnabled = value;
                NotifyOfPropertyChange(() => CreateEnabled);
            }
        }

        /// <summary>Handles the ClientConnectedEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ClientConnectedEvent message)
        {
            ServerAddress    = message.GetIP()[0] + ":" + message.GetIP()[1];
            WindowVisibility = ConstantsModel.VISIBLE;

            if(message.GetReconnect())
            {
                LobbyVisibility  = ConstantsModel.VISIBLE;
            }
        }

        /// <summary>Handles the ServerInformationEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ServerInformationEvent message)
        {
            string[] statusAndPlayers = ParseServerInformation(message.Get());

            if (statusAndPlayers[0] == "ONLINE")
            {
                StatusColor = "LightGreen";
                if (LobbyVisibility != ConstantsModel.VISIBLE)
                {
                    ServerVisibility = ConstantsModel.VISIBLE;
                }
            }
            else
            {
                StatusColor = "Red";
                ServerVisibility = ConstantsModel.HIDDEN;
            }

            Status     = statusAndPlayers[0];
            NumPlayers = statusAndPlayers[1];
        }

        /// <summary>Handles the ServerPlayerCountEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ServerPlayerCountEvent message)
        {
            NumPlayers = message.GetCount();
        }

        /// <summary>Handles the ServerPlayerReadyEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ServerPlayerReadyEvent message)
        {
            ClientManager.Instance.playersInSession[message.GetID()].Status = message.GetStatus();
        }
    }
}
