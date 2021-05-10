using System;
using System.Collections.Generic;
using System.Text;
using EfficaxServer.Simulation.Entity;
using LiteNetLib;

namespace EfficaxServer.Simulation.Entity.Entities
{
    public class PlayerEntity : BaseEntity
    {
        public NetPeer peer;
        public int playerId;
        public string name;

        public PlayerEntity(NetPeer peer, int playerId, string name, EntityData data)
        {
            this.peer = peer;
            this.playerId = playerId;
            this.name = name;
            this.data = data;
        }

        public override void Tick(long id)
        {
            data.pos += data.vel;
            Console.WriteLine("Ticked player: " + name);
        }
    }
}
