using Caliburn.Micro;
using System;
using System.Windows;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.ViewModels
{
    public class SinglePlayerViewModel : Conductor<Object>.Collection.AllActive, IHandle<GameOverEvent>, IHandle<ScoreEvent>, IHandle<GamePausedEvent>, IHandle<TickDownEvent>
    {
        private MainViewModel mainWindow;
        private BoardViewModel gameWindow;
        private StatisticsViewModel statistics;
        private EventAggregatorModel myEvents;
        private Visibility _gameOver;
        private Visibility _paused;
        private Visibility _countDown;
        private string _gameOverDialogue;
        private string _countDownDialouge;
        private string gameScore;
        public SinglePlayerViewModel(MainViewModel _mainWindow)
        {
            myEvents   = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            mainWindow = _mainWindow;
            statistics = new StatisticsViewModel();
            gameWindow = new BoardViewModel();
            gameOver   = Visibility.Hidden;
            paused     = Visibility.Hidden;
            countDown  = Visibility.Visible;
            gameScore  = "0";

            this.Items.Add(statistics);
            this.Items.Add(gameWindow);
            ActivateItem(statistics);
            ActivateItem(gameWindow);
        }

        public string gameOverDialogue
        {
            get { return _gameOverDialogue; }
            set
            {
                _gameOverDialogue = value;
                NotifyOfPropertyChange(() => gameOverDialogue);
            }
        }

        public string countDownDialouge
        {
            get { return _countDownDialouge; }
            set
            {
                _countDownDialouge = value;
                NotifyOfPropertyChange(() => countDownDialouge);
            }
        }

        public Visibility gameOver
        {
            get { return _gameOver; }
            set
            {
                _gameOver = value;
                NotifyOfPropertyChange(() => gameOver);
            }
        }

        public Visibility paused
        {
            get { return _paused; }
            set
            {
                _paused = value;
                NotifyOfPropertyChange(() => paused);
            }
        }

        public Visibility countDown
        {
            get { return _countDown; }
            set
            {
                _countDown = value;
                NotifyOfPropertyChange(() => countDown);
            }
        }

        public void restart()
        {
            paused = Visibility.Hidden;
            myEvents.getAggregator().PublishOnUIThread(new NewGameEvent());
        }

        public void loadMenu()
        {
            mainWindow.SetNewView(Screens.MENU);
        }

        public void Handle(GameOverEvent message)
        {
            if(message.get())
            {
                if (Int32.Parse(gameScore) > Properties.Settings.Default.highScore)
                {
                    Properties.Settings.Default.highScore = Int32.Parse(gameScore);
                    Properties.Settings.Default.Save();
                }

                paused           = Visibility.Hidden;
                gameOver         = Visibility.Visible;
                gameOverDialogue = "Your Score: " + gameScore + "      " + "High Score: " + Properties.Settings.Default.highScore;
            }
            else
            {
                gameOver = Visibility.Hidden;
            }
        }

        public void Handle(GamePausedEvent message)
        {
            if (!message.get())
            {
                paused = Visibility.Visible;
            }
            else
            {
                paused = Visibility.Hidden;
            }
        }

        public void Handle(ScoreEvent message)
        {
            gameScore = message.get().ToString();
        }

        public void Handle(TickDownEvent message)
        {
            countDown = Visibility.Visible;

            if (message.get() > 0)
            {
                countDownDialouge = message.get().ToString();
            }
            else if (message.get() == 0)
            {
                countDownDialouge = "GO!";
            }
            else
            {
                countDown = Visibility.Hidden;
            }
        }
    }
}
