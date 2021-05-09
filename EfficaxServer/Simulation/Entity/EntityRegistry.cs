using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxServer.Simulation.Entity
{
    public class EntityRegistry
    {
        public ServerInteractor serverInteractor;

        public EntityIdMap entityIdMap = new EntityIdMap();

        public EntityRegistry(ServerInteractor serverInteractor)
        {
            this.serverInteractor = serverInteractor;
        }
    }
}
