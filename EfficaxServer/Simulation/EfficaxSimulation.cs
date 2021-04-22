using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxServer.Simulation
{
    public class EfficaxSimulation : ITickable
    {
        public ServerInteractor serverInteractor;

        public EntityContainer entityContainer;

        public EfficaxSimulation(ServerInteractor serverInteractor)
        {
            this.serverInteractor = serverInteractor;
            entityContainer = new EntityContainer(serverInteractor);
        }

        public void Tick(long id)
        {
            entityContainer.Tick(id);
        }
    }
}
