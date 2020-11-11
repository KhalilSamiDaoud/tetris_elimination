namespace Tetris_Elimination.Events
{
    public class ServerPlayerListEvent
    {
        private int _id;
        private bool _isReady;
        private string _userName;

        public ServerPlayerListEvent(int id, bool isReady, string userName)
        {
            _id       = id;
            _isReady  = isReady;
            _userName = userName;
        }

        public int GetID()
        {
            return _id;
        }

        public bool GetIsReady()
        {
            return _isReady;
        }

        public string GetUserName()
        {
            return _userName;
        }
    }
}
