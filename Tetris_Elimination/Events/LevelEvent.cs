using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Elimination.Events
{
    public class LevelEvent
    {
        private int _level;

        public LevelEvent(int level)
        {
            this._level = level;
        }

        public int get()
        {
            return _level;
        }
    }
}
