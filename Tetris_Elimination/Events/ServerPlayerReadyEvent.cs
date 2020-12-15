namespace Tetris_Elimination.Events
{
    /// <summary>The ServerPlayerReadyEvent class is used to raise events.</summary>
    public class ServerPlayerReadyEvent
    {
        private int _id;
        private int _status;

        /// <summary>Initializes a new instance of the <see cref="ServerPlayerReadyEvent" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="status">The status.</param>
        public ServerPlayerReadyEvent(int id, int status)
        {
            _id     = id;
            _status = status;
        }

        /// <summary>Gets the identifier.</summary>
        /// <returns>The player ID.</returns>
        public int GetID()
        {
            return _id;
        }

        /// <summary>Gets the status.</summary>
        /// <returns>The players status.</returns>
        public int GetStatus()
        {
            return _status;
        }
    }
}
