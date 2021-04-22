using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxServer.Simulation
{
    public class EntityContainer : ITickable
    {
        public ServerInteractor serverInteractor;

        public EntityRegistry entityRegistry;

        public EntityContainer(ServerInteractor serverInteractor)
        {
            this.serverInteractor = serverInteractor;
            entityRegistry = new EntityRegistry(serverInteractor);
        }

        public void Tick(long id)
        {
            TickAllEntities(id);
        }

        private void TickAllEntities(long id)
        {
            foreach(Entity entity in entityRegistry.entityIdMap.GetAllEntities())
            {
                entity.Tick(id);
            }
        }
    }
}
