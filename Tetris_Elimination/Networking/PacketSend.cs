namespace Tetris_Elimination.Networking
{
    public class PacketSend
    {

        public static void WelcomeReceived()
        {
            if (!ClientManager.Instance.IsConnected)
            {
                using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
                {
                    packet.Write(ClientManager.Instance.MyID);
                    packet.Write(Properties.Settings.Default.Name);

                    SendTCPData(packet);
                }

                ClientManager.Instance.IsConnected = true;
            }
        }

        public static void ClientStatus(int msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientStatus))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        public static void ClientGrid(string msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientGrid))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        public static void ClientScore(int msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientScore))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        public static void ClientGameOver(bool msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientGameOver))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        public static void ClientReconnect()
        {
            using (Packet packet = new Packet((int)ClientPackets.clientReconnect))
            {
                packet.Write(ClientManager.Instance.MyID);

                SendTCPData(packet);
            }
        }

        public static void ClientLobbyCreate()
        {
            using (Packet packet = new Packet((int)ClientPackets.clientLobbyCreate))
            {
                packet.Write(ClientManager.Instance.MyID);

                SendTCPData(packet);
            }
        }

        public static void ClientLobbyJoin(int lobbyID)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientLobbyJoin))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(lobbyID);

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
