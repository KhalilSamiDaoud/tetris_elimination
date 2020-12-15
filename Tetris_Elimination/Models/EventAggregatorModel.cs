using Caliburn.Micro;

namespace Tetris_Elimination.Models
{
    /// <summary>The EventAggretaorModel is used in many ViewModels as an oberver to subscribe to. The EventAgregator is used to send events to all ViewModels that are listening.
    /// This class is implemented as a singleton and contains an EventAggregator "readonly" object.</summary>
    public sealed class EventAggregatorModel
    {
        private static EventAggregatorModel instance      = null;
        private static readonly object padlock            = new object();
        private readonly EventAggregator globalAggregator = new EventAggregator();

        /// <summary>Prevents a default instance of the <see cref="EventAggregatorModel" /> class from being created.</summary>
        private EventAggregatorModel(){}

        /// <summary>Gets the instance of the EventAggregator.</summary>
        /// <value>The instance.</value>
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

        /// <summary>Gets the aggregator object.</summary>
        /// <returns>The EventAggregator</returns>
        public EventAggregator getAggregator()
        {
            return globalAggregator;
        }
    }
}