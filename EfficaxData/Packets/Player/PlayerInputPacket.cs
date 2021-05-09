using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData.Packets.Player
{
    public class PlayerInputPacket : IEfficaxPacket
    {
        [Flags]
        public enum PlayerInputs : byte
        {
            W0 = 1,
            S0 = 2,
            A0 = 4,
            D0 = 8,
            W1 = 16,
            S1 = 32,
            A1 = 64,
            D1 = 128
        }
        public const int PacketId = 30;

        public PlayerInputs playerInputs = 0;
        public void FromPacket(DataReader dataReader)
        {
            byte data = dataReader.GetByte();
            playerInputs = (PlayerInputs)data;
        }

        public byte[] ToPacket()
        {
            DataWriter dataWriter = new DataWriter();
            dataWriter.Put(PacketId);
            dataWriter.Put((byte)playerInputs);
            return dataWriter.CopyData();
        }
    }
}
