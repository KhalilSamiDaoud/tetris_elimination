using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Tetris_Elimination.Networking;
using static Tetris_Elimination.Models.ConstantsModel;

namespace Tetris_Elimination.Views
{
    /// <summary>
    /// Interaction logic for MultiPlayerView.xaml
    /// </summary>
    public partial class MultiPlayerView : UserControl, IHandle<BoardUpdateEvent>, IHandle<MultiplayerScoreEvent>, IHandle<MultiplayerGameOverEvent>
    {
        private Dictionary<int, int> playerIdToBoard;
        private List<StackPanel> multiplayerLost;
        private List<TextBlock> multiplayerInfo;
        private List<Label[,]> multiplayerCells;
        private List<Grid> multiplayerGrids;
        private ImageBrush background;
        private ImageBrush border;
        private const int row = 22;
        private const int col = 12;

        private EventAggregatorModel myEvents;
        public MultiPlayerView()
        {
            myEvents = EventAggregatorModel.Instance;
            myEvents.getAggregator().Subscribe(this);

            background = new ImageBrush();
            border     = new ImageBrush();
            background = BACKGROUND_TILE;
            border     = BORDER_TILE;

            InitializeComponent();

            InitializeLists();
            MapPlayers();
            InitializeBoards();
        }

        private void InitializeBoards()
        {
            for (int i = 0; i < multiplayerGrids.Count; i++)
            {
                if(playerIdToBoard.ContainsValue(i))
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
        }

        private void InitializeLists()
        {
            multiplayerGrids = new List<Grid>();
            multiplayerCells = new List<Label[,]>();
            multiplayerLost  = new List<StackPanel>();
            multiplayerInfo  = new List<TextBlock>();
            playerIdToBoard  = new Dictionary<int, int>();

            multiplayerGrids.Add(MultiPlayerGrid0);
            multiplayerGrids.Add(MultiPlayerGrid1);
            multiplayerGrids.Add(MultiPlayerGrid2);
            multiplayerGrids.Add(MultiPlayerGrid3);

            multiplayerInfo.Add(MultiPlayerInfo0);
            multiplayerInfo.Add(MultiPlayerInfo1);
            multiplayerInfo.Add(MultiPlayerInfo2);
            multiplayerInfo.Add(MultiPlayerInfo3);

            multiplayerLost.Add(MultiPlayerLost0);
            multiplayerLost.Add(MultiPlayerLost1);
            multiplayerLost.Add(MultiPlayerLost2);
            multiplayerLost.Add(MultiPlayerLost3);

            multiplayerCells.Add(new Label[col, row]);
            multiplayerCells.Add(new Label[col, row]);
            multiplayerCells.Add(new Label[col, row]);
            multiplayerCells.Add(new Label[col, row]);
        }

        private void MapPlayers()
        {
            int i = -1;

            foreach (KeyValuePair<int, PlayerInstance> entry in ClientManager.Instance.playersInSession)
            {
                playerIdToBoard.Add(entry.Value.UID, i);

                if (i >= 0)
                {
                    multiplayerInfo[i].Visibility = System.Windows.Visibility.Visible;
                    multiplayerInfo[i].Text       = entry.Value.UserName + " : " + "0"; 
                }

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

        public void Handle(MultiplayerScoreEvent message)
        {
            multiplayerInfo[playerIdToBoard[message.GetID()]].Text = ClientManager.Instance.playersInSession[message.GetID()].UserName + " : " + message.GetScore().ToString();
        }

        public void Handle(MultiplayerGameOverEvent message)
        {
            multiplayerLost[playerIdToBoard[message.GetID()]].Visibility = System.Windows.Visibility.Visible;
        }
    }
}
