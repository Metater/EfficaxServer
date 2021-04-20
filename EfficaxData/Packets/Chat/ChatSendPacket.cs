using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData.Packets.Chat
{
    /// <summary>
    /// Server-Bound
    /// </summary>
    public class ChatSendPacket : IEfficaxPacket
    {
        public string message;

        public const ushort PacketId = 80;

        public ChatSendPacket(string message)
        {
            this.message = message;
        }

        public ChatSendPacket(DataReader dataReader)
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
