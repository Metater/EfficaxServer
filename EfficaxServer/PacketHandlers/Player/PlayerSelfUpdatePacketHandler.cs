using EfficaxData;
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Text;
using EfficaxData.Packets.Player;
using EfficaxData.Data;

namespace EfficaxServer.PacketHandlers.Player
{
    public class PlayerSelfUpdatePacketHandler : PacketHandler
    {
        public override void Handle(NetPeer peer, DataReader dataReader)
        {
            // Later give a server-registered session id, and clean this code up, it smells
            PlayerSelfUpdatePacket playerSelfUpdatePacket = new PlayerSelfUpdatePacket(dataReader);
            foreach (KeyValuePair<(int, NetPeer), PositionData> playerPositionKVP in serverPacketRouter.playerPositions)
            {
                if (playerPositionKVP.Key.Item2 == peer)
                {
                    PlayerUpdatePacket playerUpdatePacket = new PlayerUpdatePacket(playerPositionKVP.Key.Item1, playerSelfUpdatePacket.playerPos);
                    serverPacketRouter.networkInteractor.BroadcastBut(peer.Id, playerUpdatePacket.ToPacket(), DeliveryMethod.Unreliable);
                }
            }
        }
    }
}
