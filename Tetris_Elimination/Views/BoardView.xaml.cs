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
            background.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Background_Tile.png", UriKind.Relative));
            border.ImageSource = new BitmapImage(new Uri("C:/Users/khalil/source/repos/Tetris_Elimination/Tetris_Elimination/Assets/Images/Border_Tile.png", UriKind.Relative));
            cell = new Label[col, row];

            for (int i = 0; i < col; i++) 
            {
                for (int j = 0; j < row; j++) 
                {

                    cell[i, j] = new Label();
                    if ( i == 0 || i == 11 || j == 0 || j == 21)
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

        void startGame()
        {
            currentTetremino = spawnTetromino();
            drawTetremino();

            eventTimer = new Timer();
            eventTimer.Elapsed += new ElapsedEventHandler(gameLoop);
            eventTimer.Interval = interval;
            eventTimer.Start();
        }

        TetreminoModel spawnTetromino()
        {
            return new TetreminoModel((Tetremino)tetreminoTypes.GetValue(rand.Next(tetreminoTypes.Length)));
        }

        void drawTetremino()
        {
           for(int i=0; i < currentTetremino.getShape().Length; i++)
           {
                cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X) + COL_OFFSET, 
                    (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y) + ROW_OFFSET].Background = currentTetremino.getBrush();
            }
        }

        void clearTetremino()
        {
            for (int i = 0; i < currentTetremino.getShape().Length; i++)
            {
                cell[(int)(currentTetremino.getShape()[i].X + currentTetremino.getPosition().X) + COL_OFFSET,
                    (int)(currentTetremino.getShape()[i].Y + currentTetremino.getPosition().Y) + ROW_OFFSET].Background = background;
            }
        }

        void movedown()
        {
            if (moveIsLegal(Move.DOWN))
            {
                clearTetremino();
                currentTetremino.move(Move.DOWN);
            }
        }

        Boolean moveIsLegal(Move direction)
        {
            return true;
        }

        private void gameLoop(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                movedown();
                drawTetremino();
            });
        }

    }
}
