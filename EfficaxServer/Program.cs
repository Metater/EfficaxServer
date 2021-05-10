using System;
using System.Collections.Generic;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;
using EfficaxData;
using EfficaxData.Packets;
using EfficaxData.Packets.Player;
using System.Diagnostics;
using EfficaxServer.Simulation.Entity.Entities;

namespace EfficaxServer
{
    class Program
    {
        public const long TimePerTick = 400000;

        static void Main(string[] args)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            ServerInteractor serverInteractor = new ServerInteractor();

            serverInteractor.networkInteractor.server.Start(25566 /* port */);

            Dictionary<NetPeer, string> playerNames = new Dictionary<NetPeer, string>();

            Random playerIdMaker = new Random();

            serverInteractor.efficaxSimulation.entityContainer.entityRegistry.entityIdMap.AddEntity(new PlayerEntity(null, 1, "Gerry boi", new Simulation.Entity.EntityData(213123, new Simulation.Types.Vector2(0, 0), new Simulation.Types.Vector2(0, 0))));
            serverInteractor.efficaxSimulation.entityContainer.Tick(21);

            serverInteractor.networkInteractor.listener.ConnectionRequestEvent += request =>
            {
                NetPeer peer = request.Accept();

                playerNames.Add(peer, request.Data.GetString());
            };

            serverInteractor.networkInteractor.listener.PeerConnectedEvent += peer =>
            {
                foreach (KeyValuePair<(int, NetPeer), PositionData> playerPositionKVP in serverInteractor.playerPositions)
                {
                    PlayerJoinPacket previousPlayer = new PlayerJoinPacket(playerPositionKVP.Key.Item1, playerNames[playerPositionKVP.Key.Item2], playerPositionKVP.Value);
                    peer.Send(previousPlayer.ToPacket(), DeliveryMethod.ReliableOrdered);
                }

                int playerId = playerIdMaker.Next();
                PositionData playerPos = new PositionData(0, 0);
                serverInteractor.playerPositions.Add((playerId, peer), playerPos);
                PlayerJoinPacket playerJoinPacket = new PlayerJoinPacket(playerId, playerNames[peer], playerPos);
                serverInteractor.networkInteractor.BroadcastBut(peer.Id, playerJoinPacket.ToPacket(), DeliveryMethod.ReliableOrdered);
            };

            serverInteractor.networkInteractor.listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {
                serverInteractor.packetRouter.Route(fromPeer, dataReader);
                dataReader.Recycle();
            };

            var lastTick = new Stopwatch();
            lastTick.Start();
            long timerTicks = 0;

            long nextTickId = 0;

            //Console.Title = $"[Efficax Server] Port: 12733";
            Console.Title = $"[Efficax Server] Port: 25566";

            while (!Console.KeyAvailable)
            {
                serverInteractor.networkInteractor.server.PollEvents();
                lastTick.Stop();
                timerTicks += lastTick.ElapsedTicks;
                lastTick.Restart();
                if (timerTicks >= TimePerTick)
                {
                    timerTicks -= TimePerTick;
                    Tick(nextTickId);
                    nextTickId++;
                }
                Thread.Sleep(1);
            }
            serverInteractor.networkInteractor.server.Stop();

            void Tick(long tickId)
            {

            }
        }
    }
}
