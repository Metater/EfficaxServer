using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxServer.Simulation
{
    public class EntityManager
    {
        public ServerInteractor serverInteractor;

        public EntityRegistry entityRegistry = new EntityRegistry();

        public EntityManager(ServerInteractor serverInteractor)
        {
            this.serverInteractor = serverInteractor;
        }
    }
}
