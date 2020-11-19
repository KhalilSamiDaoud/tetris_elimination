using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Networking;
using Tetris_Elimination.Models;
using Tetris_Elimination.Events;
using Caliburn.Micro;
using System;

namespace Tetris_Elimination.ViewModels
{
    public class MultiPlayerMenuViewModel : Conductor<Object>.Collection.AllActive, IHandle<NewGameEvent>
    {
        private ClientManager clientManager;
        private EventAggregatorModel myEvents;
        private MainViewModel mainWindow;
        private ServerViewModel server;
        private string _connectEnabled;
        private string _playingAs;

        public MultiPlayerMenuViewModel(MainViewModel _mainWindow)
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            mainWindow      = _mainWindow;
            clientManager   = ClientManager.Instance;
            server          = new ServerViewModel();
            ConnectEnabled  = "True";
            PlayingAs       = Properties.Settings.Default.Name;

            mainWindow.SetBackground = BACKGROUND_SETTINGS;
            mainWindow.SetShade      = .5;

            this.Items.Add(server);

            ActivateItem(server);
        }

        public string InputIP { get; set; }

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

        public void LoadMenu()
        {
            if (ClientManager.Instance.IsConnected)
            {
                ClientManager.Instance.Disconnect();
            }
            mainWindow.SetNewView(Screens.MENU);
        }

        public void LoadMultiPlayer()
        {
            mainWindow.SetNewView(Screens.MULTIPLAYER);
        }

        public void Handle(NewGameEvent message)
        {
            LoadMultiPlayer();
        }
    }
}
