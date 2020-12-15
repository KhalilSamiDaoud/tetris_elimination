namespace Tetris_Elimination.Events
{
    /// <summary>The ServerPlayerListEvent class is used to raise events.</summary>
    public class ServerPlayerListEvent
    {
        private int _id;
        private int _status;
        private string _userName;

        /// <summary>Initializes a new instance of the <see cref="ServerPlayerListEvent" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        /// <param name="userName">Name of the user.</param>
        public ServerPlayerListEvent(int id, int status, string userName)
        {
            _id       = id;
            _status   = status;
            _userName = userName;
        }

        /// <summary>Gets the identifier.</summary>
        /// <returns>The player ID.</returns>
        public int GetID()
        {
            return _id;
        }

        /// <summary>Gets the status.</summary>
        /// <returns>The player status.</returns>
        public int GetStatus()
        {
            return _status;
        }

        /// <summary>Gets the name of the user.</summary>
        /// <returns>The player name.</returns>
        public string GetUserName()
        {
            return _userName;
        }
    }
}
