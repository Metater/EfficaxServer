using System;
using System.Collections.Generic;
using System.Text;
using EfficaxServer.Simulation.Entity;
using EfficaxServer.Simulation.Interfaces;

namespace EfficaxServer.Simulation.Entity.Entities
{
    public abstract class BaseEntity : ITickable
    {
        public EntityData data;

        public virtual void Tick(long id)
        {

        }
    }
}
