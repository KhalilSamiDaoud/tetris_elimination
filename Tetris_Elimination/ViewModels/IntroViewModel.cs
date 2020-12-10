using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using System.Timers;
using System;

namespace Tetris_Elimination.ViewModels
{
    public class IntroViewModel : Screen
    {
        private AudioManagerModel audioManager;
        private Timer eventTimer;
        private String _logoURL;
        private String _title;

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

        private void Intro(object sender, ElapsedEventArgs e)
        {
            this.OnUIThread(() =>
            {
                audioManager.PlaySound(Sound.INTRO);
                eventTimer.Stop();
                eventTimer.Dispose();
            });
        }

        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

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
