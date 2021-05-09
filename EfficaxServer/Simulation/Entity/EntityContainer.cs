using System;
using System.Collections.Generic;
using System.Text;
using EfficaxServer.Simulation.Entity.Entities;
using EfficaxServer.Simulation.Interfaces;

namespace EfficaxServer.Simulation.Entity
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
            foreach(BaseEntity entity in entityRegistry.entityIdMap.GetAllEntities())
            {
                entity.Tick(id);
            }
        }
    }
}
