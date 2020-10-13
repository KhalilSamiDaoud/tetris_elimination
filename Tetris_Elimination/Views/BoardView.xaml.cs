using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private int row = 22;
        private int col = 12;
        private int interval;
        private bool gameOver = false;
        private bool gameStarted = false;
        private Array tetreminoTypes = Enum.GetValues(typeof(Tetremino));
        private Label[,] cell;
        private ImageBrush background = new ImageBrush();
        private ImageBrush border = new ImageBrush();
        private Random rand = new Random();
        private TetreminoModel currentTetremino;
        private static Timer eventTimer;

        public BoardView()
        {

            InitializeComponent();
            interval = 1000;
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Background_Tile.png", UriKind.Absolute));
            border.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Border_Tile.png", UriKind.Absolute));
            cell = new Label[col, row];

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

            startGame();

        }

        private void BoardView_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += BoardView_KeyDown;
        }

        private void startGame()
        {
            currentTetremino = spawnTetromino();
            drawTetremino();

            eventTimer = new Timer();
            eventTimer.Elapsed += new ElapsedEventHandler(gameLoop);
            eventTimer.Interval = interval;
            eventTimer.Start();
            gameStarted = true;
        }

        public bool getGameStarted()
        {
            return gameStarted;
        }

        private TetreminoModel spawnTetromino()
        {
            return new TetreminoModel((Tetremino)tetreminoTypes.GetValue(rand.Next(tetreminoTypes.Length)));
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
                    (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y)].Background = background;
            }
        }

        public void moveDown()
        {
            if (moveIsLegal(Move.DOWN))
            {
                clearTetremino();
                currentTetremino.move(Move.DOWN);
                drawTetremino();
            }
            else
            {
                currentTetremino = spawnTetromino();
                drawTetremino();
            }
        }

        public void moveRight()
        {
            if (moveIsLegal(Move.RIGHT))
            {
                clearTetremino();
                currentTetremino.move(Move.RIGHT);
                drawTetremino();
            }
        }

        public void moveLeft()
        {
            if (moveIsLegal(Move.LEFT))
            {
                clearTetremino();
                currentTetremino.move(Move.LEFT);
                drawTetremino();
            }
        }

        public void Rotate()
        {
            if (moveIsLegal(Move.ROTATE))
            {
                clearTetremino();
                currentTetremino.move(Move.ROTATE);
                drawTetremino();
            }
        }

        Boolean moveIsLegal(Move direction)
        {
            switch(direction)
            {
                case Move.DOWN:
                    for (int i = 0; i < currentTetremino.getShape().Length; i++)
                    {
                        if (!cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X),
                            (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y + 1)].Background.Equals(this.background) &&
                            !cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X),
                            (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y + 1)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                case Move.RIGHT:
                    for (int i = 0; i < currentTetremino.getShape().Length; i++)
                    {
                        if (!cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X + 1),
                            (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y)].Background.Equals(this.background) &&
                            !cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X + 1),
                            (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                case Move.LEFT:
                    for (int i = 0; i < currentTetremino.getShape().Length; i++)
                    {
                        if (!cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X - 1),
                            (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y)].Background.Equals(this.background) &&
                            !cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X - 1),
                            (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                case Move.ROTATE:

                    Point[] tempShape = currentTetremino.getShape();
                    Point tempPoint;

                    for (int i = 0; i < currentTetremino.getShape().Length; i++)
                    {
                        tempPoint = currentTetremino.rotatePoint(tempShape[i], tempShape[2]);

                        if (!cell[(int)(tempShape[i].X + currentTetremino.getPosition().X),
                            (int)(tempShape[i].Y + currentTetremino.getPosition().Y)].Background.Equals(this.background) &&
                            !cell[(int)(tempShape[i].X + currentTetremino.getPosition().X),
                            (int)(tempShape[i].Y + currentTetremino.getPosition().Y)].Background.Equals(currentTetremino.getBrush()))
                        {
                            return false;
                        }
                    }
                    break;
                default:
                    break;
            }
            return true;
        }

        private void gameLoop(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                moveDown();
            });
        }

        private void BoardView_KeyDown(object sender, KeyEventArgs e)
        {
            if (getGameStarted())
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
                    case Key.X:
                        Rotate();
                        break;
                    case Key.C:
                        moveRight();
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

    }
}
