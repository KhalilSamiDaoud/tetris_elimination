using Caliburn.Micro;

namespace Tetris_Elimination.Models
{
    public sealed class EventAggregatorModel
    {
        private static EventAggregatorModel instance      = null;
        private static readonly object padlock            = new object();
        private readonly EventAggregator globalAggregator = new EventAggregator();

        private EventAggregatorModel()
        {
        }

        public static EventAggregatorModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new EventAggregatorModel();
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
