﻿using static Tetris_Elimination.Models.ConstantsModel;
using System.Windows.Media;
using System.Reflection;
using Caliburn.Micro;
using System.Timers;
using System.IO;
using System;


namespace Tetris_Elimination.Models
{
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

        private void LoopAgain(object sender, EventArgs e)
        {
            audioLoop.Position = TimeSpan.Zero;
            audioLoop.Play();
        }

        private void FadeIn(object sender, EventArgs e)
        {
            //changed this from run on UI thread
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

        public void SetVolume(double effects, double music)
        {
            userEffectsVol   = effects;
            userMusicVol     = music;
            audioLoop.Volume = userMusicVol;
        }

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

        public void PlayTheme()
        {
            audioLoop.Open(new Uri(musicFilePath + "TetrisTheme.mp3"));
            audioLoop.Volume      = userMusicVol;
            audioLoop.MediaEnded += new EventHandler(LoopAgain);
            audioLoop.Play();
        }

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

        public void PauseTheme()
        {
            audioLoop.Pause();
        }

        public void UnpauseTheme()
        {
            audioLoop.Play();
        }

    }
}
