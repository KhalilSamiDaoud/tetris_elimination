using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using System.Windows;
using System;

namespace Tetris_Elimination.ViewModels
{
    public class MultiPlayerViewModel : Conductor<Object>.Collection.AllActive, IHandle<GameOverEvent>, IHandle<ScoreEvent>, IHandle<GamePausedEvent>, IHandle<TickDownEvent>
    {
        private MultiPlayerBoardViewModel gameWindow;
        private StatisticsViewModel statistics;
        private EventAggregatorModel myEvents;
        private MainViewModel mainWindow;
        private Visibility _gameOver;
        private Visibility _paused;
        private Visibility _countDown;
        private string _gameOverDialogue;
        private string _countDownDialouge;
        private string gameScore;
        public MultiPlayerViewModel(MainViewModel _mainWindow)
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            mainWindow = _mainWindow;
            statistics = new StatisticsViewModel();
            gameWindow = new MultiPlayerBoardViewModel();
            gameOver   = Visibility.Hidden;
            paused     = Visibility.Hidden;
            countDown  = Visibility.Visible;
            gameScore  = "0";

            mainWindow.SetBackground = "pack://application:,,,/Assets/Images/Background.png";
            mainWindow.SetShade = .25;

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
            if (message.Get())
            {
                if (Int32.Parse(gameScore) > Properties.Settings.Default.highScore)
                {
                    Properties.Settings.Default.highScore = Int32.Parse(gameScore);
                    Properties.Settings.Default.Save();
                }

                paused = Visibility.Hidden;
                gameOver = Visibility.Visible;
                gameOverDialogue = "Your Score: " + gameScore + "      " + "High Score: " + Properties.Settings.Default.highScore;
            }
            else
            {
                gameOver = Visibility.Hidden;
            }
        }

        public void Handle(GamePausedEvent message)
        {
            if (!message.Get())
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
            gameScore = message.Get().ToString();
        }

        public void Handle(TickDownEvent message)
        {
            countDown = Visibility.Visible;

            if (message.Get() > 0)
            {
                countDownDialouge = message.Get().ToString();
            }
            else if (message.Get() == 0)
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