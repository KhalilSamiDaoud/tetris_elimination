namespace Tetris_Elimination.Events
{
    /// <summary>The ClientConnectEvent class is used to raise events.</summary>
    public class ClientConnectedEvent
    {
        private string[] _ipAndPort;
        private bool _isReconnect;

        /// <summary>Initializes a new instance of the <see cref="ClientConnectedEvent" /> class.</summary>
        /// <param name="ipAndPort">The ip and port.</param>
        /// <param name="isReconnect">if set to <c>true</c> [is reconnect].</param>
        public ClientConnectedEvent(string[] ipAndPort, bool isReconnect)
        {
            _ipAndPort   = ipAndPort;
            _isReconnect = isReconnect;
        }

        /// <summary>Gets the ip.</summary>
        /// <returns>The ip and port</returns>
        public string[] GetIP()
        {
            return _ipAndPort;
        }

        /// <summary>Gets the reconnect request.</summary>
        /// <returns>If its a reconnect.</returns>
        public bool GetReconnect()
        {
            return _isReconnect;
        }
    }
}
