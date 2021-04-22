using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxServer.Simulation
{
    public abstract class Entity : ITickable
    {
        public EntityData data;

        public virtual void Tick(long id)
        {

        }
    }
}
