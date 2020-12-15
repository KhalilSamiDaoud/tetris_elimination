using Tetris_Elimination.Events;
using Tetris_Elimination.Models;
using Caliburn.Micro;


namespace Tetris_Elimination.Networking
{
    /// <summary>The ClientHandle class is used to handle incoming packets. Events are raised by packet types.</summary>
    public class ClientHandle
    {
        private static EventAggregatorModel myEvents = EventAggregatorModel.Instance;

        /// <summary>Prevents a default instance of the <see cref="ClientHandle" /> class from being created.</summary>
        private ClientHandle()
        {
            myEvents.getAggregator().Subscribe(this);
        }

        /// <summary>Receives the Welcome Event. Add this client to the playersInSession.</summary>
        /// <param name="packet">The packet.</param>
        public static void Welcome(Packet packet)
        {
            string msg = packet.ReadString();
            int id     = packet.ReadInt();

            ClientManager.Instance.MyID = id;
            PacketSend.WelcomeReceived();

            if (!ClientManager.Instance.playersInSession.ContainsKey(id))
            {
                ClientManager.Instance.playersInSession.Add(id, new PlayerInstance(id, Properties.Settings.Default.Name, 0));
            }

            myEvents.getAggregator().PublishOnUIThread(new ServerInformationEvent(msg));
        }

        /// <summary>Informs client of a new player count.</summary>
        /// <param name="packet">The packet.</param>
        public static void PlayerCountChange(Packet packet)
        {
            string msg = packet.ReadString();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new ServerPlayerCountEvent(id, msg));
        }

        /// <summary>Informs the client of a new player status.</summary>
        /// <param name="packet">The packet.</param>
        public static void PlayerReadyChange(Packet packet)
        {
            int msg = packet.ReadInt();
            int id   = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new ServerPlayerReadyEvent(id, msg));
        }

        /// <summary>Informs the client of the new player list.</summary>
        /// <param name="packet">The incoming packet.</param>
        public static void PlayerList(Packet packet)
        {
            int status = packet.ReadInt();
            string msg = packet.ReadString();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new ServerPlayerListEvent(id, status, msg));
        }

        /// <summary>Informs the client of new lobby lists.</summary>
        /// <param name="packet">The incoming packet.</param>
        public static void LobbyList(Packet packet)
        {
            int id      = packet.ReadInt();
            string name = packet.ReadString();
            int count   = packet.ReadInt();
            bool full   = packet.ReadBool();
            int max     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new ServerLobbyListEvent(id, name, count, full, max));
        }

        /// <summary>Informs this client of new Player Grids.</summary>
        /// <param name="packet">The incoming packet.</param>
        public static void PlayerGrids(Packet packet)
        {
            string msg = packet.ReadString();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new BoardUpdateEvent(id, msg));
        }

        /// <summary>Informs this client of a players new score.</summary>
        /// <param name="packet">The incoming packet.</param>
        public static void PlayerScore(Packet packet)
        {
            int msg    = packet.ReadInt();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new MultiplayerScoreEvent(id, msg));
        }

        /// <summary>Informs this client of a player game over.</summary>
        /// <param name="packet">The incoming packet.</param>
        public static void PlayerGameOver(Packet packet)
        {
            bool msg   = packet.ReadBool();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new MultiplayerGameOverEvent(id, msg));
        }

        /// <summary>Starts the game.</summary>
        /// <param name="packet">The incoming packet.</param>
        public static void StartGame(Packet packet)
        {
            bool isrdy = packet.ReadBool();

            myEvents.getAggregator().PublishOnUIThread(new MultiplayerNewGameEvent());
        }

        /// <summary>Disconnects the client from the server.</summary>
        /// <param name="packet">The incoming packet.</param>
        public static void ServerDisconnect(Packet packet)
        {
            bool isded = packet.ReadBool();

            myEvents.getAggregator().PublishOnUIThread(new ServerDisconnectEvent());
        }
    }
}
