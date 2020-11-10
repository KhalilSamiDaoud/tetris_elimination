namespace Tetris_Elimination.Networking
{
    public class PacketSend
    {
        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();
            ClientManager.Instance.tcp.SendData(packet);
        }

        public static void WelcomeReceived()
        {
            using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(Properties.Settings.Default.Name);

                SendTCPData(packet);
            }
        }
    }
}
