using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Models;
using Caliburn.Micro;

namespace Tetris_Elimination.ViewModels
{
    public class MenuViewModel : Screen
    {
        private AudioManagerModel audioManager;
        private MainViewModel mainWindow;
        private string _version;
        private string _userName;
        public MenuViewModel(MainViewModel _mainWindow)
        {
            audioManager  = AudioManagerModel.Instance;
            mainWindow   = _mainWindow;
            Version       = Properties.Settings.Default.Version;
            UserName      = Properties.Settings.Default.Name;

            mainWindow.SetBackground = "pack://application:,,,/Assets/Images/Background.png";
            mainWindow.SetShade      = .25;

            OnUIThread(audioManager.PlayFadeInTheme);
        }
        public void LoadSinglePlayer()
        {
            mainWindow.SetNewView(Screens.SINGLEPLAYER);
        }

        public void LoadMultiPlayer()
        {
            mainWindow.SetNewView(Screens.MULTIPLAYER_MENU);
        }

        public void LoadSettings()
        {
            mainWindow.SetNewView(Screens.SETTINGS);
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