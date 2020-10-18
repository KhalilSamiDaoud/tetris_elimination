using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris_Elimination.Models;

namespace Tetris_Elimination.Events
{
    public class HeldPieceEvent
    {
        private TetreminoModel _heldPiece;

        public HeldPieceEvent(TetreminoModel heldPeice)
        {
            this._heldPiece = heldPeice;
        }

        public TetreminoModel get()
        {
            return _heldPiece;
        }
    }
}
