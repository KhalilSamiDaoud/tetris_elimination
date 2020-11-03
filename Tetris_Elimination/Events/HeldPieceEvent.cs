using Tetris_Elimination.Models;

namespace Tetris_Elimination.Events
{
    public class HeldPieceEvent
    {
        private TetreminoModel _heldPiece;

        public HeldPieceEvent(TetreminoModel heldPiece)
        {
            _heldPiece = heldPiece;
        }

        public TetreminoModel Get()
        {
            return _heldPiece;
        }
    }
}
