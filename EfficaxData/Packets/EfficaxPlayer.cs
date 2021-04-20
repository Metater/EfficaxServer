using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData.Packets
{
    public class EfficaxPlayer : IEfficaxPacket
    {
        public PositionData position;
        public string name;

        public const ushort PacketId = 4;

        public EfficaxPlayer(DataReader dataReader)
        {
            FromPacket(dataReader);
        }

        public EfficaxPlayer(PositionData position, string name)
        {
            this.position = position;
            this.name = name;
        }

        public byte[] ToPacket()
        {
            DataWriter dataWriter = new DataWriter();

            dataWriter.Put(PacketId);

            dataWriter.Put(position.ToBytes());
            dataWriter.Put(name);

            return dataWriter.CopyData();
        }
        public void FromPacket(DataReader dataReader)
        {
            position = new PositionData(dataReader);
            name = dataReader.GetString();
        }
        public override string ToString()
        {
            return $"Name: {name} Position: {position}";
        }
    }
}
