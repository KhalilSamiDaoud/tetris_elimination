namespace Tetris_Elimination.Networking
{
    /// <summary>The PlayerInstance class is used to store player data from the server.</summary>
    public class PlayerInstance
    {
        private int _status;

        /// <summary>Gets the uid.</summary>
        /// <value>The uid.</value>
        public int UID              { get; private set; }
        /// <summary>Gets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName      { get; private set; }
        /// <summary>Gets a value indicating whether [game over].</summary>
        /// <value><c>true</c> if [game over]; otherwise, <c>false</c>.</value>
        public bool GameOver        { get; private set; }
        /// <summary>Gets the status string.</summary>
        /// <value>The status string.</value>
        public string StatusString  { get; private set; }
        /// <summary>Gets the color of the status.</summary>
        /// <value>The color of the status.</value>
        public string StatusColor   { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="PlayerInstance" /> class.</summary>
        /// <param name="clientID">The client identifier.</param>
        /// <param name="Name">The name.</param>
        /// <param name="status">The status.</param>
        public PlayerInstance(int clientID, string Name, int status)
        {
            UID      = clientID;
            UserName = Name;
            Status   = status;
            GameOver = false;
        }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        public int Status
        {
            get { return _status; }
            set
            {
                _status = value;

                switch (_status)
                {
                    case 0:
                        StatusString = "NOT READY";
                        StatusColor = "Red";
                        break;
                    case 1:
                        StatusString = "READY";
                        StatusColor = "LightGreen";
                        break;
                    case 2:
                        StatusString = "IN GAME";
                        StatusColor = "Yellow";
                        break;
                }
            }
        }

        /// <summary>Resets the state of the object.</summary>
        public void ResetState()
        {
            Status   = 0;
            GameOver = false;
        }
    }
}