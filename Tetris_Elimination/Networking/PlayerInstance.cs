namespace Tetris_Elimination.Networking
{
    public class PlayerInstance
    {
        private int _status;

        public int UID              { get; private set; }
        public string UserName      { get; private set; }
        public bool GameOver        { get; private set; }
        public string StatusString  { get; private set; }
        public string StatusColor   { get; private set; }


        public PlayerInstance(int clientID, string Name, int status)
        {
            UID      = clientID;
            UserName = Name;
            Status   = status;
            GameOver = false;
        }

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

        public void ResetState()
        {
            Status   = 0;
            GameOver = false;
        }
    }
}