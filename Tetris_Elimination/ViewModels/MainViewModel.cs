using Caliburn.Micro;
using System;
using System.Timers;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class MainViewModel : Conductor<Object>
    {
        private Timer eventTimer;
        public MainViewModel()
        {
            ActivateItem(new IntroViewModel());
            eventTimer = new Timer();
            eventTimer.Elapsed += new ElapsedEventHandler(Transition);
            eventTimer.Interval = 5000;
            eventTimer.Start();
        }

        private void Transition(object sender, ElapsedEventArgs e)
        {
            ActivateItem(new MenuViewModel(this));
            eventTimer.Stop();
            eventTimer.Dispose();
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
