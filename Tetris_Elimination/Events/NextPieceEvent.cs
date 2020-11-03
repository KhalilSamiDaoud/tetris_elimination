using Tetris_Elimination.Models;

namespace Tetris_Elimination.Events
{
    public class NextPieceEvent
    {
        private TetreminoModel _nextPiece;

        public NextPieceEvent(TetreminoModel nextPiece)
        {
            _nextPiece = nextPiece;
        }

        public TetreminoModel Get()
        {
            return _nextPiece;
        }
    }
}
