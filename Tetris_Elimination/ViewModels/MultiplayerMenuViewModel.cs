using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Networking;
using Tetris_Elimination.Models;
using Tetris_Elimination.Events;
using Caliburn.Micro;
using System;

namespace Tetris_Elimination.ViewModels
{
    public class MultiPlayerMenuViewModel : Conductor<Object>.Collection.AllActive, IHandle<MultiplayerNewGameEvent>
    {
        private ClientManager clientManager;
        private EventAggregatorModel myEvents;
        private MainViewModel mainWindow;
        private ServerViewModel server;
        private string _connectEnabled;
        private string _playingAs;
        private string _inputIP;

        public MultiPlayerMenuViewModel(MainViewModel _mainWindow, bool isReconnect)
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            mainWindow      = _mainWindow;
            clientManager   = ClientManager.Instance;
            server          = new ServerViewModel();
            ConnectEnabled  = "True";
            PlayingAs       = Properties.Settings.Default.Name;
            InputIP         = Properties.Settings.Default.LastConnected;

            mainWindow.SetBackground = BACKGROUND_SETTINGS;
            mainWindow.SetShade      = .5;

            this.Items.Add(server);

            ActivateItem(server);

            if (isReconnect)
            {
                InputIP = Properties.Settings.Default.LastConnected;
                AttemptReconnect();
            }
        }

        public string InputIP 
        {
            get { return _inputIP; }
            set 
            {
                _inputIP = value;
                NotifyOfPropertyChange(() => InputIP);
            } 
        }

        public string PlayingAs
        {
            get { return _playingAs; }
            set
            {
                _playingAs = "Playing as, " + value;
                NotifyOfPropertyChange(() => PlayingAs);

            }
        }

        public string ConnectEnabled
        {
            get { return _connectEnabled; }
            set
            {
                _connectEnabled = value;
                NotifyOfPropertyChange(() => ConnectEnabled);
            }
        }

        private string[] ParseInputIP()
        {
            if (!String.IsNullOrEmpty(InputIP))
            {
                if (InputIP.Contains(":"))
                {
                    return InputIP.Split(':');
                }
            }

            return new string[] { "0", "0"};
        }

        public void AttemptConnect()
        {
            string[] ipAndPort = ParseInputIP();

            clientManager.ConnectToServer(ipAndPort[0], ipAndPort[1]);
            ConnectEnabled = "False";

            myEvents.getAggregator().PublishOnUIThread(new ClientConnectedEvent(ipAndPort));
        }

        public void AttemptReconnect()
        {
            string[] ipAndPort = ParseInputIP();

            PacketSend.ClientReconnect();
            ConnectEnabled = "False";

            myEvents.getAggregator().PublishOnUIThread(new ClientConnectedEvent(ipAndPort));
        }

        public void LoadMenu()
        {
            if (ClientManager.Instance.IsConnected)
            {
                ClientManager.Instance.Disconnect();
            }

            myEvents.getAggregator().Unsubscribe(this);

            mainWindow.SetNewView(Screens.MENU);
        }

        public void LoadMultiPlayer()
        {
            mainWindow.SetNewView(Screens.MULTIPLAYER);
        }

        public void Handle(MultiplayerNewGameEvent message)
        {
            ClientManager.Instance.playersInSession[ClientManager.Instance.MyID].Status = 2;
            myEvents.getAggregator().Unsubscribe(this);

            PacketSend.ClientStatus(2);

            LoadMultiPlayer();
        }
    }
}
