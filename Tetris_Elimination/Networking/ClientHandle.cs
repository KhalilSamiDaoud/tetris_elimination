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

            Debug.WriteLine(msg);

            myEvents.getAggregator().PublishOnUIThread(new ServerInformationEvent(msg));
        }
    }
}
