using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using System;

namespace Tetris_Elimination.ViewModels
{
    public class MultiPlayerMenuViewModel : Conductor<Object>.Collection.AllActive
    {
        private ClientManagerModel clientManager;
        private MainViewModel mainWindow;
        private ServerViewModel server;

        public MultiPlayerMenuViewModel(MainViewModel _mainWindow)
        {
            mainWindow    = _mainWindow;
            clientManager = ClientManagerModel.Instance;
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
        }

        public void LoadMenu()
        {
            mainWindow.SetNewView(Screens.MENU);
        }
    }
}
