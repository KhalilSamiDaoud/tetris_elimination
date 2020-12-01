namespace Tetris_Elimination.Events
{
    public class ServerPlayerReadyEvent
    {
        private int _id;
        private int _status;
        public ServerPlayerReadyEvent(int id, int status)
        {
            _id = id;
            _status = status;
        }

        public int GetID()
        {
            return _id;
        }

        public int GetStatus()
        {
            return _status;
        }
    }
}
