using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tetris_Elimination.Models;
using Tetris_Elimination.Views;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class MenuViewModel : Screen
    {
        private string _Version;

        MainViewModel main_window;
        public MenuViewModel(MainViewModel _main_window)
        {
            main_window = _main_window;
            Version = ConstantsModel.cur_version;
        }
        public void LoadSinglePlayer()
        {
            main_window.SetNewView(Screens.SINGLEPLAYER);
        }

        public void LoadMultiPlayer()
        {
            main_window.SetNewView(Screens.MULTIPLAYER);
        }

        public void LoadSettings()
        {
            main_window.SetNewView(Screens.SETTINGS);
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
