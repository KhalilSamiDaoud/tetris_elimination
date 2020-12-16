using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using System.Windows;
using System;

namespace Tetris_Elimination.ViewModels
{
    /// <summary>The SinglePlayerViewModel is used to display the MultiPlayerViewModel and Statistics ViewModel.</summary>
    /// <seealso cref="Caliburn.Micro.Conductor{System.Object}.Collection.AllActive" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.GameOverEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.ScoreEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.GamePausedEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.TickDownEvent}" />
    public class SinglePlayerViewModel : Conductor<Object>.Collection.AllActive, IHandle<GameOverEvent>, IHandle<ScoreEvent>, IHandle<GamePausedEvent>, IHandle<TickDownEvent>
    {
        private StatisticsViewModel statistics;
        private EventAggregatorModel myEvents;
        private MainViewModel mainWindow;
        private BoardViewModel gameWindow;
        private Visibility _gameOver;
        private Visibility _paused;
        private Visibility _countDown;
        private string _gameOverDialogue;
        private string _countDownDialouge;
        private string gameScore;

        /// <summary>Initializes a new instance of the <see cref="SinglePlayerViewModel" /> class.</summary>
        /// <param name="_mainWindow">The main window.</param>
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

        /// <summary>Gets or sets the game over dialogue.</summary>
        /// <value>The game over dialogue.</value>
        public string gameOverDialogue
        {
            get { return _gameOverDialogue; }
            set
            {
                _gameOverDialogue = value;
                NotifyOfPropertyChange(() => gameOverDialogue);
            }
        }

        /// <summary>Gets or sets the count down dialouge.</summary>
        /// <value>The count down dialouge.</value>
        public string countDownDialouge
        {
            get { return _countDownDialouge; }
            set
            {
                _countDownDialouge = value;
                NotifyOfPropertyChange(() => countDownDialouge);
            }
        }

        /// <summary>Gets or sets the game over.</summary>
        /// <value>The game over.</value>
        public Visibility gameOver
        {
            get { return _gameOver; }
            set
            {
                _gameOver = value;
                NotifyOfPropertyChange(() => gameOver);
            }
        }

        /// <summary>Gets or sets the paused.</summary>
        /// <value>The paused.</value>
        public Visibility paused
        {
            get { return _paused; }
            set
            {
                _paused = value;
                NotifyOfPropertyChange(() => paused);
            }
        }

        /// <summary>Gets or sets the count down.</summary>
        /// <value>The count down.</value>
        public Visibility countDown
        {
            get { return _countDown; }
            set
            {
                _countDown = value;
                NotifyOfPropertyChange(() => countDown);
            }
        }

        /// <summary>Restarts the game.</summary>
        public void restart()
        {
            paused = Visibility.Hidden;
            myEvents.getAggregator().PublishOnUIThread(new NewGameEvent());
        }

        /// <summary>Loads the menu screen.</summary>
        public void loadMenu()
        {
            mainWindow.SetNewView(Screens.MENU);
        }

        /// <summary>Handles the GameOverEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(GameOverEvent message)
        {
            if(message.Get())
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

        /// <summary>Handles the GamePausedEvent event.</summary>
        /// <param name="message">The message.</param>
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

        /// <summary>Handles the ScoreEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ScoreEvent message)
        {
            gameScore = message.Get().ToString();
        }

        /// <summary>Handles the TickDownEvent event.</summary>
        /// <param name="message">The message.</param>
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
