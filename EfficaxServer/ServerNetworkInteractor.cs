using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;

namespace EfficaxServer
{
    public class ServerNetworkInteractor
    {
        public EventBasedNetListener listener;
        public NetManager server;

        public ServerNetworkInteractor()
        {
            listener = new EventBasedNetListener();
            server = new NetManager(listener);
        }

        public void Broadcast(byte[] data, DeliveryMethod deliveryMethod)
        {
            server.SendToAll(data, deliveryMethod);
        }
        public void BroadcastBut(int excludedPeerId, byte[] data, DeliveryMethod deliveryMethod)
        {
            foreach (NetPeer peer in server.ConnectedPeerList)
            {
                if (peer.Id != excludedPeerId)
                {
                    Console.WriteLine("Sent data to peer id: " + peer.Id);
                    peer.Send(data, deliveryMethod);
                }
            }
        }
    }
}
