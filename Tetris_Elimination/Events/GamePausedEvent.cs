namespace Tetris_Elimination.Events
{
    /// <summary>The GamePausedEvent class is used to raise events.</summary>
    public class GamePausedEvent
    {
        private bool _paused;

        /// <summary>Initializes a new instance of the <see cref="GamePausedEvent" /> class.</summary>
        /// <param name="paused">if set to <c>true</c> [paused].</param>
        public GamePausedEvent(bool paused)
        {
            _paused = paused;
        }

        /// <summary>Gets this instance.</summary>
        /// <returns>If its a 'pause'.</returns>
        public bool Get()
        {
            return _paused;
        }
    }
}
