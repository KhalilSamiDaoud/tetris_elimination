namespace Tetris_Elimination.Events
{
    public class ClientConnectedEvent
    {
        private string[] _ipAndPort;

        public ClientConnectedEvent(string[] ipAndPort)
        {
            _ipAndPort = ipAndPort;
        }

        public string[] Get()
        {
            return _ipAndPort;
        }
    }
}
