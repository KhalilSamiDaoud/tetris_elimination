namespace Tetris_Elimination.Events
{
    /// <summary>The ServerLobbyListEvent class is used to raise events.</summary>
    public class ServerLobbyListEvent
    {
        private string _name;
        private bool _full;
        private int _count;
        private int _id;
        private int _max;

        /// <summary>Initializes a new instance of the <see cref="ServerLobbyListEvent" /> class.</summary>
        /// <param name="id">The lobby identifier.</param>
        /// <param name="name">The lobby name.</param>
        /// <param name="count">The lobby player count.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <param name="max">The maximum amount of allowed players.</param>
        public ServerLobbyListEvent(int id, string name, int count, bool full, int max)
        {
            _name  = name;
            _count = count;
            _full  = full;
            _id    = id;
            _max   = max;
        }

        /// <summary>Gets the identifier.</summary>
        /// <returns>The lobby ID.</returns>
        public int GetID()
        {
            return _id;
        }

        /// <summary>Gets the name.</summary>
        /// <returns>The lobby name.</returns>
        public string GetName()
        {
            return _name;
        }

        /// <summary>Gets the count.</summary>
        /// <returns>The lobby player count.</returns>
        public int GetCount()
        {
            return _count;
        }

        /// <summary>Gets the maximum.</summary>
        /// <returns>The lobby player max.</returns>
        public int GetMax()
        {
            return _max;
        }

        /// <summary>Gets the full.</summary>
        /// <returns>If the lobby is full.</returns>
        public bool GetFull()
        {
            return _full;
        }
    }
}
