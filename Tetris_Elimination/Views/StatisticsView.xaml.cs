using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : UserControl, IHandle<HeldPieceEvent>, IHandle<NextPieceEvent>
    {
        private const int row        = 4;
        private const int col        = 4;
        private Brush empty          = Brushes.Transparent;
        private Label[,] heldCell    = new Label[col, row];
        private Label[,] nextCell    = new Label[col, row];
        private EventAggregatorSingleton myEvents;
        public StatisticsView()
        {
            myEvents = EventAggregatorSingleton.Instance;
            myEvents.getAggregator().Subscribe(this);
            InitializeComponent();
            initializeBoards();
        }

        void initializeBoards()
        {
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    heldCell[i, j] = new Label();
                    Grid.SetRow(heldCell[i, j], j);
                    Grid.SetColumn(heldCell[i, j], i);
                    heldPiece.Children.Add(heldCell[i, j]);
                }
            }

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    nextCell[i, j] = new Label();
                    Grid.SetRow(nextCell[i, j], j);
                    Grid.SetColumn(nextCell[i, j], i);
                    nextPiece.Children.Add(nextCell[i, j]);
                }
            }
        }
        void setHeld(TetreminoModel heldTetremino)
        {
            clearCells(heldCell);

            for (int i = 0; i < heldTetremino.getShape().Length; i++)
            {
                if (heldTetremino.getType() == Tetremino.BLUE_I || heldTetremino.getType() == Tetremino.YELLOW_O)
                {
                    heldCell[(int)(heldTetremino.getShape()[i].X - 4),
                    (int)(heldTetremino.getShape()[i].Y)].Background = heldTetremino.getBrush();
                }
                else
                {
                    heldCell[(int)(heldTetremino.getShape()[i].X - 3),
                     (int)(heldTetremino.getShape()[i].Y)].Background = heldTetremino.getBrush();
                }
            }
        }

        void setNext(TetreminoModel nextTetremino)
        {
            clearCells(nextCell);

            for (int i = 0; i < nextTetremino.getShape().Length; i++)
            {
                if (nextTetremino.getType() == Tetremino.BLUE_I || nextTetremino.getType() == Tetremino.YELLOW_O)
                {
                    nextCell[(int)(nextTetremino.getShape()[i].X - 4),
                    (int)(nextTetremino.getShape()[i].Y)].Background = nextTetremino.getBrush();
                }
                else
                {
                    nextCell[(int)(nextTetremino.getShape()[i].X - 3),
                     (int)(nextTetremino.getShape()[i].Y)].Background = nextTetremino.getBrush();
                }
            }
        }

        void clearCells(Label[,] board)
        {
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    board[i, j].Background = empty;
                }
            }
        }

        public void Handle(HeldPieceEvent message)
        {
            if (message.get() == null)
            {
                clearCells(heldCell);
            }
            else
            {
                setHeld(message.get());
            }
        }

        public void Handle(NextPieceEvent message)
        {
            if (message.get() == null)
            {
                clearCells(nextCell);
            }
            else
            {
                setNext(message.get());
            }
        }
    }
}
