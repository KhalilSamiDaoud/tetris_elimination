namespace Tetris_Elimination.Events
{
    public class GamePausedEvent
    {
        private bool _paused;

        public GamePausedEvent(bool paused)
        {
            _paused = paused;
        }

        public bool Get()
        {
            return _paused;
        }
    }
}
