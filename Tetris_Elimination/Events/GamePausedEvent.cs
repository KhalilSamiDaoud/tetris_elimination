using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Elimination.Events
{
    public class GamePausedEvent
    {
        private bool _paused;

        public GamePausedEvent(bool paused)
        {
            this._paused = paused;
        }

        public bool get()
        {
            return _paused;
        }
    }
}
