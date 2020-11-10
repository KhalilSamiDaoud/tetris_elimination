using Caliburn.Micro;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for MultiPlayerView.xaml
    /// </summary>
    public partial class MultiPlayerView : UserControl, IHandle<BoardUpdateEvent>
    {
        private Label[,] mp1Cell = new Label[col, row];
        private const int row = 22;
        private const int col = 12;
        private ImageBrush background;
        private ImageBrush border;

        private EventAggregatorModel myEvents;
        public MultiPlayerView()
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            background = new ImageBrush();
            border = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Background_Tile.png", UriKind.Absolute));
            border.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Border_Tile.png", UriKind.Absolute));

            InitializeComponent();
            InitializeBoards();
        }

        private void InitializeBoards()
        {
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    mp1Cell[i, j] = new Label();

                    if (i == 0 || i == 11 || j == 0 || j == 21)
                    {
                        mp1Cell[i, j].Background = border;
                        Grid.SetRow(mp1Cell[i, j], j);
                        Grid.SetColumn(mp1Cell[i, j], i);
                        MultiPlayerGrid0.Children.Add(mp1Cell[i, j]);
                    }
                    else
                    {
                        mp1Cell[i, j].Background = background;
                        Grid.SetRow(mp1Cell[i, j], j);
                        Grid.SetColumn(mp1Cell[i, j], i);
                        MultiPlayerGrid0.Children.Add(mp1Cell[i, j]);
                    }
                }
            }
        }

        private void DecodeGrid(int id, string encodedGrid)
        {
            int i = 1;
            int j = 1;
            for (int k = 0; k < encodedGrid.Length; k++)
            {
                if (encodedGrid[k] == '0')
                {
                    i++;
                    j = 1;
                }
                else
                {
                    mp1Cell[i, j].Background = TILE_ARRAY[encodedGrid[k] - '0'];
                    j++;
                }
            }
        }

        public void Handle(BoardUpdateEvent message)
        {
            DecodeGrid(message.GetID(), message.GetEncodedGrid());
        }
    }
}
