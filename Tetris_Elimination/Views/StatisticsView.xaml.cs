using static Tetris_Elimination.Models.ConstantsModel;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using System.Windows.Controls;
using System.Windows.Media;
using Caliburn.Micro;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml, Breaking MVVM as CM does not support binding to grids
    /// </summary>
    public partial class StatisticsView : UserControl, IHandle<HeldPieceEvent>, IHandle<NextPieceEvent>
    {
        private const int row        = 4;
        private const int col        = 4;
        private Brush empty          = Brushes.Transparent;
        private Label[,] heldCell    = new Label[col, row];
        private Label[,] nextCell    = new Label[col, row];
        private EventAggregatorModel myEvents;
        public StatisticsView()
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            InitializeComponent();
            InitializeBoards();
        }

        private void InitializeBoards()
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
        private void SetHeld(TetreminoModel heldTetremino)
        {
            ClearCells(heldCell);

            for (int i = 0; i < heldTetremino.GetShape().Length; i++)
            {
                if (heldTetremino.GetType() == Tetremino.BLUE_I || heldTetremino.GetType() == Tetremino.YELLOW_O)
                {
                    heldCell[(int)(heldTetremino.GetShape()[i].X - 4),
                    (int)(heldTetremino.GetShape()[i].Y)].Background = heldTetremino.GetBrush();
                }
                else
                {
                    heldCell[(int)(heldTetremino.GetShape()[i].X - 3),
                     (int)(heldTetremino.GetShape()[i].Y)].Background = heldTetremino.GetBrush();
                }
            }
        }

        private void SetNext(TetreminoModel nextTetremino)
        {
            ClearCells(nextCell);

            for (int i = 0; i < nextTetremino.GetShape().Length; i++)
            {
                if (nextTetremino.GetType() == Tetremino.BLUE_I || nextTetremino.GetType() == Tetremino.YELLOW_O)
                {
                    nextCell[(int)(nextTetremino.GetShape()[i].X - 4),
                    (int)(nextTetremino.GetShape()[i].Y)].Background = nextTetremino.GetBrush();
                }
                else
                {
                    nextCell[(int)(nextTetremino.GetShape()[i].X - 3),
                     (int)(nextTetremino.GetShape()[i].Y)].Background = nextTetremino.GetBrush();
                }
            }
        }

        private void ClearCells(Label[,] board)
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
            if (message.Get() == null)
            {
                ClearCells(heldCell);
            }
            else
            {
                SetHeld(message.Get());
            }
        }

        public void Handle(NextPieceEvent message)
        {
            if (message.Get() == null)
            {
                ClearCells(nextCell);
            }
            else
            {
                SetNext(message.Get());
            }
        }
    }
}
