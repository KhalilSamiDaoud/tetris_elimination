namespace Tetris_Elimination.Events
{
    public class ServerLobbyListEvent
    {
        private string _name;
        private bool _full;
        private int _count;
        private int _id;
        private int _max;
        public ServerLobbyListEvent(int id, string name, int count, bool full, int max)
        {
            _name  = name;
            _count = count;
            _full  = full;
            _id    = id;
            _max   = max;
        }

        public int GetID()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetCount()
        {
            return _count;
        }

        public int GetMax()
        {
            return _max;
        }

        public bool GetFull()
        {
            return _full;
        }
    }
}
