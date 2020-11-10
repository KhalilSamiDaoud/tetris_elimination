using Caliburn.Micro;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Elimination.ViewModels
{
    public class ServerViewModel : Screen, IHandle<ClientConnectedEvent>
    {
        private EventAggregatorModel myEvents;
        private string _serverAddress;
        private string _status;
        private string _numPlayers;
        private string _statusColor;
        private string _windowVisibility;
        private string _serverVisibility;
        private string _lobbyVisibility;

        public ServerViewModel()
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            ServerAddress = "???";
            Status        = "OFFLINE";
            StatusColor   = "Red";
            NumPlayers    = "n/a";

            WindowVisibility = ConstantsModel.HIDDEN;
            ServerVisibility = ConstantsModel.HIDDEN;
            LobbyVisibility  = ConstantsModel.HIDDEN;
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

        public void JoinLobby(int lobbyNumber)
        {
            ServerVisibility = ConstantsModel.HIDDEN;
            LobbyVisibility  = ConstantsModel.VISIBLE;
        }

        public void SetReady()
        {
            myEvents.getAggregator().PublishOnUIThread(new NewGameEvent());
        }

        public void Handle(ClientConnectedEvent message)
        {
            WindowVisibility = ConstantsModel.VISIBLE;
            ServerVisibility = ConstantsModel.VISIBLE;
        }
    }
}
