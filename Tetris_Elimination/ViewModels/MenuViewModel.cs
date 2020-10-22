using Caliburn.Micro;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class MenuViewModel : Screen
    {
        private MainViewModel _main_window;
        private string _Version;
        private AudioManagerModel audioManager;
        public MenuViewModel(MainViewModel main_window)
        {
            _main_window = main_window;
            Version = CUR_VERSION;
            audioManager = new AudioManagerModel();
        }
        public void LoadSinglePlayer()
        {
           _main_window.SetNewView(Screens.SINGLEPLAYER);
        }

        public void LoadMultiPlayer()
        {
            _main_window.SetNewView(Screens.MULTIPLAYER);
        }

        public void LoadSettings()
        {
            _main_window.SetNewView(Screens.SETTINGS);
        }

        public void hoveredOver() // make this work
        {
            audioManager.playSound(Sound.TIMER);

        }
        public string Version
        {
            get
            {
                return _Version;
            }
            set {
                _Version = value;
                NotifyOfPropertyChange(() => Version);
            }
        }
    }
}
