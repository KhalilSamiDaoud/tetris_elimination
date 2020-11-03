namespace Tetris_Elimination.Events
{
    public class LevelEvent
    {
        private int _level;

        public LevelEvent(int level)
        {
            _level = level;
        }

        public int Get()
        {
            return _level;
        }
    }
}
