using System;
using System.Windows.Media;
using Tetris_Elimination.Properties;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Models
{
    public class AudioManagerModel
    {
        private MediaPlayer audioLoop;
        private MediaPlayer dropPlayer;
        private MediaPlayer clearedPlayer;
        private MediaPlayer rotatePlayer;
        public AudioManagerModel()
        {
            rotatePlayer = new MediaPlayer();
            dropPlayer = new MediaPlayer();
            clearedPlayer = new MediaPlayer();
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
                default:
                    break;
            }
        }
    }
}
