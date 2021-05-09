using System;
using System.Collections.Generic;
using System.Text;
using EfficaxServer.Simulation.Entity;

namespace EfficaxServer.Simulation.Entity.Entities
{
    public class PlayerEntity : BaseEntity
    {
        public string name;

        public PlayerEntity(string name, EntityData data)
        {
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
