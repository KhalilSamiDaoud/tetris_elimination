using Caliburn.Micro;
using System;
using System.Timers;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class IntroViewModel : Screen
    {
        private String _title;
        private String _logoURL;
        private Timer eventTimer;
        private AudioManagerModel audioManager;


        public IntroViewModel()
        {
            audioManager = new AudioManagerModel();

            eventTimer = new Timer();
            eventTimer.Elapsed += new ElapsedEventHandler(Intro);
            eventTimer.Interval = 2000;
            eventTimer.Start();

            Title = "- A Game :P -";
            LogoURL = "pack://application:,,,/Assets/Images/Blue_Bottle.png";
        }

        private void Intro(object sender, ElapsedEventArgs e)
        {
            this.OnUIThread(() =>
            {
                audioManager.playSound(Sound.INTRO);
                eventTimer.Stop();
                eventTimer.Dispose();
            });
        }

        public String Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public String LogoURL
        {
            get
            {
                return _logoURL;
            }
            set
            {
                _logoURL = value;
                NotifyOfPropertyChange(() => LogoURL);
            }
        }
    }
}
