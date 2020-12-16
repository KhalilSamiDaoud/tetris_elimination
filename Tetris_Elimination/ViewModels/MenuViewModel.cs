using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Models;
using Caliburn.Micro;

namespace Tetris_Elimination.ViewModels
{
    /// <summary>The MenuViewModel class is used to select between single player, multi player, and settings.</summary>
    public class MenuViewModel : Screen
    {
        private AudioManagerModel audioManager;
        private MainViewModel mainWindow;
        private string _version;
        private string _userName;

        /// <summary>Initializes a new instance of the <see cref="MenuViewModel" /> class.</summary>
        /// <param name="_mainWindow">The main window.</param>
        public MenuViewModel(MainViewModel _mainWindow)
        {
            audioManager  = AudioManagerModel.Instance;
            mainWindow    = _mainWindow;
            Version       = Properties.Settings.Default.Version;
            UserName      = Properties.Settings.Default.Name;

            mainWindow.SetBackground = BACKGROUND;
            mainWindow.SetShade      = .25;

            OnUIThread(audioManager.PlayFadeInTheme);
        }

        /// <summary>Loads the single player screen.</summary>
        public void LoadSinglePlayer()
        {
            mainWindow.SetNewView(Screens.SINGLEPLAYER);
        }

        /// <summary>Loads the multi player screen.</summary>
        public void LoadMultiPlayer()
        {
            mainWindow.SetNewView(Screens.MULTIPLAYER_MENU);
        }

        /// <summary>Loads the settings screen.</summary>
        public void LoadSettings()
        {
            mainWindow.SetNewView(Screens.SETTINGS);
        }

        /// <summary>Gets or sets the version.</summary>
        /// <value>The version.</value>
        public string Version
        {
            get { return _version; }
            set 
            {
                _version = value;
                NotifyOfPropertyChange(() => Version);
            }
        }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
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