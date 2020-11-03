namespace Tetris_Elimination.Events
{
    public class ScoreEvent
    {
        private int _score;

        public ScoreEvent(int score)
        {
            _score = score;
        }

        public int Get()
        {
            return _score;
        }
    }
}
