namespace Tetris_Elimination.Events
{
    /// <summary>The LevelEvent class is used to raise events.</summary>
    public class LevelEvent
    {
        private int _level;

        /// <summary>Initializes a new instance of the <see cref="LevelEvent" /> class.</summary>
        /// <param name="level">The level.</param>
        public LevelEvent(int level)
        {
            _level = level;
        }

        /// <summary>Gets this instance.</summary>
        /// <returns>The level of the player.</returns>
        public int Get()
        {
            return _level;
        }
    }
}
