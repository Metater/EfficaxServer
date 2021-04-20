using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using EfficaxData;
using EfficaxData.Packets.Chat;

namespace EfficaxServer.PacketHandlers.Chat
{
    public class ChatSendPacketHandler : PacketHandler
    {
        public override void Handle(NetPeer peer, DataReader dataReader)
        {
            ChatSendPacket chatSendPacket = new ChatSendPacket(dataReader);
            ChatReceivePacket chatReceivePacket = new ChatReceivePacket(chatSendPacket.message);
            serverPacketRouter.networkInteractor.BroadcastBut(peer.Id, chatReceivePacket.ToPacket(), DeliveryMethod.ReliableOrdered);
        }
    }
}
