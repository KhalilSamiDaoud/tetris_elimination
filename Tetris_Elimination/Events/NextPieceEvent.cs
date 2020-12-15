using Tetris_Elimination.Models;

namespace Tetris_Elimination.Events
{
    /// <summary>The NextPieceEvent is used to raise events.</summary>
    public class NextPieceEvent
    {
        private TetreminoModel _nextPiece;

        /// <summary>Initializes a new instance of the <see cref="NextPieceEvent" /> class.</summary>
        /// <param name="nextPiece">The next piece.</param>
        public NextPieceEvent(TetreminoModel nextPiece)
        {
            _nextPiece = nextPiece;
        }

        /// <summary>Gets this instance.</summary>
        /// <returns>Gets the next piece type.</returns>
        public TetreminoModel Get()
        {
            return _nextPiece;
        }
    }
}
