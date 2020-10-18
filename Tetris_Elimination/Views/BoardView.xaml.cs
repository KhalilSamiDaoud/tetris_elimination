

using Caliburn.Micro;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private const int row            =     22;
        private const int col            =     12;
        private int interval             =     1000;
        private int score                =     0;
        private int level                =     1;
        private int clearedRows          =     0;
        private int levelThreshhold      =     500;
        private bool gameOver            =     false;
        private bool gameStarted         =     false;
        private bool swappedThisTurn     =     false;
        private Array tetreminoTypes     =     Enum.GetValues(typeof(Tetremino));
        private ImageBrush background    =     new ImageBrush();
        private ImageBrush border        =     new ImageBrush();
        private Random rand              =     new Random();
        private Label[,] cell            =     new Label[col, row];
        private TetreminoModel currentTetremino;
        private TetreminoModel nextTetremino;
        private TetreminoModel heldTetremino;
        private static Timer eventTimer;
        private EventAggregatorSingleton myEvents;

        public BoardView()
        {
            myEvents = EventAggregatorSingleton.Instance;
            myEvents.getAggregator().Subscribe(this);
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Background_Tile.png", UriKind.Absolute));
            border.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Border_Tile.png", UriKind.Absolute));

            InitializeComponent();
            setupBoard();
            startGame();
        }

        private void BoardView_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += BoardView_KeyDown;
        }

        private void BoardView_KeyDown(object sender, KeyEventArgs e)
        {
            if (getGameStarted() && !GetGameStatus())
            {
                switch (e.Key)
                {
                    case Key.Down:
                        moveDown();
                        break;
                    case Key.Right:
                        moveRight();
                        break;
                    case Key.Left:
                        moveLeft();
                        break;
                    case Key.Up:
                        Rotate();
                        break;
                    case Key.C:
                        holdPeice();
                        break;
                    case Key.Space:
                        break;
                    case Key.P:
                        break;
                    default:
                        break;
                }
            }
        }

        private void gameLoop(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (!GetGameStatus())
                {
                    moveDown();
                    calculateInterval();
                }
                else
                {
                    eventTimer.Stop();
                }
            });
        }

        public int GetScore()
        {
            return score;
        }

        public int GetLevel()
        {
            return level;
        }

        public bool GetGameStatus()
        {
            return gameOver;
        }

        public bool getGameStarted()
        {
            return gameStarted;
        }

        private void setupBoard()
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

        private void startGame()
        {
            eventTimer          = new Timer();
            eventTimer.Elapsed += new ElapsedEventHandler(gameLoop);
            eventTimer.Interval = interval;
            eventTimer.Start();
            gameStarted         = true;

            currentTetremino    = spawnTetromino();
            nextTetremino       = spawnTetromino();
            myEvents.getAggregator().PublishOnUIThread(new NextPieceEvent(nextTetremino));
            drawTetremino();

        }

        private TetreminoModel spawnTetromino()
        {
            checkRows();
            calculateScore();
            calculateLevel();
            TetreminoModel tempTetremino = new TetreminoModel((Tetremino)tetreminoTypes.GetValue(rand.Next(tetreminoTypes.Length)));

            if(moveIsLegal(Move.SPAWN, tempTetremino))
            {
                return tempTetremino;
            }
            else
            {
                gameOver = true;
                return null;
            }
        }

        private void drawTetremino()
        {
           for(int i=0; i < currentTetremino.getShape().Length; i++)
           {
                cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X), 
                     (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y)].Background = currentTetremino.getBrush();
            }
        }

        private void clearTetremino()
        {
            for (int i = 0; i < currentTetremino.getShape().Length; i++)
            {
                cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X),
                     (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y)].Background = this.background;
            }
        }

        private void moveDown()
        {
            if (moveIsLegal(Move.DOWN, currentTetremino))
            {
                clearTetremino();
                currentTetremino.move(Move.DOWN);
                drawTetremino();
            }
            else
            {
                currentTetremino = nextTetremino;
                nextTetremino = spawnTetromino();
                swappedThisTurn = false;
                if (nextTetremino != null)
                {
                    //this check is to reduce the overall amounts of duplicates
                    if (currentTetremino.getType() == nextTetremino.getType())
                    {
                        nextTetremino = spawnTetromino();
                    }
                    drawTetremino();
                    myEvents.getAggregator().PublishOnUIThread(new NextPieceEvent(nextTetremino));
                }
            }
        }

        private void moveRight()
        {
            if (moveIsLegal(Move.RIGHT, currentTetremino))
            {
                clearTetremino();
                currentTetremino.move(Move.RIGHT);
                drawTetremino();
            }
        }

        private void moveLeft()
        {
            if (moveIsLegal(Move.LEFT, currentTetremino))
            {
                clearTetremino();
                currentTetremino.move(Move.LEFT);
                drawTetremino();
            }
        }

        private void Rotate()
        {
            if (currentTetremino.getType() != Tetremino.YELLOW_O)
            {
                if(moveIsLegal(Move.ROTATE, currentTetremino))
                {
                    clearTetremino();
                    currentTetremino.move(Move.ROTATE);
                    drawTetremino();
                }
            }
        }

        private Boolean moveIsLegal(Move direction, TetreminoModel checkingTetremino)
        {
            switch(direction)
            {
                case Move.SPAWN:
                    for (int i = 0; i < checkingTetremino.getShape().Length; i++)
                    {
                        if (!cell[(int)(checkingTetremino.getShape()[i].X + checkingTetremino.getPosition().X),
                                  (int)(checkingTetremino.getShape()[i].Y + checkingTetremino.getPosition().Y)].Background.Equals(this.background) &&
                            !cell[(int)(checkingTetremino.getShape()[i].X + checkingTetremino.getPosition().X),
                                  (int)(checkingTetremino.getShape()[i].Y + checkingTetremino.getPosition().Y)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                case Move.DOWN:
                    for (int i = 0; i < checkingTetremino.getShape().Length; i++)
                    {
                        if (!cell[(int)(checkingTetremino.getShape()[i].X + checkingTetremino.getPosition().X),
                                  (int)(checkingTetremino.getShape()[i].Y + checkingTetremino.getPosition().Y + 1)].Background.Equals(this.background) &&
                            !cell[(int)(checkingTetremino.getShape()[i].X + checkingTetremino.getPosition().X),
                                  (int)(checkingTetremino.getShape()[i].Y + checkingTetremino.getPosition().Y + 1)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                case Move.RIGHT:
                    for (int i = 0; i < checkingTetremino.getShape().Length; i++)
                    {
                        if (!cell[(int)(checkingTetremino.getShape()[i].X + checkingTetremino.getPosition().X + 1),
                                  (int)(checkingTetremino.getShape()[i].Y + checkingTetremino.getPosition().Y)].Background.Equals(this.background) &&
                            !cell[(int)(checkingTetremino.getShape()[i].X + checkingTetremino.getPosition().X + 1),
                                  (int)(checkingTetremino.getShape()[i].Y + checkingTetremino.getPosition().Y)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                case Move.LEFT:
                    for (int i = 0; i < checkingTetremino.getShape().Length; i++)
                    {
                        if (!cell[(int)(checkingTetremino.getShape()[i].X + checkingTetremino.getPosition().X - 1),
                                  (int)(checkingTetremino.getShape()[i].Y + checkingTetremino.getPosition().Y)].Background.Equals(this.background) &&
                            !cell[(int)(checkingTetremino.getShape()[i].X + checkingTetremino.getPosition().X - 1),
                                  (int)(checkingTetremino.getShape()[i].Y + checkingTetremino.getPosition().Y)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                case Move.ROTATE: //fix this and add auto-adjusting

                    Point[] tempShape = checkingTetremino.getShape();
                    Point tempPoint;

                    for (int i = 0; i < checkingTetremino.getShape().Length; i++)
                    {
                        tempPoint = checkingTetremino.rotatePoint(tempShape[i], tempShape[2]);
                        if (!cell[(int)(tempPoint.X + checkingTetremino.getPosition().X),
                                  (int)(tempPoint.Y + checkingTetremino.getPosition().Y)].Background.Equals(this.background) &&
                            !cell[(int)(tempPoint.X + checkingTetremino.getPosition().X),
                                  (int)(tempPoint.Y + checkingTetremino.getPosition().Y)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }

        private void checkRows()
        {
            int count = 0;

            for (int i=1; i < row; i++)
            {
                for (int j=1; j < (col-1); j++)
                {
                    if (cell[j,i].Background.Equals(this.background)) {
                        count = 0;
                        break;
                    }
                    else if (count == 10)
                    {
                        count = 0;
                        clearedRows++;
                        clearRow(i);
                        checkRows();
                        break;
                    }
                    count++;
                }
            }
        }

        private void clearRow(int rowToBeCleared)
        {
            for (int j=1; j < (col-1); j++)
            {
                cell[j, (rowToBeCleared-1)].Background = this.background;
            }
            shiftRows(rowToBeCleared-1);
        }

        private void shiftRows(int rowToStartAt)
        {
            for (int i=rowToStartAt; i > 1; i--)
            {
                for (int j = 1; j < (col - 1); j++)
                {
                    cell[j, i].Background = cell[j, (i-1)].Background;
                }
            }
        }

        private void holdPeice()
        {
            TetreminoModel temp;

            if (heldTetremino == null && !swappedThisTurn)
            {
                clearTetremino();
                heldTetremino    = new TetreminoModel(currentTetremino.getType());
                currentTetremino = nextTetremino;
                nextTetremino    = spawnTetromino();
                swappedThisTurn  = true;
                drawTetremino();
                myEvents.getAggregator().PublishOnUIThread(new NextPieceEvent(nextTetremino));
                myEvents.getAggregator().PublishOnUIThread(new HeldPieceEvent(heldTetremino));
            }
            else
            {
                if(moveIsLegal(Move.SPAWN, heldTetremino) && !swappedThisTurn)
                {
                    temp = currentTetremino;
                    clearTetremino();
                    currentTetremino = new TetreminoModel(heldTetremino.getType());
                    heldTetremino = new TetreminoModel(temp.getType());
                    drawTetremino();
                    swappedThisTurn = true;
                    myEvents.getAggregator().PublishOnUIThread(new HeldPieceEvent(heldTetremino));
                }
            }
        }

        private void calculateScore()
        {
            int multiplier = 0;

            switch(clearedRows)
            {
                case int n when (n == 0 || n == 1):
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

            score = (score + (clearedRows * multiplier));
            clearedRows = 0;
            myEvents.getAggregator().PublishOnUIThread(new ScoreEvent(score));
        }

        private void calculateLevel()
        {
            int tempLevel = (score / levelThreshhold);

            if (tempLevel > level)
            {
                level++;
                levelThreshhold = (int)(levelThreshhold * 1.25); 
            }
            myEvents.getAggregator().PublishOnUIThread(new LevelEvent(level));
        }
        private void calculateInterval()
        {
            if (level <= 8 && level > 1)
            {
                interval = (1000 - (level * 80));
                eventTimer.Interval = interval;
            }
        }
    }
}
