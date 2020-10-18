using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Elimination.Events
{
    public class GameOverEvent
    {

        private bool _gameOver;
        public GameOverEvent(bool gameOver)
        {
            this._gameOver = gameOver;
        }

        public bool get()
        {
            return _gameOver;
        }
    }
}
