using EfficaxData;
using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using EfficaxData.Packets;
using EfficaxServer.Network.PacketHandlers;

namespace EfficaxServer.Network
{
    public class PlayerPacketHandler : BasePacketHandler
    {
        public override void Handle(NetPeer peer, DataReader dataReader)
        {
            EfficaxPlayer efficaxPlayer = new EfficaxPlayer(dataReader);

            Console.WriteLine("Received a Player packet: " + efficaxPlayer);

        }
    }
}
