using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using System.Windows;
using System.Timers;
using System;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class MultiPlayerBoardView : UserControl, IHandle<NewGameEvent>
    {
        private const int row = 22;
        private const int col = 12;
        private TetreminoModel currentTetremino;
        private TetreminoModel nextTetremino;
        private TetreminoModel heldTetremino;
        private EventAggregatorModel myEvents;
        private AudioManagerModel audioManager;
        private ImageBrush background;
        private ImageBrush border;
        private Array tetreminoTypes;
        private Random rand;
        private Label[,] cell;
        private bool gameOver;
        private bool gameStarted;
        private bool swappedThisTurn;
        private int interval;
        private int level;
        private int clearedRows;
        private int score;
        private int levelThreshhold;
        private int countDown;
        private static Timer eventTimer;
        private static Timer encoderTimer;

        public MultiPlayerBoardView()
        {
            gameOver = false;
            gameStarted = false;
            swappedThisTurn = false;
            interval = 1000;
            level = 1;
            clearedRows = 0;
            score = 0;
            levelThreshhold = 500;
            countDown = 4;
            tetreminoTypes = Enum.GetValues(typeof(Tetremino));
            background = BACKGROUND_TILE;
            border = BORDER_TILE;
            rand = new Random();
            cell = new Label[col, row];
            eventTimer = new Timer();
            encoderTimer = new Timer();

            myEvents = EventAggregatorModel.Instance;
            audioManager = AudioManagerModel.Instance;

            myEvents.getAggregator().Subscribe(this);
            audioManager.PauseTheme();

            eventTimer.Elapsed += new ElapsedEventHandler(GameLoop);
            eventTimer.Interval = interval;
            eventTimer.Start();

            encoderTimer.Elapsed += new ElapsedEventHandler(EncodeGrid);
            encoderTimer.Interval = 50;

            InitializeComponent();
            SetupBoard();
        }

        private void BoardView_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += BoardView_KeyDown;
        }

        private void BoardView_KeyDown(object sender, KeyEventArgs e)
        {
            int keyPressed = (int)e.Key;

            if (gameStarted && !gameOver && countDown < 0)
            {
                if (keyPressed == Properties.Settings.Default.Down)
                {
                    MoveDown();
                }
                else if (keyPressed == Properties.Settings.Default.Right)
                {
                    MoveRight();
                }
                else if (keyPressed == Properties.Settings.Default.Left)
                {
                    MoveLeft();
                }
                else if (keyPressed == Properties.Settings.Default.Hold)
                {
                    HoldPiece();
                }
                else if (keyPressed == Properties.Settings.Default.Drop)
                {
                    DropPiece();
                }
                else if (keyPressed == Properties.Settings.Default.Rotate)
                {
                    Rotate();
                }
                else if (keyPressed == Properties.Settings.Default.Pause)
                {
                    PauseGame();
                }
            }
            else if (!gameStarted && !gameOver && countDown < 0)
            {
                if (keyPressed == Properties.Settings.Default.Pause)
                {
                    PauseGame();
                }
            }
        }

        private void GameLoop(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (gameStarted && !gameOver && countDown < 0)
                {
                    MoveDown();
                    CalculateInterval();
                }
                else if (countDown >= 0)
                {

                    countDown--;

                    if (countDown == -1)
                    {
                        myEvents.getAggregator().PublishOnUIThread(new TickDownEvent(countDown));
                        StartGame();
                        encoderTimer.Start();
                    }
                    else
                    {
                        if (countDown == 0)
                        {
                            audioManager.PlaySound(Sound.TIMER_END);
                        }
                        else
                        {
                            audioManager.PlaySound(Sound.TIMER);
                        }
                        myEvents.getAggregator().PublishOnUIThread(new TickDownEvent(countDown));
                    }
                }
            });
        }

        private void EncodeGrid(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                string encodedGrid = "";

                for (int i = 1; i < (col - 1); i++)
                {
                    for (int j = 1; j < row; j++)
                    {
                        encodedGrid += Array.IndexOf(TILE_ARRAY, cell[i, j].Background);
                    }
                }

                myEvents.getAggregator().PublishOnUIThread(new BoardUpdateEvent(1, encodedGrid));
            });
        }

        private void SetupBoard()
        {
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {

                    cell[i, j] = new Label();

                    if (i == 0 || i == 11 || j == 0 || j == 21)
                    {
                        cell[i, j].Background = border;
                        Grid.SetRow(cell[i, j], j);
                        Grid.SetColumn(cell[i, j], i);
                        BoardGrid.Children.Add(cell[i, j]);
                    }
                    else
                    {
                        cell[i, j].Background = background;
                        Grid.SetRow(cell[i, j], j);
                        Grid.SetColumn(cell[i, j], i);
                        BoardGrid.Children.Add(cell[i, j]);
                    }
                }
            }
        }

        private void StartGame()
        {
            gameStarted = true;
            currentTetremino = SpawnTetromino();
            nextTetremino = SpawnTetromino();
            heldTetremino = null;
            myEvents.getAggregator().PublishOnUIThread(new NextPieceEvent(nextTetremino));
            myEvents.getAggregator().PublishOnUIThread(new HeldPieceEvent(heldTetremino));
            myEvents.getAggregator().PublishOnUIThread(new ScoreEvent(score));
            myEvents.getAggregator().PublishOnUIThread(new LevelEvent(level));
            DrawTetremino();
            audioManager.PlayTheme();
        }

        private void ResetGame()
        {
            ClearBoard();
            score = 0;
            level = 1;
            levelThreshhold = 500;
            gameOver = false;
            countDown = 4;
            interval = 1000;
            eventTimer.Interval = interval;
            currentTetremino = null;
            myEvents.getAggregator().PublishOnUIThread(new NextPieceEvent(null));
            myEvents.getAggregator().PublishOnUIThread(new HeldPieceEvent(null));
            myEvents.getAggregator().PublishOnUIThread(new GameOverEvent(gameOver));
            myEvents.getAggregator().PublishOnUIThread(new ScoreEvent(score));
            myEvents.getAggregator().PublishOnUIThread(new LevelEvent(level));
            eventTimer.Start();
            audioManager.PauseTheme();
        }

        private void PauseGame()
        {
            if (gameStarted)
            {
                gameStarted = false;
                eventTimer.Stop();
                encoderTimer.Stop();
                audioManager.PauseTheme();
                myEvents.getAggregator().PublishOnUIThread(new GamePausedEvent(gameStarted));
            }
            else
            {
                gameStarted = true;
                eventTimer.Start();
                encoderTimer.Start();
                audioManager.UnpauseTheme();
                myEvents.getAggregator().PublishOnUIThread(new GamePausedEvent(gameStarted));
            }
        }

        private void EndGame()
        {
            gameOver = true;
            eventTimer.Stop();
            encoderTimer.Stop();
            myEvents.getAggregator().PublishOnUIThread(new GameOverEvent(gameOver));
        }

        private TetreminoModel SpawnTetromino()
        {
            CheckRows();
            CalculateScore();
            CalculateLevel();
            TetreminoModel tempTetremino = new TetreminoModel((Tetremino)tetreminoTypes.GetValue(rand.Next(tetreminoTypes.Length)));

            if (MoveIsLegal(Move.SPAWN, tempTetremino))
            {
                return tempTetremino;
            }
            else
            {
                EndGame();
                return null;
            }
        }

        private void DrawTetremino()
        {
            if (currentTetremino != null)
            {
                for (int i = 0; i < currentTetremino.GetShape().Length; i++)
                {
                    cell[(int)(currentTetremino.GetShape()[i].X + currentTetremino.GetPosition().X),
                         (int)(currentTetremino.GetShape()[i].Y + currentTetremino.GetPosition().Y)].Background = currentTetremino.GetBrush();
                }
            }
        }

        private void ClearTetremino()
        {
            if (currentTetremino != null)
            {
                for (int i = 0; i < currentTetremino.GetShape().Length; i++)
                {
                    cell[(int)(currentTetremino.GetShape()[i].X + currentTetremino.GetPosition().X),
                         (int)(currentTetremino.GetShape()[i].Y + currentTetremino.GetPosition().Y)].Background = this.background;
                }
            }
        }

        private void MoveDown()
        {
            if (MoveIsLegal(Move.DOWN, currentTetremino))
            {
                ClearTetremino();
                currentTetremino.MovePoint(Move.DOWN);
                DrawTetremino();
            }
            else
            {
                currentTetremino = nextTetremino;
                nextTetremino = SpawnTetromino();
                swappedThisTurn = false;
                if (nextTetremino != null)
                {
                    //this check is to reduce the overall amounts of duplicates
                    if (currentTetremino.GetType() == nextTetremino.GetType())
                    {
                        nextTetremino = SpawnTetromino();
                    }
                    DrawTetremino();
                    myEvents.getAggregator().PublishOnUIThread(new NextPieceEvent(nextTetremino));
                }
            }
        }

        private void MoveRight()
        {
            if (MoveIsLegal(Move.RIGHT, currentTetremino))
            {
                ClearTetremino();
                currentTetremino.MovePoint(Move.RIGHT);
                DrawTetremino();
            }
        }

        private void MoveLeft()
        {
            if (MoveIsLegal(Move.LEFT, currentTetremino))
            {
                ClearTetremino();
                currentTetremino.MovePoint(Move.LEFT);
                DrawTetremino();
            }
        }

        private void Rotate()
        {
            if (currentTetremino.GetType() != Tetremino.YELLOW_O)
            {
                if (MoveIsLegal(Move.ROTATE, currentTetremino))
                {
                    ClearTetremino();
                    currentTetremino.MovePoint(Move.ROTATE);
                    DrawTetremino();
                    audioManager.PlaySound(Sound.ROTATE);
                }
            }
        }

        private void HoldPiece()
        {
            TetreminoModel temp;

            if (heldTetremino == null && !swappedThisTurn)
            {
                ClearTetremino();
                heldTetremino = new TetreminoModel(currentTetremino.GetType());
                currentTetremino = nextTetremino;
                nextTetremino = SpawnTetromino();
                swappedThisTurn = true;
                DrawTetremino();
                myEvents.getAggregator().PublishOnUIThread(new NextPieceEvent(nextTetremino));
                myEvents.getAggregator().PublishOnUIThread(new HeldPieceEvent(heldTetremino));
            }
            else
            {
                if (MoveIsLegal(Move.SPAWN, heldTetremino) && !swappedThisTurn)
                {
                    temp = currentTetremino;
                    ClearTetremino();
                    currentTetremino = new TetreminoModel(heldTetremino.GetType());
                    heldTetremino = new TetreminoModel(temp.GetType());
                    DrawTetremino();
                    swappedThisTurn = true;
                    myEvents.getAggregator().PublishOnUIThread(new HeldPieceEvent(heldTetremino));
                }
            }
        }

        private void DropPiece()
        {
            while (MoveIsLegal(Move.DOWN, currentTetremino))
            {
                MoveDown();
            }
            MoveDown();
            audioManager.PlaySound(Sound.DROP);
        }

        private Boolean MoveIsLegal(Move direction, TetreminoModel checkingTetremino)
        {
            ClearTetremino();

            switch (direction)
            {
                case Move.SPAWN:
                    for (int i = 0; i < checkingTetremino.GetShape().Length; i++)
                    {
                        if (!cell[(int)(checkingTetremino.GetShape()[i].X + checkingTetremino.GetPosition().X),
                                  (int)(checkingTetremino.GetShape()[i].Y + checkingTetremino.GetPosition().Y)].Background.Equals(this.background))
                        {
                            if (currentTetremino == null)
                            {
                                return false;
                            }
                            DrawTetremino();
                            return false;
                        }
                    }
                    break;
                case Move.DOWN:
                    for (int i = 0; i < checkingTetremino.GetShape().Length; i++)
                    {
                        if (!cell[(int)(checkingTetremino.GetShape()[i].X + checkingTetremino.GetPosition().X),
                                  (int)(checkingTetremino.GetShape()[i].Y + checkingTetremino.GetPosition().Y + 1)].Background.Equals(this.background))
                        {
                            DrawTetremino();
                            return false;
                        }
                    }
                    break;
                case Move.RIGHT:
                    for (int i = 0; i < checkingTetremino.GetShape().Length; i++)
                    {
                        if (!cell[(int)(checkingTetremino.GetShape()[i].X + checkingTetremino.GetPosition().X + 1),
                                  (int)(checkingTetremino.GetShape()[i].Y + checkingTetremino.GetPosition().Y)].Background.Equals(this.background))
                        {
                            DrawTetremino();
                            return false;
                        }
                    }
                    break;
                case Move.LEFT:
                    for (int i = 0; i < checkingTetremino.GetShape().Length; i++)
                    {
                        if (!cell[(int)(checkingTetremino.GetShape()[i].X + checkingTetremino.GetPosition().X - 1),
                                  (int)(checkingTetremino.GetShape()[i].Y + checkingTetremino.GetPosition().Y)].Background.Equals(this.background))
                        {
                            DrawTetremino();
                            return false;
                        }
                    }
                    break;
                case Move.ROTATE: //fix this and add auto-adjusting

                    Point[] tempShape = checkingTetremino.GetShape();
                    Point tempPoint;

                    for (int i = 0; i < checkingTetremino.GetShape().Length; i++)
                    {
                        tempPoint = checkingTetremino.RotatePoint(tempShape[i], tempShape[2]);
                        if (!cell[(int)(tempPoint.X + checkingTetremino.GetPosition().X),
                                  (int)(tempPoint.Y + checkingTetremino.GetPosition().Y)].Background.Equals(this.background))
                        {
                            DrawTetremino();
                            return false;
                        }
                    }
                    break;
                default:
                    DrawTetremino();
                    return false;
            }
            DrawTetremino();
            return true;
        }

        private void ClearBoard()
        {
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (i == 0 || i == 11 || j == 0 || j == 21)
                    {
                        cell[i, j].Background = border;
                    }
                    else
                    {
                        cell[i, j].Background = background;
                    }
                }
            }
        }

        private void CheckRows() //row 12 & 15 does not clear?
        {
            int count = 0;

            for (int i = 1; i < row; i++)
            {
                for (int j = 1; j < (col - 1); j++)
                {
                    if (cell[j, i].Background.Equals(this.background))
                    {
                        count = 0;
                        break;
                    }
                    else if (count == 10)
                    {
                        count = 0;
                        clearedRows++;
                        ClearRow(i);
                        CheckRows();
                        break;
                    }
                    count++;
                }
            }
        }

        private void ClearRow(int rowToBeCleared)
        {
            for (int j = 1; j < (col - 1); j++)
            {
                cell[j, (rowToBeCleared - 1)].Background = this.background;
            }
            ShiftRows(rowToBeCleared - 1);
        }

        private void ShiftRows(int rowToStartAt)
        {
            for (int i = rowToStartAt; i > 1; i--)
            {
                for (int j = 1; j < (col - 1); j++)
                {
                    cell[j, i].Background = cell[j, (i - 1)].Background;
                }
            }
        }

        private void CalculateScore()
        {
            int multiplier = 0;

            switch (clearedRows)
            {
                case 0:
                    break;
                case 1:
                    multiplier = 100;
                    break;
                case int n when (n == 2 || n == 3):
                    multiplier = 200;
                    break;
                case 4:
                    multiplier = 500;
                    break;
                case 5:
                    multiplier = 1000;
                    break;
                default:
                    multiplier = 1500;
                    break;
            }

            if (multiplier != 0)
            {
                audioManager.PlaySound(Sound.CLEARED_ROW);
            }

            score = (score + (clearedRows * multiplier));
            clearedRows = 0;
            myEvents.getAggregator().PublishOnUIThread(new ScoreEvent(score));
        }

        private void CalculateLevel()
        {
            int tempLevel = (score / levelThreshhold);

            if (tempLevel > level)
            {
                level++;
                levelThreshhold = (int)(levelThreshhold * 1.25);
            }
            myEvents.getAggregator().PublishOnUIThread(new LevelEvent(level));
        }

        private void CalculateInterval()
        {
            if (level <= 8 && level > 1)
            {
                interval = (1000 - (level * 80));
                eventTimer.Interval = interval;
            }
        }

        public void Handle(NewGameEvent message)
        {
            ResetGame();
        }
    }
}
