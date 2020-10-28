using Caliburn.Micro;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class MenuViewModel : Screen
    {
        private MainViewModel _mainWindow;
        private string _version;
        private string _userName;
        public MenuViewModel(MainViewModel mainWindow)
        {
            _mainWindow   = mainWindow;
            Version       = Properties.Settings.Default.Version;
            UserName      = Properties.Settings.Default.Name;
        }
        public void LoadSinglePlayer()
        {
            _mainWindow.SetNewView(Screens.SINGLEPLAYER);
        }

        public void LoadMultiPlayer()
        {
            _mainWindow.SetNewView(Screens.MULTIPLAYER);
        }

        public void LoadSettings()
        {
            _mainWindow.SetNewView(Screens.SETTINGS);
        }

        public string Version
        {
            get
            {
                return _version;
            }
            set 
            {
                _version = value;
                NotifyOfPropertyChange(() => Version);
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = "Welcome, " + value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
    }
}
