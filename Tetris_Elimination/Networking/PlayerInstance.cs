namespace Tetris_Elimination.Networking
{
    public class PlayerInstance
    {
        private bool _isReady;

        public int UID              { get; set; }
        public string UserName      { get; set; }
        public bool GameOver        { get; set; }
        public string IsReadyString { get; set; }
        public string IsReadyColor  { get; set; }


        public PlayerInstance(int clientID, string Name, bool isReady)
        {
            UID      = clientID;
            UserName = Name;
            IsReady  = isReady;
        }

        public bool IsReady
        {
            get { return _isReady; }
            set
            {
                _isReady = value;

                if(_isReady)
                {
                    IsReadyString = "READY";
                    IsReadyColor = "LightGreen";
                }
                else
                {
                    IsReadyString = "NOT READY";
                    IsReadyColor = "Red";
                }
            }
        }
    }
}