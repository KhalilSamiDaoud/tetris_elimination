using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using System.Windows;
using System;
using Tetris_Elimination.Networking;

namespace Tetris_Elimination.ViewModels //fix namings! and in single player!
{
    public class MultiPlayerViewModel : Conductor<Object>.Collection.AllActive, IHandle<GameOverEvent>, IHandle<ScoreEvent>, IHandle<GamePausedEvent>, IHandle<TickDownEvent>
    {
        private MultiPlayerBoardViewModel gameWindow;
        private StatisticsViewModel statistics;
        private EventAggregatorModel myEvents;
        private MainViewModel mainWindow;
        private Visibility _lost;
        private Visibility _menu;
        private Visibility _countDown;
        private string _lostDialogue;
        private string _countDownDialouge;
        private string gameScore;
        public MultiPlayerViewModel(MainViewModel _mainWindow)
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            mainWindow = _mainWindow;
            statistics = new StatisticsViewModel();
            gameWindow = new MultiPlayerBoardViewModel();
            lost       = Visibility.Hidden;
            menu       = Visibility.Hidden;
            countDown  = Visibility.Visible;
            gameScore  = "0";

            mainWindow.SetBackground = BACKGROUND;
            mainWindow.SetShade = .25;

            this.Items.Add(statistics);
            this.Items.Add(gameWindow);
            ActivateItem(statistics);
            ActivateItem(gameWindow);
        }

        public string lostDialogue
        {
            get { return _lostDialogue; }
            set
            {
                _lostDialogue = value;
                NotifyOfPropertyChange(() => lostDialogue);
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

        public Visibility lost
        {
            get { return _lost; }
            set
            {
                _lost = value;
                NotifyOfPropertyChange(() => lost);
            }
        }

        public Visibility menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                NotifyOfPropertyChange(() => menu);
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

        public void queueAgain()
        {
            menu = Visibility.Hidden;
            myEvents.getAggregator().PublishOnUIThread(new NewGameEvent());
        }

        public void loadMenu()
        {
            ClientManager.Instance.Disconnect();
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

                menu = Visibility.Hidden;
                lost = Visibility.Visible;
                lostDialogue = "Your Score: " + gameScore + "      " + "High Score: " + Properties.Settings.Default.highScore;
            }
            else
            {
                lost = Visibility.Hidden;
            }
        }

        public void Handle(GamePausedEvent message)
        {
            if (!message.Get())
            {
                menu = Visibility.Visible;
            }
            else
            {
                menu = Visibility.Hidden;
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