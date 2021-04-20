using System;
using System.Collections.Generic;
using System.Text;
using EfficaxData.Data;

namespace EfficaxData.Packets.Player
{
    public class PlayerJoinPacket : IEfficaxPacket
    {
        public int playerId;
        public string playerName;
        public PositionData playerPos;

        public const ushort PacketId = 40;

        public PlayerJoinPacket(int playerId, string playerName, PositionData playerPos)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.playerPos = playerPos;
        }

        public PlayerJoinPacket(DataReader dataReader)
        {
            FromPacket(dataReader);
        }

        public void FromPacket(DataReader dataReader)
        {
            playerId = dataReader.GetInt();
            playerName = dataReader.GetString();
            playerPos = new PositionData(dataReader);
        }

        public byte[] ToPacket()
        {
            DataWriter dataWriter = new DataWriter();
            dataWriter.Put(PacketId);
            dataWriter.Put(playerId);
            dataWriter.Put(playerName);
            dataWriter.Put(playerPos.ToBytes());
            return dataWriter.CopyData();
        }
    }
}
