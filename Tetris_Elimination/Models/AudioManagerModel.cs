using System;
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
        public AudioManagerModel()
        {
            rotatePlayer = new MediaPlayer();
            dropPlayer = new MediaPlayer();
            clearedPlayer = new MediaPlayer();
            timerPlayer = new MediaPlayer();
            timerEndPlayer = new MediaPlayer();
        }

        public void playSound(Sound sound)
        {
            switch(sound)
            {
                case Sound.ROTATE:
                    rotatePlayer.Open(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Sounds/Rotate.wav", UriKind.Absolute));
                    rotatePlayer.Play();
                    break;
                case Sound.DROP:
                    dropPlayer.Open(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Sounds/Drop.wav", UriKind.Absolute));
                    dropPlayer.Play();
                    break;
                case Sound.CLEARED_ROW:
                    clearedPlayer.Open(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Sounds/ClearedRow.wav", UriKind.Absolute));
                    clearedPlayer.Play();
                    break;
                case Sound.TIMER:
                    timerPlayer.Open(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Sounds/Timer.wav", UriKind.Absolute));
                    timerPlayer.Play();
                    break;
                case Sound.TIMER_END:
                    timerEndPlayer.Open(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Sounds/TimerEnd.wav", UriKind.Absolute));
                    timerEndPlayer.Play();
                    break;
                default:
                    break;
            }
        }
    }
}
