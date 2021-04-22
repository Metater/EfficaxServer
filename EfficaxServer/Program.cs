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
            //Thread.CurrentThread.Priority = ThreadPriority.Highest;

            ServerInteractor serverInteractor = new ServerInteractor();

            serverInteractor.networkInteractor.server.Start(25566 /* port */);

            Dictionary<NetPeer, string> playerNames = new Dictionary<NetPeer, string>();

            Random playerIdMaker = new Random();

            serverInteractor.efficaxSimulation.entityContainer.entityRegistry.entityIdMap.AddEntity(new Simulation.Entities.PlayerEntity("Gerry boi", new Simulation.EntityData(213123, new Simulation.Types.Vector2(0, 0), new Simulation.Types.Vector2(0, 0))));
            serverInteractor.efficaxSimulation.entityContainer.Tick(21);

            serverInteractor.networkInteractor.listener.ConnectionRequestEvent += request =>
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

            serverInteractor.networkInteractor.listener.PeerConnectedEvent += peer =>
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
                //Console.WriteLine("Received, ID: " + fromPeer.Id);
                serverInteractor.packetRouter.Route(fromPeer, dataReader);
                dataReader.Recycle();
            };


            var t = new Stopwatch();
            t.Start();

            var lastTick = new Stopwatch();
            lastTick.Start();
            long timePerTick = 500000;
            long timerTicks = 0;

            long maxTimeBetweenTick = -100000000;
            long minTimeBetweenTick = 100000000;

            List<long> timeBetweenTicks = new List<long>();

            double Average()
            {
                long sum = 0;
                foreach(long l in timeBetweenTicks)
                {
                    sum += l;
                }
                return sum / ((double)timeBetweenTicks.Count);
            }

            int ticks = 0;

            var workTimer = new Timer((x) => {
                Console.Title = $"[Efficax Server] Port: 12733 TPS: {ticks / 20f} " + 
                $"Deviation: (-{(500000 - minTimeBetweenTick) / 10000f}ms, +{(maxTimeBetweenTick - 500000) / 10000f}ms " +
                $"Average Tick Period: {(Average() / 10000f)}ms";
                ticks = 0;
            }, null, 0, 20000);

            long nextTickId = 0;

            while (!Console.KeyAvailable)
            {
                serverInteractor.networkInteractor.server.PollEvents();
                lastTick.Stop();
                timerTicks += lastTick.ElapsedTicks;
                lastTick.Restart();
                if (timerTicks >= timePerTick)
                {
                    timerTicks -= timePerTick;
                    Tick(nextTickId);
                    nextTickId++;
                }
                Thread.Sleep(1);
            }
            serverInteractor.networkInteractor.server.Stop();

            void Tick(long tickId)
            {
                t.Stop();
                long eTicks = t.ElapsedTicks;
                t.Restart();
                if (eTicks > maxTimeBetweenTick) maxTimeBetweenTick = eTicks;
                if (eTicks < minTimeBetweenTick) minTimeBetweenTick = eTicks;
                timeBetweenTicks.Add(eTicks);
                ticks++;
            }
        }
    }
}
