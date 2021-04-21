using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using EfficaxData;

namespace EfficaxServer
{
    public class ServerInteractor
    {
        public ServerPacketRouter packetRouter;
        public ServerNetworkInteractor networkInteractor;
        public PeerPlayerIdMap peerPlayerIdMap;
        public Dictionary<(int, NetPeer), PositionData> playerPositions;

        public void Load(ServerPacketRouter packetRouter, ServerNetworkInteractor networkInteractor, PeerPlayerIdMap peerPlayerIdMap, Dictionary<(int, NetPeer), PositionData> playerPositions)
        {
            this.packetRouter = packetRouter;
            this.networkInteractor = networkInteractor;
            this.peerPlayerIdMap = peerPlayerIdMap;
            this.playerPositions = playerPositions;
        }
    }
}
