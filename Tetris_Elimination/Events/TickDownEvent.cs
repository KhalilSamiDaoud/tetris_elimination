namespace Tetris_Elimination.Events
{
    public class TickDownEvent
    {
        private int _counter;

        public TickDownEvent(int counter)
        {
            _counter = counter;
        }

        public int Get()
        {
            return _counter;
        }
    }
}
