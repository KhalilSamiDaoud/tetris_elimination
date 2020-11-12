using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Tetris_Elimination.Networking;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for MultiPlayerView.xaml
    /// </summary>
    public partial class MultiPlayerView : UserControl, IHandle<BoardUpdateEvent>
    {
        private Dictionary<int, int> playerIdToBoard;
        private List<Grid> multiplayerGrids;
        private List<Label[,]> multiplayerCells;
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
            border     = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Background_Tile.png", UriKind.Absolute));
            border.ImageSource     = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Border_Tile.png", UriKind.Absolute));

            multiplayerGrids = new List<Grid>();
            multiplayerCells = new List<Label[,]>();
            playerIdToBoard  = new Dictionary<int, int>();

            InitializeComponent();

            multiplayerGrids.Add(MultiPlayerGrid0);
            multiplayerGrids.Add(MultiPlayerGrid1);
            multiplayerGrids.Add(MultiPlayerGrid2);
            multiplayerGrids.Add(MultiPlayerGrid3);

            multiplayerCells.Add(new Label[col, row]);
            multiplayerCells.Add(new Label[col, row]);
            multiplayerCells.Add(new Label[col, row]);
            multiplayerCells.Add(new Label[col, row]);

            InitializeBoards();
            MapPlayers();
        }

        private void InitializeBoards()
        {
            for (int i = 0; i < multiplayerGrids.Count; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    for (int k = 0; k < row; k++)
                    {
                        multiplayerCells[i][j, k] = new Label();

                        if (j == 0 || j == 11 || k == 0 || k == 21)
                        {
                            multiplayerCells[i][j, k].Background = border;
                            Grid.SetRow(multiplayerCells[i][j, k], k);
                            Grid.SetColumn(multiplayerCells[i][j, k], j);
                            multiplayerGrids[i].Children.Add(multiplayerCells[i][j, k]);
                        }
                        else
                        {
                            multiplayerCells[i][j, k].Background = background;
                            Grid.SetRow(multiplayerCells[i][j, k], k);
                            Grid.SetColumn(multiplayerCells[i][j, k], j);
                            multiplayerGrids[i].Children.Add(multiplayerCells[i][j, k]);
                        }
                    }
                }
            }
        }

        private void MapPlayers()
        {
            int i = -1;

            foreach (KeyValuePair<int, PlayerInstance> entry in ClientManager.Instance.playersInSession)
            {
                playerIdToBoard.Add(entry.Value.UID, i);
                i++;
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
                    multiplayerCells[playerIdToBoard[id]][i, j].Background = TILE_ARRAY[encodedGrid[k] - '0'];
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
