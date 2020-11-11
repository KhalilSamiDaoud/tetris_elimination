namespace Tetris_Elimination.Events
{
    public class ServerPlayerReadyEvent
    {
        private int _id;
        private bool _isReady;
        public ServerPlayerReadyEvent(int id, bool isReady)
        {
            _id = id;
            _isReady = isReady;
        }

        public int GetID()
        {
            return _id;
        }

        public bool GetIsReady()
        {
            return _isReady;
        }
    }
}
