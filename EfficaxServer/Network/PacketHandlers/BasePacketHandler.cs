using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using EfficaxData.Packets;
using EfficaxData;

namespace EfficaxServer.Network.PacketHandlers
{
    public abstract class BasePacketHandler
    {
        protected ServerInteractor serverInteractor;

        public void InitBase(ServerInteractor serverInteractor)
        {
            this.serverInteractor = serverInteractor;
        }

        public abstract void Handle(NetPeer peer, DataReader dataReader);
    }
}
