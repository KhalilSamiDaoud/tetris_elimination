namespace Tetris_Elimination.Events
{
    public class ServerPlayerCountEvent
    {
        private string _playerCount;
        private int _id;

        public ServerPlayerCountEvent(int id, string playerCount)
        {
            _playerCount = playerCount;
            _id          = id;
        }

        public string GetCount()
        {
            return _playerCount;
        }

        public int GetID()
        {
            return _id;
        }
    }
}
