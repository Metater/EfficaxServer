using EfficaxData;
using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using EfficaxData.Packets;

namespace EfficaxServer
{
    public class PlayerPacketHandler : PacketHandler
    {
        public override void Handle(NetPeer peer, DataReader dataReader)
        {
            EfficaxPlayer efficaxPlayer = new EfficaxPlayer(dataReader);

            Console.WriteLine("Received a Player packet: " + efficaxPlayer);

        }
    }
}
