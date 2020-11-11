namespace Tetris_Elimination.Networking
{
    public class PacketSend
    {
        public static void WelcomeReceived()
        {
            using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(Properties.Settings.Default.Name);

                SendTCPData(packet);
            }
        }

        public static void ClientReady(bool msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientReady))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();
            ClientManager.Instance.tcp.SendData(packet);
        }
    }
}
