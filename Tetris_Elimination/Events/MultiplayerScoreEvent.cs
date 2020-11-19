namespace Tetris_Elimination.Events
{
    public class MultiplayerScoreEvent
    {
        private int _boardID;
        private int _score;

        public MultiplayerScoreEvent(int id, int score)
        {
            _boardID = id;
            _score = score;
        }

        public int GetID()
        {
            return _boardID;
        }

        public int GetScore()
        {
            return _score;
        }
    }
}
