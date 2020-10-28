using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    class SettingsViewModel : Screen
    {
        MainViewModel main_window;
        public SettingsViewModel(MainViewModel _main_window)
        {
            main_window = _main_window;
        }

        public void SaveAndExit()
        {
            Properties.Settings.Default.Save();
            main_window.SetNewView(Screens.MENU);
        }
    }
}
