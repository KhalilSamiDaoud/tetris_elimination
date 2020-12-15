namespace Tetris_Elimination.Events
{
    /// <summary>The ServerInformationEvent is used to raise events.</summary>
    public class ServerInformationEvent
    {
        private string _information;

        /// <summary>Initializes a new instance of the <see cref="ServerInformationEvent" /> class.</summary>
        /// <param name="information">The information.</param>
        public ServerInformationEvent(string information)
        {
            _information = information;
        }

        /// <summary>Gets the information string.</summary>
        /// <returns>The server information (IP, Status)</returns>
        public string Get()
        {
            return _information;
        }
    }
}
