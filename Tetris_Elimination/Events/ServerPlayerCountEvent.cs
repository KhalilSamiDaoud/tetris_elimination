namespace Tetris_Elimination.Events
{
    /// <summary>The ServerPlayerCountEvent class is used to raise events.</summary>
    public class ServerPlayerCountEvent
    {
        private string _playerCount;
        private int _id;

        /// <summary>Initializes a new instance of the <see cref="ServerPlayerCountEvent" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="playerCount">The ID of the player who left or joined.</param>
        public ServerPlayerCountEvent(int id, string playerCount)
        {
            _id          = id;
            _playerCount = playerCount;
        }

        /// <summary>Gets the count.</summary>
        /// <returns>Gets the amount of players in the server.</returns>
        public string GetCount()
        {
            return _playerCount;
        }

        /// <summary>Gets the identifier.</summary>
        /// <returns>The ID of the player who left or joined.</returns>
        public int GetID()
        {
            return _id;
        }
    }
}
