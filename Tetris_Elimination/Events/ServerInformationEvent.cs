namespace Tetris_Elimination.Events
{
    public class ServerInformationEvent
    {
        private string _information;

        public ServerInformationEvent(string information)
        {
            _information = information;
        }

        public string Get()
        {
            return _information;
        }
    }
}
