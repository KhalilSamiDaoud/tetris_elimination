namespace Tetris_Elimination.Events
{
    public class BoardUpdateEvent
    {
        private int _boardID;
        private string _encodedGrid;
        public BoardUpdateEvent(int id, string grid)
        {
            _boardID     = id;
            _encodedGrid = grid;
        }

        public int GetID()
        {
            return _boardID;
        }

        public string GetEncodedGrid()
        {
            return _encodedGrid;
        }
    }
}
