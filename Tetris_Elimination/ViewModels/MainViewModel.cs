using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tetris_Elimination.Views;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class MainViewModel : Conductor<Object>
    {
        public MainViewModel()
        {
            ActivateItem(new MenuViewModel(this));
        }

        public void SetNewView(Screens cmd)
        {
            switch (cmd)
            {
                case Screens.MENU:
                    ActivateItem(new MenuViewModel(this));
                    break;
                case Screens.SINGLEPLAYER:
                    ActivateItem(new SinglePlayerViewModel(this));
                    break;
                case Screens.MULTIPLAYER:
                    ActivateItem(new MultiPlayerViewModel(this));
                    break;
                case Screens.SETTINGS:
                    ActivateItem(new SettingsViewModel(this));
                    break;
                default:
                    break;
            }
        }
    }
}
