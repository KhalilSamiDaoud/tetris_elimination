using System;
using System.IO;
using System.Reflection;
using System.Timers;
using System.Windows.Media;
using Caliburn.Micro;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Models
{
    public class AudioManagerModel : Conductor<Object>
    {
        private MediaPlayer audioLoop;
        private MediaPlayer dropPlayer;
        private MediaPlayer clearedPlayer;
        private MediaPlayer rotatePlayer;
        private MediaPlayer timerPlayer;
        private MediaPlayer timerEndPlayer;
        private MediaPlayer introPlayer;
        string audioFilePath;
        string musicFilePath;
        double userSoundVol;
        double userMusicVol;
        Timer eventTimer;

        public AudioManagerModel()
        {
            rotatePlayer    = new MediaPlayer();
            dropPlayer      = new MediaPlayer();
            clearedPlayer   = new MediaPlayer();
            timerPlayer     = new MediaPlayer();
            timerEndPlayer  = new MediaPlayer();
            introPlayer     = new MediaPlayer();
            audioLoop       = new MediaPlayer();

            //Media Player does not support "pack" as a starting URI, so get the pack URI manually
            audioFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../Assets/Sounds/");
            musicFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../Assets/Music/");

            setVolume();
        }

        private void LoopAgain(object sender, EventArgs e)
        {
            audioLoop.Position = TimeSpan.Zero;
            audioLoop.Play();
        }

        private void FadeIn(object sender, EventArgs e)
        {
            this.OnUIThread(() =>
            {
                if (audioLoop.Volume >= userMusicVol)
                {
                    eventTimer.Stop();
                    eventTimer.Dispose();
                }
                else if (userMusicVol >= 0.2)
                {
                    audioLoop.Volume += 0.005;
                }
                else
                {
                    audioLoop.Volume += 0.001;
                }
            });
        }

        public void setVolume()
        {
            userSoundVol = Properties.Settings.Default.EffectsVol;
            userMusicVol = Properties.Settings.Default.MusicVol;
        }

        public void playSound(Sound sound)
        {
            switch(sound)
            {
                case Sound.ROTATE:
                    rotatePlayer.Open(new Uri(audioFilePath + "Rotate.wav"));
                    rotatePlayer.Volume = userSoundVol;
                    rotatePlayer.Play();
                    break;
                case Sound.DROP:
                    dropPlayer.Open(new Uri (audioFilePath + "Drop.wav"));
                    dropPlayer.Volume = userSoundVol;
                    dropPlayer.Play();
                    break;
                case Sound.CLEARED_ROW:
                    clearedPlayer.Open(new Uri(audioFilePath + "ClearedRow.wav"));
                    clearedPlayer.Volume = userSoundVol;
                    clearedPlayer.Play();
                    break;
                case Sound.TIMER:
                    timerPlayer.Open(new Uri(audioFilePath + "Timer.wav"));
                    timerPlayer.Volume = userSoundVol;
                    timerPlayer.Play();
                    break;
                case Sound.TIMER_END:
                    timerEndPlayer.Open(new Uri(audioFilePath +  "TimerEnd.wav"));
                    timerEndPlayer.Volume = userSoundVol;
                    timerEndPlayer.Play();
                    break;
                case Sound.INTRO:
                    introPlayer.Open(new Uri(audioFilePath + "Intro.wav"));
                    introPlayer.Volume = userSoundVol;
                    introPlayer.Play();
                    break;
                default:
                    break;
            }
        }

        public void playTheme()
        {
            audioLoop.Open(new Uri(musicFilePath + "Tetris_theme.mp3"));
            audioLoop.Volume      = 0;
            audioLoop.Play();
            audioLoop.MediaEnded += new EventHandler(LoopAgain);

            eventTimer            = new Timer();
            eventTimer.Elapsed   += new ElapsedEventHandler(FadeIn);
            eventTimer.Interval   = 20;
            eventTimer.Start();
        }
    }
}
