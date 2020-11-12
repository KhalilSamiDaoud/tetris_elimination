using Caliburn.Micro;
using System.Collections.ObjectModel;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Tetris_Elimination.Networking;
using System.Timers;

namespace Tetris_Elimination.ViewModels
{
    public class ServerViewModel : Screen, IHandle<ClientConnectedEvent>, IHandle<ServerInformationEvent>, IHandle<ServerPlayerCountEvent>, IHandle<ServerPlayerReadyEvent>
    {
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

        public ServerViewModel()
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            ServerAddress = "???";
            Status        = "OFFLINE";
            StatusColor   = "Red";
            NumPlayers    = "n/a";
            ReadyEnabled  = "True";

            WindowVisibility = ConstantsModel.HIDDEN;
            ServerVisibility = ConstantsModel.HIDDEN;
            LobbyVisibility  = ConstantsModel.HIDDEN;

            eventTimer           = new Timer();
            eventTimer.Elapsed  += new ElapsedEventHandler(UpdatePlayerList);
            eventTimer.Interval  = 150;
            eventTimer.Start();

            Players = new ObservableCollection<PlayerInstance>();
        }

        private void UpdatePlayerList(object sender, ElapsedEventArgs e)
        {
            OnUIThread(() =>
            {
                Players.Clear();
                Players = new ObservableCollection<PlayerInstance>(ClientManager.Instance.playersInSession.Values);

                NotifyOfPropertyChange(() => Players);
            });
        }

        public ObservableCollection<PlayerInstance> Players { get; private set; }

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

        public void JoinLobby(int lobbyNumber)
        {
            ServerVisibility = ConstantsModel.HIDDEN;
            LobbyVisibility  = ConstantsModel.VISIBLE;
        }

        public void SetReady()
        {
            ClientManager.Instance.playersInSession[ClientManager.Instance.MyID].IsReady = true;
            ReadyEnabled = "False";

            PacketSend.ClientReady(true);
            //myEvents.getAggregator().PublishOnUIThread(new NewGameEvent()); //remove me
        }

        private string[] ParseServerInformation(string msg)
        {
            return msg.Split('-');
        }

        public void Handle(ClientConnectedEvent message)
        {
            ServerAddress    = message.Get()[0] + ":" + message.Get()[1];
            WindowVisibility = ConstantsModel.VISIBLE;
        }

        public void Handle(ServerInformationEvent message)
        {
            string[] statusAndPlayers = ParseServerInformation(message.Get());

            if (statusAndPlayers[0] == "ONLINE")
            {
                StatusColor = "LightGreen";
                ServerVisibility = ConstantsModel.VISIBLE;
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
            ClientManager.Instance.playersInSession[message.GetID()].IsReady = message.GetIsReady();
        }
    }
}
