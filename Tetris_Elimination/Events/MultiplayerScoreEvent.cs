namespace Tetris_Elimination.Events
{
    /// <summary>The MultiplayerScoreEvent class is used to raise events.</summary>
    public class MultiplayerScoreEvent
    {
        private int _boardID;
        private int _score;

        /// <summary>Initializes a new instance of the <see cref="MultiplayerScoreEvent" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="score">The score.</param>
        public MultiplayerScoreEvent(int id, int score)
        {
            _boardID = id;
            _score = score;
        }

        /// <summary>Gets the identifier.</summary>
        /// <returns>The players ID.</returns>
        public int GetID()
        {
            return _boardID;
        }

        /// <summary>Gets the score.</summary>
        /// <returns>The players score.</returns>
        public int GetScore()
        {
            return _score;
        }
    }
}
