namespace Tetris_Elimination.Events
{
    /// <summary>The TickDownEvent class is used to raise events.</summary>
    public class TickDownEvent
    {
        private int _counter;

        /// <summary>Initializes a new instance of the <see cref="TickDownEvent" /> class.</summary>
        /// <param name="counter">The current counter number.</param>
        public TickDownEvent(int counter)
        {
            _counter = counter;
        }

        /// <summary>Gets the counter number.</summary>
        /// <returns>The surrent counter number.</returns>
        public int Get()
        {
            return _counter;
        }
    }
}
