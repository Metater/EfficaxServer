using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData.Packets.Chat
{
    /// <summary>
    /// Client-Bound
    /// </summary>
    public class ChatReceivePacket : IEfficaxPacket
    {
        public string message;

        public const ushort PacketId = 81;

        public ChatReceivePacket(string message)
        {
            this.message = message;
        }

        public ChatReceivePacket(DataReader dataReader)
        {
            FromPacket(dataReader);
        }

        public void FromPacket(DataReader dataReader)
        {
            message = dataReader.GetString();
        }

        public byte[] ToPacket()
        {
            DataWriter dataWriter = new DataWriter();

            dataWriter.Put(PacketId);
            dataWriter.Put(message);

            return dataWriter.CopyData();
        }
    }
}
