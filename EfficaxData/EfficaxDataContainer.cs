using System;
using System.Collections.Generic;
using EfficaxData.Packets;

namespace EfficaxData
{
    public class EfficaxDataContainer
    {
        public List<EfficaxPlayer> players = new List<EfficaxPlayer>();

        public EfficaxPlayer GetPlayer(string name)
        {
            foreach (EfficaxPlayer player in players)
            {
                if (player.name == name)
                    return player;
            }
            throw new Exception($"Player with name: {name} not found!");
        }

        public EfficaxPlayer GetClosestPlayer(EfficaxPlayer efficaxPlayer)
        {
            EfficaxPlayer closestPlayer = efficaxPlayer;
            float closest = float.MaxValue;

            foreach (EfficaxPlayer player in players)
            {
                if (player != efficaxPlayer)
                {
                    float dist = Math.Distance.GetDistanceSquared(player.position, efficaxPlayer.position);
                    if (closest > dist)
                    {
                        closest = dist;
                        closestPlayer = player;
                    }
                }
            }
            return closestPlayer;
        }
    }
}
