namespace Tetris_Elimination.Events
{
    /// <summary>The MultiplayerGameOverEvent class is used to raise events.</summary>
    public class MultiplayerGameOverEvent
    {
        private int _boardID;
        private bool _gameOver;

        /// <summary>Initializes a new instance of the <see cref="MultiplayerGameOverEvent" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="gameOver">if set to <c>true</c> [game over].</param>
        public MultiplayerGameOverEvent(int id, bool gameOver)
        {
            _boardID = id;
            _gameOver = gameOver;
        }

        /// <summary>Gets the identifier.</summary>
        /// <returns>The players ID who 'game overed'.</returns>
        public int GetID()
        {
            return _boardID;
        }

        /// <summary>Gets the game over.</summary>
        /// <returns>If its a 'game over'.</returns>
        public bool GetGameOver()
        {
            return _gameOver;
        }
    }
}
