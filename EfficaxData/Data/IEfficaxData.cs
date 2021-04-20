using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxData.Data
{
    public interface IEfficaxData
    {
        byte[] ToBytes();
        void FromBytes(DataReader dataReader);
    }
}
