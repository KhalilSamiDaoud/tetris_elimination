using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Caliburn.Micro;

namespace Tetris_Elimination.ViewModels
{
    /// <summary>The StatisticsViewModel class is used to display score, level, next, and held pieces.</summary>
    /// <remarks>The grid related implentations (for next and held) are in the code-behinf for StatisticsView.</remarks>
    /// <seealso cref="Caliburn.Micro.Screen" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.ScoreEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.LevelEvent}" />
    public class StatisticsViewModel : Screen, IHandle<ScoreEvent>, IHandle<LevelEvent>
    {
        private EventAggregatorModel myEvents;
        private int _dispScore;
        private int _dispLevel;

        /// <summary>Initializes a new instance of the <see cref="StatisticsViewModel" /> class.</summary>
        public StatisticsViewModel()
        {
            myEvents  = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            dispScore = 0;
            dispLevel = 1;
        }

        /// <summary>Gets or sets the disp score.</summary>
        /// <value>The disp score.</value>
        public int dispScore
        {
            get { return _dispScore; }
            set
            {
                _dispScore = value;
                NotifyOfPropertyChange(() => dispScore);
            }
        }

        /// <summary>Gets or sets the disp level.</summary>
        /// <value>The disp level.</value>
        public int dispLevel
        {
            get { return _dispLevel; }
            set
            {
                _dispLevel = value;
                NotifyOfPropertyChange(() => dispLevel);
            }
        }

        /// <summary>Handles the message.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ScoreEvent message)
        {
            dispScore = message.Get();
        }

        /// <summary>Handles the message.</summary>
        /// <param name="message">The message.</param>
        public void Handle(LevelEvent message)
        {
            dispLevel = message.Get();
        }
    }
}
