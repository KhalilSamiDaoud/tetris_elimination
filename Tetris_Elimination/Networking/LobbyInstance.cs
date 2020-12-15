namespace Tetris_Elimination.Networking
{
    /// <summary>The LobbyInstance class is used to hold lobby information from the server.</summary>
    public class LobbyInstance
    {
        private string _playerCountString;
        private int _playerCount;

        /// <summary>Gets or sets the name of the lobby.</summary>
        /// <value>The name of the lobby.</value>
        public string LobbyName { get; set; }
        /// <summary>Gets or sets the maximum players.</summary>
        /// <value>The maximum players.</value>
        public int MaxPlayers   { get; set; }
        /// <summary>Gets or sets a value indicating whether this instance is full.</summary>
        /// <value>
        ///   <c>true</c> if this instance is full; otherwise, <c>false</c>.</value>
        public bool IsFull      { get; set; }
        /// <summary>Gets or sets the uid.</summary>
        /// <value>The uid.</value>
        public int UID          { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LobbyInstance" /> class.</summary>
        /// <param name="lobbyID">The lobby identifier.</param>
        /// <param name="lobbyName">Name of the lobby.</param>
        /// <param name="isFull">if set to <c>true</c> [is full].</param>
        /// <param name="playerCount">The player count.</param>
        /// <param name="maxPlayers">The maximum players.</param>
        public LobbyInstance(int lobbyID, string lobbyName, bool isFull, int playerCount, int maxPlayers)
        {
            PlayerCount = playerCount;
            MaxPlayers  = maxPlayers;
            LobbyName   = lobbyName;
            IsFull      = isFull;
            UID         = lobbyID;

            PlayerCountString = "1";
        }

        /// <summary>Gets or sets the player count.</summary>
        /// <value>The player count.</value>
        public int PlayerCount
        {
            get { return _playerCount; }
            set
            {
                _playerCount = value;

                PlayerCountString = value.ToString();

                if (_playerCount == MaxPlayers)
                {
                    IsFull = true;
                }
                else
                {
                    IsFull = false;
                }
            }
        }

        /// <summary>Gets or sets the player count string.</summary>
        /// <value>The player count string.</value>
        public string PlayerCountString
        {
            get { return _playerCountString; }
            set
            {
                _playerCountString = value + "/" + MaxPlayers;
            }
        }
    }
}
