using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxServer.Simulation.Entities
{
    public class PlayerEntity : Entity
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
