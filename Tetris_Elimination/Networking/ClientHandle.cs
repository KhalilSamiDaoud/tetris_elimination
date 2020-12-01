using Caliburn.Micro;
using System.Diagnostics;
using Tetris_Elimination.Events;
using Tetris_Elimination.Models;

namespace Tetris_Elimination.Networking
{
    public class ClientHandle
    {
        private static EventAggregatorModel myEvents = EventAggregatorModel.Instance;

        private ClientHandle()
        {
            myEvents.getAggregator().Subscribe(this);
        }

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

        public static void PlayerCountChange(Packet packet)
        {
            string msg = packet.ReadString();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new ServerPlayerCountEvent(id, msg));
        }

        public static void PlayerReadyChange(Packet packet)
        {
            int msg = packet.ReadInt();
            int id   = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new ServerPlayerReadyEvent(id, msg));
        }

        public static void PlayerListToAll(Packet packet)
        {
            int status = packet.ReadInt();
            string msg = packet.ReadString();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new ServerPlayerListEvent(id, status, msg));
        }

        public static void PlayerListToOne(Packet packet)
        {
            int status = packet.ReadInt();
            string msg = packet.ReadString();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new ServerPlayerListEvent(id, status, msg));
        }

        public static void PlayerGrids(Packet packet)
        {
            string msg = packet.ReadString();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new BoardUpdateEvent(id, msg));
        }

        public static void PlayerScore(Packet packet)
        {
            int msg    = packet.ReadInt();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new MultiplayerScoreEvent(id, msg));
        }

        public static void PlayerGameOver(Packet packet)
        {
            bool msg   = packet.ReadBool();
            int id     = packet.ReadInt();

            myEvents.getAggregator().PublishOnUIThread(new MultiplayerGameOverEvent(id, msg));
        }

        public static void StartGame(Packet packet)
        {
            bool isrdy = packet.ReadBool();

            myEvents.getAggregator().PublishOnUIThread(new NewGameEvent());
        }
    }
}
