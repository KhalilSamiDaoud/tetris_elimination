using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using Tetris_Elimination.Views;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class SinglePlayerViewModel : Conductor<Object>.Collection.AllActive
    {
        private MainViewModel mainWindow;
        private BoardViewModel gameWindow;
        private StatisticsViewModel statistics;
        public SinglePlayerViewModel(MainViewModel _mainWindow)
        {
            mainWindow = _mainWindow;
            statistics = new StatisticsViewModel();
            gameWindow = new BoardViewModel();
            this.Items.Add(gameWindow);
            this.Items.Add(statistics);
            ActivateItem(gameWindow);
            ActivateItem(statistics);
        }

        public void LoadMenu()
        {
            mainWindow.SetNewView(Screens.MENU);
        }
    }
}
