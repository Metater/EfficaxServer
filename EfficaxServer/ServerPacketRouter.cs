using System;
using System.Collections.Generic;
using System.Text;

using LiteNetLib;
using LiteNetLib.Utils;

using EfficaxData;
using EfficaxData.Packets.Chat;
using EfficaxData.Packets.Player;

using EfficaxServer.PacketHandlers.Chat;
using EfficaxServer.PacketHandlers.Player;

namespace EfficaxServer
{
    public class ServerPacketRouter
    {
        public ServerNetworkInteractor networkInteractor;
        public Dictionary<(int, NetPeer), PositionData> playerPositions;

        private ChatSendPacketHandler chatSendPacketHandler = new ChatSendPacketHandler();
        private PlayerSelfUpdatePacketHandler playerSelfUpdatePacketHandler = new PlayerSelfUpdatePacketHandler();

        public ServerPacketRouter(ServerNetworkInteractor networkInteractor, Dictionary<(int, NetPeer), PositionData> playerPositions)
        {
            this.networkInteractor = networkInteractor;
            this.playerPositions = playerPositions;
            Init();
        }

        private void Init()
        {
            chatSendPacketHandler.InitBase(this);
            playerSelfUpdatePacketHandler.InitBase(this);
        }

        public void Route(NetPeer peer, NetDataReader netDataReader)
        {
            DataReader dataReader = new DataReader(netDataReader.GetRemainingBytes());

            while (dataReader.AvailableBytes > 0)
            {
                RouteParentPacket(peer, dataReader);
            }
        }

        private void RouteParentPacket(NetPeer peer, DataReader dataReader)
        {
            ushort packetId = dataReader.GetUShort();

            switch (packetId)
            {
                case ChatSendPacket.PacketId:
                    chatSendPacketHandler.Handle(peer, dataReader);
                    break;
                case PlayerSelfUpdatePacket.PacketId:
                    playerSelfUpdatePacketHandler.Handle(peer, dataReader);
                    break;

                default:
                    Console.WriteLine("Unimplemented packet ID: " + packetId);
                    break;
            }
        }
    }
}
