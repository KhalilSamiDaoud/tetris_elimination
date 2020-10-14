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
        MainViewModel mainWindow;
        BoardViewModel gameWindow;
        BoardView gameBoard;
        StatisticsViewModel statistics;
        public SinglePlayerViewModel(MainViewModel _mainWindow)
        {
            //gameBoard = gameWindow.ge
            mainWindow = _mainWindow;
            gameWindow = new BoardViewModel();
            ActivateItem(gameWindow);

        }

    public void LoadMenu()
        {
            mainWindow.SetNewView(Screens.MENU);
        }
    }
}
