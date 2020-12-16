using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Networking;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Caliburn.Micro;
using System.Windows;
using System;

namespace Tetris_Elimination.ViewModels //fix namings! and in single player!
{
    /// <summary>The MultiPlayerViewModel is used to display the MultiPlayerViewModel and Statistics ViewModel.</summary>
    /// <seealso cref="Caliburn.Micro.Conductor{Caliburn.Micro.IScreen}.Collection.AllActive" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.GameOverEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.ScoreEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.GamePausedEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.TickDownEvent}" />
    /// <seealso cref="Caliburn.Micro.IHandle{Tetris_Elimination.Events.ServerDisconnectEvent}" />
    public class MultiPlayerViewModel : Conductor<IScreen>.Collection.AllActive, IHandle<GameOverEvent>, IHandle<ScoreEvent>, IHandle<GamePausedEvent>, IHandle<TickDownEvent>, IHandle<ServerDisconnectEvent>
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

        /// <summary>Initializes a new instance of the <see cref="MultiPlayerViewModel" /> class.</summary>
        /// <param name="_mainWindow">The main window.</param>
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

        /// <summary>Gets or sets the lost dialogue.</summary>
        /// <value>The lost dialogue.</value>
        public string lostDialogue
        {
            get { return _lostDialogue; }
            set
            {
                _lostDialogue = value;
                NotifyOfPropertyChange(() => lostDialogue);
            }
        }

        /// <summary>Gets or sets the count down dialouge number.</summary>
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

        /// <summary>Gets or sets the lost visibility.</summary>
        /// <value>The lost.</value>
        public Visibility lost
        {
            get { return _lost; }
            set
            {
                _lost = value;
                NotifyOfPropertyChange(() => lost);
            }
        }

        /// <summary>Gets or sets the menu visibility.</summary>
        /// <value>The menu.</value>
        public Visibility menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                NotifyOfPropertyChange(() => menu);
            }
        }

        /// <summary>Gets or sets the count down visibility.</summary>
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

        /// <summary>Queues the user again by reconnecting them to the lobby.</summary>
        public void queueAgain()
        {
            ClientManager.Instance.playersInSession[ClientManager.Instance.MyID].ResetState();

            PacketSend.ClientStatus(0);

            mainWindow.SetNewView(Screens.MULTIPLAYER_MENU_RC);
        }

        /// <summary>Loads the menu screen.</summary>
        public void loadMenu()
        {
            ClientManager.Instance.Disconnect();
            mainWindow.SetNewView(Screens.MENU);
        }

        /// <summary>Handles the GameOverEvent event.</summary>
        /// <param name="message">The message.</param>
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

        /// <summary>Handles the GamePausedEvent event.</summary>
        /// <param name="message">The message.</param>
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

        /// <summary>Handles the ServerDisconnectEvent event.</summary>
        /// <param name="message">The message.</param>
        public void Handle(ServerDisconnectEvent message)
        {
            mainWindow.SetNewView(Screens.MENU);
        }
    }
}