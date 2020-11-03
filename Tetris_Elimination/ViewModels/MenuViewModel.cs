using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Models;
using Caliburn.Micro;

namespace Tetris_Elimination.ViewModels
{
    public class MenuViewModel : Screen
    {
        private AudioManagerModel audioManager;
        private MainViewModel _mainWindow;
        private string _version;
        private string _userName;
        public MenuViewModel(MainViewModel mainWindow)
        {
            audioManager  = AudioManagerModel.Instance;
            _mainWindow   = mainWindow;
            Version       = Properties.Settings.Default.Version;
            UserName      = Properties.Settings.Default.Name;

            OnUIThread(audioManager.PlayFadeInTheme);
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
            get { return _version; }
            set 
            {
                _version = value;
                NotifyOfPropertyChange(() => Version);
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = "Welcome, " + value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
    }
}