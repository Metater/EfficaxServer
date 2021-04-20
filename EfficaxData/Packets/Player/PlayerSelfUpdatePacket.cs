using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData.Packets.Player
{
    public class PlayerSelfUpdatePacket
    {
        public PositionData playerPos;

        public const ushort PacketId = 50;

        public PlayerSelfUpdatePacket(PositionData playerPos)
        {
            this.playerPos = playerPos;
        }

        public PlayerSelfUpdatePacket(DataReader dataReader)
        {
            FromPacket(dataReader);
        }

        public void FromPacket(DataReader dataReader)
        {
            playerPos = new PositionData(dataReader);
        }

        public byte[] ToPacket()
        {
            DataWriter dataWriter = new DataWriter();
            dataWriter.Put(PacketId);
            dataWriter.Put(playerPos.ToBytes());
            return dataWriter.CopyData();
        }
    }
}
