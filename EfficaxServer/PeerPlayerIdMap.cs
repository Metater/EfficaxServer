using System.Collections.Generic;
using LiteNetLib;

namespace EfficaxServer
{
    public class PeerPlayerIdMap
    {
        private List<(NetPeer, int)> peerPlayerIdMap = new List<(NetPeer, int)>();
        private int nextPlayerId = 0;

        public int AddPeer(NetPeer peer)
        {
            peerPlayerIdMap.Add((peer, nextPlayerId));
            nextPlayerId++;
            return nextPlayerId-1;
        }

        public int GetPlayerId(NetPeer peer)
        {
            foreach ((NetPeer, int) peerPlayerId in peerPlayerIdMap)
            {
                if (peerPlayerId.Item1 == peer)
                    return peerPlayerId.Item2;
            }
            return -1;
        }
        public NetPeer GetPeer(int playerId)
        {
            foreach ((NetPeer, int) peerPlayerId in peerPlayerIdMap)
            {
                if (peerPlayerId.Item2 == playerId)
                    return peerPlayerId.Item1;
            }
            return null;
        }

        public void RemovePlayerId(int playerId)
        {
            for (int i = 0; i < peerPlayerIdMap.Count; i++)
            {
                (NetPeer, int) peerPlayerId = peerPlayerIdMap[i];
                if (peerPlayerId.Item2 == playerId)
                {
                    peerPlayerIdMap.Remove(peerPlayerId);
                    break;
                }
            }
        }
        public void RemovePeer(NetPeer peer)
        {
            for (int i = 0; i < peerPlayerIdMap.Count; i++)
            {
                (NetPeer, int) peerPlayerId = peerPlayerIdMap[i];
                if (peerPlayerId.Item1 == peer)
                {
                    peerPlayerIdMap.Remove(peerPlayerId);
                    break;
                }
            }
        }
    }
}
