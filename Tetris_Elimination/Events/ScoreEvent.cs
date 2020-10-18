using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Elimination.Events
{
    public class ScoreEvent
    {
        private int _score;

        public ScoreEvent(int score)
        {
            this._score = score;
        }

        public int get()
        {
            return _score;
        }
    }
}
