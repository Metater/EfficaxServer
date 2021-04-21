using System;
using System.Collections.Generic;
using System.Text;
using EfficaxServer.Simulation;

namespace EfficaxServer.Simulation
{
    public class EntityIdMap
    {
        private List<(Entity, int)> entityIdMap = new List<(Entity, int)>();
        private int nextId = 0;

        public int AddEntity(Entity entity)
        {
            entityIdMap.Add((entity, nextId));
            nextId++;
            return nextId - 1;
        }

        public int GetEntityId(Entity entity)
        {
            foreach ((Entity, int) entityId in entityIdMap)
            {
                if (entityId.Item1 == entity)
                    return entityId.Item2;
            }
            return -1;
        }
        public Entity GetEntity(int id)
        {
            foreach ((Entity, int) entityId in entityIdMap)
            {
                if (entityId.Item2 == id)
                    return entityId.Item1;
            }
            return null;
        }

        public void RemovePlayerId(int id)
        {
            for (int i = 0; i < entityIdMap.Count; i++)
            {
                (Entity, int) entityId = entityIdMap[i];
                if (entityId.Item2 == id)
                {
                    entityIdMap.Remove(entityId);
                    break;
                }
            }
        }
        public void RemoveEntity(Entity entity)
        {
            for (int i = 0; i < entityIdMap.Count; i++)
            {
                (Entity, int) entityId = entityIdMap[i];
                if (entityId.Item1 == entity)
                {
                    entityIdMap.Remove(entityId);
                    break;
                }
            }
        }

        public List<Entity> GetAllEntities()
        {
            List<Entity> allEntities = new List<Entity>();
            foreach((Entity, int) entityId in entityIdMap)
            {
                allEntities.Add(entityId.Item1);
            }
            return allEntities;
        }
    }
}
