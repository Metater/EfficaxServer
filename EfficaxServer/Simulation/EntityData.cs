using System;
using System.Collections.Generic;
using System.Text;
using EfficaxServer.Simulation.Types;

namespace EfficaxServer.Simulation
{
    public class EntityData
    {
        public int id;
        public Vector2 pos;
        public Vector2 vel;

        public EntityData(int id, Vector2 pos, Vector2 vel)
        {
            this.id = id;
            this.pos = pos;
            this.vel = vel;
        }
    }
}
