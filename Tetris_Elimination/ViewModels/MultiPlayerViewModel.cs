using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class MultiPlayerViewModel : Screen
    {
        MainViewModel main_window;

        public MultiPlayerViewModel(MainViewModel _main_window)
        {
            main_window = _main_window;
        }

        public void LoadMenu()
        {
            main_window.SetNewView(Screens.MENU);
        }
    }
}
