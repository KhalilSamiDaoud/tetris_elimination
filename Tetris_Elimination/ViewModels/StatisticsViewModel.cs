using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris_Elimination.Models;

namespace Tetris_Elimination.ViewModels
{
    public class StatisticsViewModel : Screen, IHandle<int>
    {
        private int _dispScore;
        private EventAggregatorSingleton myEvents;
        public StatisticsViewModel()
        {
            myEvents = EventAggregatorSingleton.Instance;
            myEvents.getAggregator().Subscribe(this);
            dispScore = 0;
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

        public void Handle(int message)
        {
            dispScore = message;
        }
    }
}
