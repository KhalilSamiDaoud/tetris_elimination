namespace Tetris_Elimination.Networking
{
    ///<summary>The PacketSend class is reponsible for assembling packets and sending them to the target client / clients.</summary>
    public class PacketSend
    {

        /// <summary>Sends the welcome received packet and initializes the player instance.</summary>
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

        /// <summary>Sends the clients new status.</summary>
        /// <param name="msg">This clients status.</param>
        public static void ClientStatus(int msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientStatus))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        /// <summary>Sends the the encoded client grid.</summary>
        /// <param name="msg">This clients grid.</param>
        public static void ClientGrid(string msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientGrid))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        /// <summary>Sends the new client score.</summary>
        /// <param name="msg">This clients score.</param>
        public static void ClientScore(int msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientScore))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        /// <summary>Sends the clients new gameover state.</summary>
        /// <param name="msg">if set to <c>true</c> [MSG].</param>
        public static void ClientGameOver(bool msg)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientGameOver))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(msg);

                SendTCPData(packet);
            }
        }

        /// <summary>Sends the clients reconnect request.</summary>
        public static void ClientReconnect()
        {
            using (Packet packet = new Packet((int)ClientPackets.clientReconnect))
            {
                packet.Write(ClientManager.Instance.MyID);

                SendTCPData(packet);
            }
        }

        /// <summary>Sends a new client lobby create request.</summary>
        public static void ClientLobbyCreate()
        {
            using (Packet packet = new Packet((int)ClientPackets.clientLobbyCreate))
            {
                packet.Write(ClientManager.Instance.MyID);

                SendTCPData(packet);
            }
        }

        /// <summary>Sends a new client lobby join request.</summary>
        /// <param name="lobbyID">The lobby identifier.</param>
        public static void ClientLobbyJoin(int lobbyID)
        {
            using (Packet packet = new Packet((int)ClientPackets.clientLobbyJoin))
            {
                packet.Write(ClientManager.Instance.MyID);
                packet.Write(lobbyID);

                SendTCPData(packet);
            }
        }

        /// <summary>Sends the TCP data to the server.</summary>
        /// <param name="packet">The packet to send.</param>
        private static void SendTCPData(Packet packet)
        {
            packet.WriteLength();
            ClientManager.Instance.tcp.SendData(packet);
        }
    }
}
