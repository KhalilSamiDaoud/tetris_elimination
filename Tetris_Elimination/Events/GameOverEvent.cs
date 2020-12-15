namespace Tetris_Elimination.Events
{
    /// <summary>The GameOverClass is used to raise events.</summary>
    public class GameOverEvent
    {
        private bool _gameOver;

        /// <summary>Initializes a new instance of the <see cref="GameOverEvent" /> class.</summary>
        /// <param name="gameOver">if set to <c>true</c> [game over].</param>
        public GameOverEvent(bool gameOver)
        {
            _gameOver = gameOver;
        }

        /// <summary>Gets this instance.</summary>
        /// <returns>If its a 'gameover'.</returns>
        public bool Get()
        {
            return _gameOver;
        }
    }
}
