using Caliburn.Micro;
using System;
using System.Timers;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class MainViewModel : Conductor<Object>
    {
        private Timer eventTimer;

        public MainViewModel()
        {
            eventTimer           = new Timer();
            eventTimer.Elapsed  += new ElapsedEventHandler(Transition);
            eventTimer.Interval  = 5000;
            eventTimer.Start();
            ActivateItem(new IntroViewModel());
        }

        private void Transition(object sender, ElapsedEventArgs e)
        {
            eventTimer.Stop();
            eventTimer.Dispose();
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
