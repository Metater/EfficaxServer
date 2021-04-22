using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using EfficaxData;
using EfficaxServer.Simulation;

namespace EfficaxServer
{
    public class ServerInteractor
    {
        public ServerPacketRouter packetRouter;
        public ServerNetworkInteractor networkInteractor;
        public PeerPlayerIdMap peerPlayerIdMap;
        public Dictionary<(int, NetPeer), PositionData> playerPositions;
        public EfficaxSimulation efficaxSimulation;

        public ServerInteractor()
        {
            packetRouter = new ServerPacketRouter(this);
            networkInteractor = new ServerNetworkInteractor();
            peerPlayerIdMap = new PeerPlayerIdMap();
            playerPositions = new Dictionary<(int, NetPeer), PositionData>();
            efficaxSimulation = new EfficaxSimulation(this);
        }
    }
}
