using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxServer.Simulation.Entities
{
    public class PlayerEntity : Entity, ITickable
    {
        public string name;

        public PlayerEntity(string name, EntityData data)
        {
            this.name = name;
            this.data = data;
        }

        public void Tick()
        {
            data.Tick();
        }
    }
}
