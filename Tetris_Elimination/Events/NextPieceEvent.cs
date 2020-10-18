using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris_Elimination.Models;

namespace Tetris_Elimination.Events
{
    public class NextPieceEvent
    {
        private TetreminoModel _nextPiece;

        public NextPieceEvent(TetreminoModel nextPiece)
        {
            this._nextPiece = nextPiece;
        }

        public TetreminoModel get()
        {
            return _nextPiece;
        }
    }
}
