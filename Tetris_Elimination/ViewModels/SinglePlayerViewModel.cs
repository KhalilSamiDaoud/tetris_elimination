using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using Tetris_Elimination.Views;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class SinglePlayerViewModel : Conductor<Object>.Collection.AllActive, IHandle<GameOverEvent>, IHandle<ScoreEvent>
    {
        private MainViewModel mainWindow;
        private BoardViewModel gameWindow;
        private StatisticsViewModel statistics;
        private Visibility _gameOver;
        private EventAggregatorSingleton myEvents;
        private string _gameOverDialogue;
        private String highScore;
        private String gameScore;
        public SinglePlayerViewModel(MainViewModel _mainWindow)
        {
            myEvents = EventAggregatorSingleton.Instance;
            myEvents.getAggregator().Subscribe(this);
            mainWindow = _mainWindow;
            statistics = new StatisticsViewModel();
            gameWindow = new BoardViewModel();
            this.Items.Add(statistics);
            this.Items.Add(gameWindow);
            ActivateItem(statistics);
            ActivateItem(gameWindow);
            gameOver = Visibility.Hidden;
            highScore = "N/A";
            gameScore = "0";
        }

        public Visibility gameOver
        {
            get
            {
                return _gameOver;
            }
            set
            {
                _gameOver = value;
                NotifyOfPropertyChange(() => gameOver);
            }
        }
        public string gameOverDialogue
        {
            get
            {
                return _gameOverDialogue;
            }
            set
            {
                _gameOverDialogue = value;
                NotifyOfPropertyChange(() => gameOverDialogue);
            }
        }

        public void restart()
        {
            myEvents.getAggregator().PublishOnUIThread(new NewGameEvent());
        }

        public void LoadMenu()
        {
            mainWindow.SetNewView(Screens.MENU);
        }

        public void Handle(GameOverEvent message)
        {
            if(message.get())
            {
                gameOver = Visibility.Visible;
                gameOverDialogue = "Your Score: " + gameScore + "      " + "High Score: N / A";
            }
            else
            {
                gameOver = Visibility.Hidden;
            }
        }
        public void Handle(ScoreEvent message)
        {
            gameScore = message.get().ToString();
        }
    }
}
