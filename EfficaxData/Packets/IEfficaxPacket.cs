using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData
{
    public interface IEfficaxPacket
    {
        byte[] ToPacket();
        void FromPacket(DataReader dataReader);
    }
}
