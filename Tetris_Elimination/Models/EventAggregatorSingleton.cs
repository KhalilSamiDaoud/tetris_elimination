using Caliburn.Micro;

namespace Tetris_Elimination.Models
{
    public sealed class EventAggregatorSingleton
    {
        private static EventAggregatorSingleton instance = null;
        private static readonly object padlock = new object();
        private readonly EventAggregator globalAggregator = new EventAggregator();
        private EventAggregatorSingleton()
        {
        }
        public static EventAggregatorSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new EventAggregatorSingleton();
                    }
                    return instance;
                }
            }
        }

        public EventAggregator getAggregator()
        {
            return globalAggregator;
        }
    }
}
