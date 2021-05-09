using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using EfficaxData;
using EfficaxData.Packets.Chat;

namespace EfficaxServer.Network.PacketHandlers.Chat
{
    public class ChatSendPacketHandler : BasePacketHandler
    {
        public override void Handle(NetPeer peer, DataReader dataReader)
        {
            ChatSendPacket chatSendPacket = new ChatSendPacket(dataReader);
            ChatReceivePacket chatReceivePacket = new ChatReceivePacket(chatSendPacket.message);
            serverInteractor.networkInteractor.BroadcastBut(peer.Id, chatReceivePacket.ToPacket(), DeliveryMethod.ReliableOrdered);
        }
    }
}
