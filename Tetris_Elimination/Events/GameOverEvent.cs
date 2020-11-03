namespace Tetris_Elimination.Events
{
    public class GameOverEvent
    {
        private bool _gameOver;

        public GameOverEvent(bool gameOver)
        {
            _gameOver = gameOver;
        }

        public bool Get()
        {
            return _gameOver;
        }
    }
}
