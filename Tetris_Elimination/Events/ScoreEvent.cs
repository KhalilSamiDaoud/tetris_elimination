namespace Tetris_Elimination.Events
{
    /// <summary>The ScoreEvent class is used to raise events.</summary>
    public class ScoreEvent
    {
        private int _score;

        /// <summary>Initializes a new instance of the <see cref="ScoreEvent" /> class.</summary>
        /// <param name="score">The score.</param>
        public ScoreEvent(int score)
        {
            _score = score;
        }

        /// <summary>Gets this instance.</summary>
        /// <returns>The (single player) players score.</returns>
        public int Get()
        {
            return _score;
        }
    }
}
