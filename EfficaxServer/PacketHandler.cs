using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using EfficaxData.Packets;
using EfficaxData;

namespace EfficaxServer
{
    public abstract class PacketHandler
    {
        protected ServerPacketRouter serverPacketRouter;

        public void InitBase(ServerPacketRouter serverPacketRouter)
        {
            this.serverPacketRouter = serverPacketRouter;
        }

        public abstract void Handle(NetPeer peer, DataReader dataReader);
    }
}
