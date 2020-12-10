namespace Tetris_Elimination.Networking
{
    public class LobbyInstance
    {
        private string _playerCountString;
        private int _playerCount;

        public string LobbyName { get; set; }
        public int MaxPlayers   { get; set; }
        public bool IsFull      { get; set; }
        public int UID          { get; set; }

        public LobbyInstance(int lobbyID, string lobbyName, bool isFull, int playerCount, int maxPlayers)
        {
            PlayerCount = playerCount;
            MaxPlayers  = maxPlayers;
            LobbyName   = lobbyName;
            IsFull      = isFull;
            UID         = lobbyID;

            PlayerCountString = "1";
        }

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
