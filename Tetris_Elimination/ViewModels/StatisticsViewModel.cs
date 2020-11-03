using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Caliburn.Micro;

namespace Tetris_Elimination.ViewModels
{
    public class StatisticsViewModel : Screen, IHandle<ScoreEvent>, IHandle<LevelEvent>
    {
        private EventAggregatorModel myEvents;
        private int _dispScore;
        private int _dispLevel;

        public StatisticsViewModel()
        {
            myEvents  = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            dispScore = 0;
            dispLevel = 1;
        }

        public int dispScore
        {
            get { return _dispScore; }
            set
            {
                _dispScore = value;
                NotifyOfPropertyChange(() => dispScore);
            }
        }

        public int dispLevel
        {
            get { return _dispLevel; }
            set
            {
                _dispLevel = value;
                NotifyOfPropertyChange(() => dispLevel);
            }
        }

        public void Handle(ScoreEvent message)
        {
            dispScore = message.Get();
        }

        public void Handle(LevelEvent message)
        {
            dispLevel = message.Get();
        }
    }
}
