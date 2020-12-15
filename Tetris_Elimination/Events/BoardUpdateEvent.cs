namespace Tetris_Elimination.Events
{
    /// <summary>The BoardUpdateEvent class is used to raise events.</summary>
    public class BoardUpdateEvent
    {
        private int _boardID;
        private string _encodedGrid;

        /// <summary>Initializes a new instance of the <see cref="BoardUpdateEvent" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="grid">The grid.</param>
        public BoardUpdateEvent(int id, string grid)
        {
            _boardID     = id;
            _encodedGrid = grid;
        }

        /// <summary>Gets the identifier.</summary>
        /// <returns>The board ID</returns>
        public int GetID()
        {
            return _boardID;
        }

        /// <summary>Gets the encoded grid.</summary>
        /// <returns>The encoded player grid</returns>
        public string GetEncodedGrid()
        {
            return _encodedGrid;
        }
    }
}
