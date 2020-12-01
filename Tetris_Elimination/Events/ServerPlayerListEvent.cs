namespace Tetris_Elimination.Events
{
    public class ServerPlayerListEvent
    {
        private int _id;
        private int _status;
        private string _userName;

        public ServerPlayerListEvent(int id, int status, string userName)
        {
            _id       = id;
            _status   = status;
            _userName = userName;
        }

        public int GetID()
        {
            return _id;
        }

        public int GetStatus()
        {
            return _status;
        }

        public string GetUserName()
        {
            return _userName;
        }
    }
}
