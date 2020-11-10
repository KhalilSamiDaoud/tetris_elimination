using static Tetris_Elimination.Models.ConstantsModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Caliburn.Micro;
using System.Timers;
using System;

namespace Tetris_Elimination.ViewModels
{
    public class MainViewModel : Conductor<Object>
    {
        private Timer eventTimer;
        private String _setBackground;
        private double _shade;

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

        public void SetNewView(Screens newView)
        {
            switch (newView)
            {
                case Screens.MENU:
                    ActivateItem(new MenuViewModel(this));
                    break;
                case Screens.SINGLEPLAYER:
                    ActivateItem(new SinglePlayerViewModel(this));
                    break;
                case Screens.MULTIPLAYER_MENU:
                    ActivateItem(new MultiPlayerMenuViewModel(this));
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

        public String SetBackground
        {
            get { return _setBackground; }
            set
            {
                _setBackground = value;
                NotifyOfPropertyChange(() => SetBackground);
            }
        }

        public double SetShade
        {
            get { return _shade; }
            set
            {
                _shade = value;
                NotifyOfPropertyChange(() => SetShade);
            }
        }
    }
}
