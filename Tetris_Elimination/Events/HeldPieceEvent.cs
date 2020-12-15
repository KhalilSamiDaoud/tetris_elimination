using Tetris_Elimination.Models;

namespace Tetris_Elimination.Events
{
    /// <summary>The HeldPieceEvent class is used to raise events.</summary>
    public class HeldPieceEvent
    {
        private TetreminoModel _heldPiece;

        /// <summary>Initializes a new instance of the <see cref="HeldPieceEvent" /> class.</summary>
        /// <param name="heldPiece">The held piece type.</param>
        public HeldPieceEvent(TetreminoModel heldPiece)
        {
            _heldPiece = heldPiece;
        }

        /// <summary>Gets this instance.</summary>
        /// <returns>The held piece type.</returns>
        public TetreminoModel Get()
        {
            return _heldPiece;
        }
    }
}
