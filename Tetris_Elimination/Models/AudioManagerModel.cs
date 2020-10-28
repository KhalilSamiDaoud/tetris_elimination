using System;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Models
{
    public class AudioManagerModel
    {
        private MediaPlayer audioLoop;
        private MediaPlayer dropPlayer;
        private MediaPlayer clearedPlayer;
        private MediaPlayer rotatePlayer;
        private MediaPlayer timerPlayer;
        private MediaPlayer timerEndPlayer;
        private MediaPlayer IntroPlayer;
        string audioFilePath;
        string musicFilePath;
        public AudioManagerModel()
        {
            rotatePlayer    = new MediaPlayer();
            dropPlayer      = new MediaPlayer();
            clearedPlayer   = new MediaPlayer();
            timerPlayer     = new MediaPlayer();
            timerEndPlayer  = new MediaPlayer();
            IntroPlayer     = new MediaPlayer();
            audioLoop       = new MediaPlayer();

            //Media Player does not support "pack" as a starting URI, so get the pack URI manually
            audioFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../Assets/Sounds/");
            musicFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../Assets/Music/");
        }

        private void LoopAgain(object sender, EventArgs e)
        {
            audioLoop.Position = TimeSpan.Zero;
            audioLoop.Play();
        }

        public void playSound(Sound sound)
        {
            switch(sound)
            {
                case Sound.ROTATE:
                    rotatePlayer.Open(new Uri(audioFilePath + "Rotate.wav"));
                    rotatePlayer.Play();
                    break;
                case Sound.DROP:
                    dropPlayer.Open(new Uri (audioFilePath + "Drop.wav"));
                    dropPlayer.Play();
                    break;
                case Sound.CLEARED_ROW:
                    clearedPlayer.Open(new Uri(audioFilePath + "ClearedRow.wav"));
                    clearedPlayer.Play();
                    break;
                case Sound.TIMER:
                    timerPlayer.Open(new Uri(audioFilePath + "Timer.wav"));
                    timerPlayer.Play();
                    break;
                case Sound.TIMER_END:
                    timerEndPlayer.Open(new Uri(audioFilePath +  "TimerEnd.wav"));
                    timerEndPlayer.Play();
                    break;
                case Sound.INTRO:
                    IntroPlayer.Open(new Uri(audioFilePath + "Intro.wav"));
                    IntroPlayer.Play();
                    break;
                default:
                    break;
            }
        }

        public void playTheme()
        {
            audioLoop.Open(new Uri(musicFilePath + "Tetris_theme.mp3"));
            audioLoop.Play();
            audioLoop.MediaEnded += new EventHandler(LoopAgain);
        }
    }
}
