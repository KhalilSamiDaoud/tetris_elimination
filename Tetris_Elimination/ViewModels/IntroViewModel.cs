using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using System.Timers;
using System;

namespace Tetris_Elimination.ViewModels
{
    /// <summary>The IntroViewModel class defines all functions and member variables for the IntroView.</summary>
    public class IntroViewModel : Screen
    {
        private AudioManagerModel audioManager;
        private Timer eventTimer;
        private String _logoURL;
        private String _title;

        /// <summary>Initializes a new instance of the <see cref="IntroViewModel" /> class.</summary>
        public IntroViewModel()
        {
            audioManager = AudioManagerModel.Instance;

            eventTimer          = new Timer();
            eventTimer.Elapsed += new ElapsedEventHandler(Intro);
            eventTimer.Interval = 2000;
            eventTimer.Start();

            Title   = "~ By Khalil Daoud ~";
            LogoURL = LOGO;
        }

        /// <summary>Plays intro sound on the UI thread.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs" /> instance containing the event data.</param>
        private void Intro(object sender, ElapsedEventArgs e)
        {
            this.OnUIThread(() =>
            {
                audioManager.PlaySound(Sound.INTRO);
                eventTimer.Stop();
                eventTimer.Dispose();
            });
        }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        /// <summary>Gets or sets the logo URL.</summary>
        /// <value>The logo URL.</value>
        public String LogoURL
        {
            get { return _logoURL; }
            set
            {
                _logoURL = value;
                NotifyOfPropertyChange(() => LogoURL);
            }
        }
    }
}
