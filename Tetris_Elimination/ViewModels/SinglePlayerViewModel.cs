using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tetris_Elimination.Views;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    class SinglePlayerViewModel : Conductor<Object>
    {
        MainViewModel main_window;
        BoardViewModel game_board;
        public SinglePlayerViewModel(MainViewModel _main_window)
        {
            main_window = _main_window;
            game_board = new BoardViewModel();
            ActivateItem(game_board);

        }

    public void LoadMenu()
        {
            main_window.SetNewView(Screens.MENU);
        }
    }
}
