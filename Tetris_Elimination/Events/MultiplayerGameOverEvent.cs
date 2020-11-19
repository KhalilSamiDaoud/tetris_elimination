namespace Tetris_Elimination.Events
{
    public class MultiplayerGameOverEvent
    {
        private int _boardID;
        private bool _gameOver;
        public MultiplayerGameOverEvent(int id, bool gameOver)
        {
            _boardID = id;
            _gameOver = gameOver;
        }

        public int GetID()
        {
            return _boardID;
        }

        public bool GetGameOver()
        {
            return _gameOver;
        }
    }
}
