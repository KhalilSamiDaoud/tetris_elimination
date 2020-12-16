using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Networking;
using Tetris_Elimination.Models;
using Tetris_Elimination.Events;
using Caliburn.Micro;
using System;

namespace Tetris_Elimination.ViewModels
{
    /// <summary>The MultiPlayerMenuViewModel class is used by the user to connect to a server, and displays the server browser window.</summary>
    /// <seealso cref="Caliburn.Micro.Conductor{System.Object}.Collection.AllActive" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.MultiplayerNewGameEvent}" />
    public class MultiPlayerMenuViewModel : Conductor<Object>.Collection.AllActive, IHandle<MultiplayerNewGameEvent>
    {
        private ClientManager clientManager;
        private EventAggregatorModel myEvents;
        private MainViewModel mainWindow;
        private ServerViewModel server;
        private string _connectEnabled;
        private string _playingAs;
        private string _inputIP;

        /// <summary>Initializes a new instance of the <see cref="MultiPlayerMenuViewModel" /> class.</summary>
        /// <param name="_mainWindow">The main window.</param>
        /// <param name="isReconnect">if set to <c>true</c> [is reconnect].</param>
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

        /// <summary>Gets or sets the input ip.</summary>
        /// <value>The input ip.</value>
        public string InputIP 
        {
            get { return _inputIP; }
            set 
            {
                _inputIP = value;
                NotifyOfPropertyChange(() => InputIP);
            } 
        }

        /// <summary>Gets or sets the playing as string.</summary>
        /// <value>The 'playing as' string.</value>
        public string PlayingAs
        {
            get { return _playingAs; }
            set
            {
                _playingAs = "Playing as, " + value;
                NotifyOfPropertyChange(() => PlayingAs);

            }
        }

        /// <summary>Gets or sets the connect enabled string.</summary>
        /// <value>The connect enabled.</value>
        public string ConnectEnabled
        {
            get { return _connectEnabled; }
            set
            {
                _connectEnabled = value;
                NotifyOfPropertyChange(() => ConnectEnabled);
            }
        }

        /// <summary>Parses the input ip.</summary>
        /// <returns>The input IP as IP and Port</returns>
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

        /// <summary>Attempts to connect to the provided address.</summary>
        public void AttemptConnect()
        {
            string[] ipAndPort = ParseInputIP();

            clientManager.ConnectToServer(ipAndPort[0], ipAndPort[1]);
            ConnectEnabled = "False";

            myEvents.getAggregator().PublishOnUIThread(new ClientConnectedEvent(ipAndPort, false));
        }

        /// <summary>Attempts to reconnect to the previous address.</summary>
        public void AttemptReconnect()
        {
            string[] ipAndPort = ParseInputIP();

            PacketSend.ClientReconnect();
            ConnectEnabled = "False";

            myEvents.getAggregator().PublishOnUIThread(new ClientConnectedEvent(ipAndPort, true));
        }

        /// <summary>Loads the menu screen.</summary>
        public void LoadMenu()
        {
            if (ClientManager.Instance.IsConnected)
            {
                ClientManager.Instance.Disconnect();
            }

            myEvents.getAggregator().Unsubscribe(this);

            mainWindow.SetNewView(Screens.MENU);
        }

        /// <summary>Loads the multiplayer screen.</summary>
        public void LoadMultiPlayer()
        {
            mainWindow.SetNewView(Screens.MULTIPLAYER);
        }

        /// <summary>Handles the MultiplayerNewGameEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(MultiplayerNewGameEvent message)
        {
            ClientManager.Instance.playersInSession[ClientManager.Instance.MyID].Status = 2;
            myEvents.getAggregator().Unsubscribe(this);

            PacketSend.ClientStatus(2);

            LoadMultiPlayer();
        }
    }
}
