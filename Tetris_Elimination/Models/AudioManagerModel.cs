using static Tetris_Elimination.Models.ConstantsModel;
using System.Windows.Media;
using System.Reflection;
using Caliburn.Micro;
using System.Timers;
using System.IO;
using System;

namespace Tetris_Elimination.Models
{
    /// <summary>The AudioManagerModel is used my ViewModels that access audio functionality. This is a singleton implementation. 
    /// The audioplayers are eagerly instantiated, and played when needed.</summary>
    public sealed class AudioManagerModel : Screen
    {
        private MediaPlayer audioLoop;
        private MediaPlayer dropPlayer;
        private MediaPlayer clearedPlayer;
        private MediaPlayer rotatePlayer;
        private MediaPlayer timerPlayer;
        private MediaPlayer timerEndPlayer;
        private MediaPlayer introPlayer;
        private string audioFilePath;
        private string musicFilePath;
        private double userEffectsVol;
        private double userMusicVol;
        private Timer eventTimer;

        private static AudioManagerModel instance = null;
        private static readonly object padlock    = new object();

        /// <summary>Prevents a default instance of the <see cref="AudioManagerModel" /> class from being created. Instantiates all private memebers 
        /// and also creates the file path objects needed. This class also sets default user volume levels.</summary>
        private AudioManagerModel()
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

            SetVolume(Properties.Settings.Default.EffectsVol, Properties.Settings.Default.MusicVol);
        }

        /// <summary>Gets the AudioManagerModel instance.</summary>
        /// <value>The instance.</value>
        public static AudioManagerModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AudioManagerModel();
                    }
                    return instance;
                }
            }
        }

        /// <summary>Loops the Tetris Theme repreatedly.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void LoopAgain(object sender, EventArgs e)
        {
            audioLoop.Position = TimeSpan.Zero;
            audioLoop.Play();
        }

        /// <summary>Fades the audio in until the users audio level is reached.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FadeIn(object sender, EventArgs e)
        {
            OnUIThread(() =>
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

        /// <summary>Sets the volume level.</summary>
        /// <param name="effects">The effects volume.</param>
        /// <param name="music">The music volume.</param>
        public void SetVolume(double effects, double music)
        {
            userEffectsVol   = effects;
            userMusicVol     = music;
            audioLoop.Volume = userMusicVol;
        }

        /// <summary>Plays the selected sound at the users volume level.</summary>
        /// <param name="sound">The audio clip.</param>
        public void PlaySound(Sound sound)
        {
            switch(sound)
            {
                case Sound.ROTATE:
                    rotatePlayer.Open(new Uri(audioFilePath + "Rotate.wav"));
                    rotatePlayer.Volume   = userEffectsVol;
                    rotatePlayer.Play();
                    break;
                case Sound.DROP:
                    dropPlayer.Open(new Uri (audioFilePath + "Drop.wav"));
                    dropPlayer.Volume     = userEffectsVol;
                    dropPlayer.Play();
                    break;
                case Sound.CLEARED_ROW:
                    clearedPlayer.Open(new Uri(audioFilePath + "ClearedRow.wav"));
                    clearedPlayer.Volume  = userEffectsVol;
                    clearedPlayer.Play();
                    break;
                case Sound.TIMER:
                    timerPlayer.Open(new Uri(audioFilePath + "Timer.wav"));
                    timerPlayer.Volume    = userEffectsVol;
                    timerPlayer.Play();
                    break;
                case Sound.TIMER_END:
                    timerEndPlayer.Open(new Uri(audioFilePath +  "TimerEnd.wav"));
                    timerEndPlayer.Volume = userEffectsVol;
                    timerEndPlayer.Play();
                    break;
                case Sound.INTRO:
                    introPlayer.Open(new Uri(audioFilePath + "Intro.wav"));
                    introPlayer.Volume    = userEffectsVol;
                    introPlayer.Play();
                    break;
                default:
                    break;
            }
        }

        /// <summary>Plays the Tetris theme at the users volume level, and initalizes the audio-loop action listener.</summary>
        public void PlayTheme()
        {
            audioLoop.Open(new Uri(musicFilePath + "TetrisTheme.mp3"));
            audioLoop.Volume      = userMusicVol;
            audioLoop.MediaEnded += new EventHandler(LoopAgain);
            audioLoop.Play();
        }

        /// <summary>Plays the Tetris theme with a fade in effect at the users volume level, and initalizes the audio-loop action listener.</summary>
        public void PlayFadeInTheme()
        {
            audioLoop.Open(new Uri(musicFilePath + "TetrisTheme.mp3"));
            audioLoop.Volume      = 0;
            audioLoop.MediaEnded += new EventHandler(LoopAgain);
            audioLoop.Play();

            eventTimer            = new Timer();
            eventTimer.Elapsed   += new ElapsedEventHandler(FadeIn);
            eventTimer.Interval   = 20;
            eventTimer.Start();
        }

        /// <summary>Pauses the Tetris theme.</summary>
        public void PauseTheme()
        {
            audioLoop.Pause();
        }

        /// <summary>Unpauses the Tetris theme.</summary>
        public void UnpauseTheme()
        {
            audioLoop.Play();
        }

    }
}
