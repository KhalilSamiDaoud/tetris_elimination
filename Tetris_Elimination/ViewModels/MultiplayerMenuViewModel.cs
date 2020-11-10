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

        public MultiPlayerMenuViewModel(MainViewModel _mainWindow)
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            mainWindow    = _mainWindow;
            clientManager = ClientManager.Instance;
            server        = new ServerViewModel();

            mainWindow.SetBackground = "pack://application:,,,/Assets/Images/Background_Settings.png";
            mainWindow.SetShade      = .5;

            this.Items.Add(server);

            ActivateItem(server);
        }

        public string InputIP { get; set; }

        private string[] ParseInputIP()
        {
            return InputIP.Split(':');
        }

        public void AttemptConnect()
        {
            string[] ipAndPort = ParseInputIP();

           clientManager.ConnectToServer(ipAndPort[0], ipAndPort[1]);

            myEvents.getAggregator().PublishOnUIThread(new ClientConnectedEvent());
        }

        public void LoadMenu()
        {
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
