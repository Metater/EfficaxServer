using System;
using System.Collections.Generic;
using System.Text;
using EfficaxServer.Simulation;
using EfficaxServer.Simulation.Entity.Entities;

namespace EfficaxServer.Simulation.Entity
{
    public class EntityIdMap
    {
        private List<(BaseEntity, int)> entityIdMap = new List<(BaseEntity, int)>();
        private int nextId = 0;

        public int AddEntity(BaseEntity entity)
        {
            entityIdMap.Add((entity, nextId));
            nextId++;
            return nextId - 1;
        }

        public int GetEntityId(BaseEntity entity)
        {
            foreach ((BaseEntity, int) entityId in entityIdMap)
            {
                if (entityId.Item1 == entity)
                    return entityId.Item2;
            }
            return -1;
        }
        public BaseEntity GetEntity(int id)
        {
            foreach ((BaseEntity, int) entityId in entityIdMap)
            {
                if (entityId.Item2 == id)
                    return entityId.Item1;
            }
            return null;
        }

        internal void AddEntity(PlayerEntity playerEntity)
        {
            throw new NotImplementedException();
        }

        public void RemovePlayerId(int id)
        {
            for (int i = 0; i < entityIdMap.Count; i++)
            {
                (BaseEntity, int) entityId = entityIdMap[i];
                if (entityId.Item2 == id)
                {
                    entityIdMap.Remove(entityId);
                    break;
                }
            }
        }
        public void RemoveEntity(BaseEntity entity)
        {
            for (int i = 0; i < entityIdMap.Count; i++)
            {
                (BaseEntity, int) entityId = entityIdMap[i];
                if (entityId.Item1 == entity)
                {
                    entityIdMap.Remove(entityId);
                    break;
                }
            }
        }

        public List<BaseEntity> GetAllEntities()
        {
            List<BaseEntity> allEntities = new List<BaseEntity>();
            foreach((BaseEntity, int) entityId in entityIdMap)
            {
                allEntities.Add(entityId.Item1);
            }
            return allEntities;
        }
    }
}
