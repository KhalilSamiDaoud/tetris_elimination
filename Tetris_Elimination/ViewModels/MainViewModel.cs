using static Tetris_Elimination.Models.ConstantsModel;
using Caliburn.Micro;
using System.Timers;
using System;

namespace Tetris_Elimination.ViewModels
{
    /// <summary>The MainViewModel is used to conduct other screens and play the intro.</summary>
    /// <seealso cref="Caliburn.Micro.Conductor{System.Object}" />
    public class MainViewModel : Conductor<Object>
    {
        private Timer eventTimer;
        private String _setBackground;
        private double _shade;

        /// <summary>Initializes a new instance of the <see cref="MainViewModel" /> class.</summary>
        public MainViewModel()
        {
            eventTimer           = new Timer();
            eventTimer.Elapsed  += new ElapsedEventHandler(Transition);
            eventTimer.Interval  = 5000;
            eventTimer.Start();
            ActivateItem(new IntroViewModel());
        }

        /// <summary>Deactivates the IntroViewModel and transitions to the MenuViewModel.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        private void Transition(object sender, ElapsedEventArgs e)
        {
            eventTimer.Stop();
            eventTimer.Dispose();
            ActivateItem(new MenuViewModel(this));
        }

        /// <summary>Sets the new view based on the enum provided. Deactivates other screens.</summary>
        /// <param name="newView">The new view.</param>
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
                    ActivateItem(new MultiPlayerMenuViewModel(this, false));
                    break;
                case Screens.MULTIPLAYER_MENU_RC:
                    ActivateItem(new MultiPlayerMenuViewModel(this, true));
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

        /// <summary>Gets or sets the set background.</summary>
        /// <value>The set background.</value>
        public String SetBackground
        {
            get { return _setBackground; }
            set
            {
                _setBackground = value;
                NotifyOfPropertyChange(() => SetBackground);
            }
        }

        /// <summary>Gets or sets the set shade.</summary>
        /// <value>The set shade.</value>
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
