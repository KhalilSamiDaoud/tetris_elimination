namespace Tetris_Elimination.Events
{
    public class ClientConnectedEvent
    {
        private string[] _ipAndPort;
        private bool _isReconnect;

        public ClientConnectedEvent(string[] ipAndPort, bool isReconnect)
        {
            _ipAndPort   = ipAndPort;
            _isReconnect = isReconnect;
        }

        public string[] GetIP()
        {
            return _ipAndPort;
        }

        public bool GetReconnect()
        {
            return _isReconnect;
        }
    }
}
