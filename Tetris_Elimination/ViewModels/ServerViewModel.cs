using Caliburn.Micro;
using System.Collections.ObjectModel;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Tetris_Elimination.Networking;
using System.Timers;
using System.Windows;

namespace Tetris_Elimination.ViewModels
{
    public class ServerViewModel : Screen, IHandle<ClientConnectedEvent>, IHandle<ServerInformationEvent>, IHandle<ServerPlayerCountEvent>, IHandle<ServerPlayerReadyEvent>
    {
        public class ButtonState
        {
            public Visibility btnVisible { get; set; }
            public bool btnEnabled       { get; set; }

            public ButtonState()
            {
                btnVisible = Visibility.Hidden;
                btnEnabled = false;
            }
        }

        private EventAggregatorModel myEvents;
        private Timer eventTimer;
        private string _serverAddress;
        private string _status;
        private string _numPlayers;
        private string _statusColor;
        private string _windowVisibility;
        private string _serverVisibility;
        private string _lobbyVisibility;
        private string _readyEnabled;
        private string _createEnabled;

        public ObservableCollection<PlayerInstance> Players   { get; private set; }

        public ObservableCollection<LobbyInstance> Lobbies    { get; private set; }

        public ObservableCollection<ButtonState> ButtonStates { get; private set; }

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

        private void UpdatePlayers()
        {
            Players.Clear();
            Players = new ObservableCollection<PlayerInstance>(ClientManager.Instance.playersInSession.Values);

            NotifyOfPropertyChange(() => Players);
        }

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

        public void CreateLobby()
        {
            if (Lobbies.Count < 4)
            {
                PacketSend.ClientLobbyCreate();

                ServerVisibility = ConstantsModel.HIDDEN;
                LobbyVisibility = ConstantsModel.VISIBLE;
            }
        }

        public void JoinLobby(int lobbyindex)
        {
            ServerVisibility = ConstantsModel.HIDDEN;
            LobbyVisibility = ConstantsModel.VISIBLE;

            PacketSend.ClientLobbyJoin(Lobbies[lobbyindex].UID);
        }

        public void SetReady()
        {
            ClientManager.Instance.playersInSession[ClientManager.Instance.MyID].Status = 1;
            ReadyEnabled = "False";

            PacketSend.ClientStatus(1);
        }

        private string[] ParseServerInformation(string msg)
        {
            return msg.Split('-');
        }

        public string ServerAddress
        {
            get { return _serverAddress; }
            set
            {
                _serverAddress = value;
                NotifyOfPropertyChange(() => ServerAddress);
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        public string StatusColor
        {
            get { return _statusColor; }
            set
            {
                _statusColor = value;
                NotifyOfPropertyChange(() => StatusColor);
            }
        }

        public string NumPlayers
        {
            get { return _numPlayers; }
            set
            {
                _numPlayers = value;
                NotifyOfPropertyChange(() => NumPlayers);
            }
        }

        public string WindowVisibility
        {
            get { return _windowVisibility; }
            set
            {
                _windowVisibility = value;
                NotifyOfPropertyChange(() => WindowVisibility);
            }
        }

        public string ServerVisibility
        {
            get { return _serverVisibility; }
            set
            {
                _serverVisibility = value;
                NotifyOfPropertyChange(() => ServerVisibility);
            }
        }

        public string LobbyVisibility
        {
            get { return _lobbyVisibility; }
            set
            {
                _lobbyVisibility = value;
                NotifyOfPropertyChange(() => LobbyVisibility);
            }
        }

        public string ReadyEnabled
        {
            get { return _readyEnabled; }
            set
            {
                _readyEnabled = value;
                NotifyOfPropertyChange(() => ReadyEnabled);
            }
        }

        public string CreateEnabled
        {
            get { return _createEnabled; }
            set
            {
                _createEnabled = value;
                NotifyOfPropertyChange(() => CreateEnabled);
            }
        }

        public void Handle(ClientConnectedEvent message)
        {
            ServerAddress    = message.GetIP()[0] + ":" + message.GetIP()[1];
            WindowVisibility = ConstantsModel.VISIBLE;

            if(message.GetReconnect())
            {
                LobbyVisibility  = ConstantsModel.VISIBLE;
            }
        }

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

        public void Handle(ServerPlayerCountEvent message)
        {
            NumPlayers = message.GetCount();
        }

        public void Handle(ServerPlayerReadyEvent message)
        {
            ClientManager.Instance.playersInSession[message.GetID()].Status = message.GetStatus();
        }
    }
}
