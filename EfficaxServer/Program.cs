using System;
using System.Collections.Generic;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;
using EfficaxData;
using EfficaxData.Packets;
using EfficaxData.Packets.Player;
using System.Diagnostics;

namespace EfficaxServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerInteractor serverInteractor = new ServerInteractor();

            ServerPacketRouter packetRouter = new ServerPacketRouter(serverInteractor);
            ServerNetworkInteractor networkInteractor = new ServerNetworkInteractor();
            PeerPlayerIdMap peerPlayerIdMap = new PeerPlayerIdMap();
            Dictionary<(int, NetPeer), PositionData> playerPositions = new Dictionary<(int, NetPeer), PositionData>();


            networkInteractor.server.Start(25566 /* port */);

            Dictionary<NetPeer, string> playerNames = new Dictionary<NetPeer, string>();

            serverInteractor.Load(packetRouter, networkInteractor, peerPlayerIdMap, playerPositions);

            Random playerIdMaker = new Random();

            networkInteractor.listener.ConnectionRequestEvent += request =>
            {
                NetPeer peer = request.Accept();

                playerNames.Add(peer, request.Data.GetString());

                /*
                if (server.ConnectedPeersCount < 10)
                    request.AcceptIfKey("SomeConnectionKey");
                else
                    request.Reject();
                */
            };

            networkInteractor.listener.PeerConnectedEvent += peer =>
            {
                /*
                Console.WriteLine("We got connection: {0}", peer.EndPoint); // Show peer ip
                NetDataWriter writer = new NetDataWriter();                 // Create writer class
                EfficaxPlayer efficaxPlayer = new EfficaxPlayer(new PositionData(32, 0), $"Jimmy {networkInteractor.server.GetPeersCount(ConnectionState.Any)}");
                byte[] data = efficaxPlayer.ToPacket();
                writer.Put(data);
                Console.WriteLine(Convert.ToBase64String(data));
                peer.Send(writer, DeliveryMethod.ReliableOrdered);             // Send with reliability
                */
                foreach (KeyValuePair<(int, NetPeer), PositionData> playerPositionKVP in playerPositions)
                {
                    PlayerJoinPacket previousPlayer = new PlayerJoinPacket(playerPositionKVP.Key.Item1, playerNames[playerPositionKVP.Key.Item2], playerPositionKVP.Value);
                    peer.Send(previousPlayer.ToPacket(), DeliveryMethod.ReliableOrdered);
                }

                int playerId = playerIdMaker.Next();
                PositionData playerPos = new PositionData(0, 0);
                playerPositions.Add((playerId, peer), playerPos);
                PlayerJoinPacket playerJoinPacket = new PlayerJoinPacket(playerId, playerNames[peer], playerPos);
                networkInteractor.BroadcastBut(peer.Id, playerJoinPacket.ToPacket(), DeliveryMethod.ReliableOrdered);
            };

            networkInteractor.listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {
                //Console.WriteLine("Received, ID: " + fromPeer.Id);
                packetRouter.Route(fromPeer, dataReader);
                dataReader.Recycle();
            };


            var t = new Stopwatch();
            t.Start();

            var lastTick = new Stopwatch();
            lastTick.Start();
            long timePerTick = 50;
            long timerTicks = 0;
            long timerTicksReset = 0;

            int ticks = 0;

            var workTimer = new Timer((x) => {
                Console.WriteLine(ticks);
                ticks = 0;
            }, null, 0, 1000);

            while (!Console.KeyAvailable)
            {
                networkInteractor.server.PollEvents();
                timerTicks += lastTick.ElapsedMilliseconds;
                if (timerTicks >= timePerTick)
                {
                    timerTicks = timerTicksReset;
                    Tick();
                    lastTick.Restart();
                }
                Thread.Sleep(1);
            }
            networkInteractor.server.Stop();

            void TryTick()
            {
                lastTick.Stop();
                if (lastTick.ElapsedTicks > 50)
                {
                    lastTick.Restart();
                    Tick();
                }
            }

            void Tick()
            {
                //Console.WriteLine("Tick: " + t.ElapsedMilliseconds + ":::" + t.ElapsedTicks);
                t.Restart();
                ticks++;
            }
        }
    }
}
