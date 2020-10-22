using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Elimination.Events
{
    public class TickDownEvent
    {
        private int _counter;

        public TickDownEvent(int counter)
        {
            this._counter = counter;
        }

        public int get()
        {
            return _counter;
        }
    }
}
