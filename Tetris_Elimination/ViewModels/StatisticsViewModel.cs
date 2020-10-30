using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;

namespace Tetris_Elimination.ViewModels
{
    public class StatisticsViewModel : Screen, IHandle<ScoreEvent>, IHandle<LevelEvent>
    {
        private int _dispScore;
        private int _dispLevel;
        private EventAggregatorModel myEvents;
        public StatisticsViewModel()
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);
            dispScore = 0;
            dispLevel = 1;
        }

        public int dispScore
        {
            get
            {
                return _dispScore;
            }
            set
            {
                _dispScore = value;
                NotifyOfPropertyChange(() => dispScore);
            }
        }

        public int dispLevel
        {
            get
            {
                return _dispLevel;
            }
            set
            {
                _dispLevel = value;
                NotifyOfPropertyChange(() => dispLevel);
            }
        }

        public void Handle(ScoreEvent message)
        {
            dispScore = message.get();
        }

        public void Handle(LevelEvent message)
        {
            dispLevel = message.get();
        }
    }
}
