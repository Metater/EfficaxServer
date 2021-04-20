using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData.Packets.Player
{
    public class PlayerUpdatePacket
    {
        public int playerId;
        public PositionData playerPos;

        public const ushort PacketId = 41;

        public PlayerUpdatePacket(int playerId, PositionData playerPos)
        {
            this.playerId = playerId;
            this.playerPos = playerPos;
        }

        public PlayerUpdatePacket(DataReader dataReader)
        {
            FromPacket(dataReader);
        }

        public void FromPacket(DataReader dataReader)
        {
            playerId = dataReader.GetInt();
            playerPos = new PositionData(dataReader);
        }

        public byte[] ToPacket()
        {
            DataWriter dataWriter = new DataWriter();
            dataWriter.Put(PacketId);
            dataWriter.Put(playerId);
            dataWriter.Put(playerPos.ToBytes());
            return dataWriter.CopyData();
        }
    }
}
