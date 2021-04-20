using System;
using System.Collections.Generic;
using System.Text;
using EfficaxData;
using EfficaxData.Packets;

namespace EfficaxServer
{
    public class PlayerDataInteractor
    {
        private EfficaxDataContainer data;
        public PlayerDataInteractor(EfficaxDataContainer data)
        {
            this.data = data;
        }

        public void AddPlayer(EfficaxPlayer efficaxPlayer)
        {
            data.players.Add(efficaxPlayer);
        }
    }
}
