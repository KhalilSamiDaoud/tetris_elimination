using Caliburn.Micro;
using System;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class MultiPlayerMenuViewModel : Conductor<Object>.Collection.AllActive
    {
        private MainViewModel mainWindow;
        private ServerViewModel server;

        public MultiPlayerMenuViewModel(MainViewModel _mainWindow)
        {
            mainWindow = _mainWindow;
            server     = new ServerViewModel();

            mainWindow.SetBackground = "pack://application:,,,/Assets/Images/Background_Settings.png";
            mainWindow.SetShade      = .5;

            this.Items.Add(server);

            ActivateItem(server);
        }

        public void LoadMenu()
        {
            mainWindow.SetNewView(Screens.MENU);
        }
    }
}
