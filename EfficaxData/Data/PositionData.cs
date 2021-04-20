using System;
using System.Collections.Generic;
using System.Text;
using EfficaxData.Data;

namespace EfficaxData
{
    public class PositionData : IEfficaxData
    {
        public float x;
        public float y;

        public PositionData(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public PositionData(DataReader dataReader)
        {
            x = dataReader.GetFloat();
            y = dataReader.GetFloat();
        }

        public byte[] ToBytes()
        {
            DataWriter dataWriter = new DataWriter();
            dataWriter.Put(x);
            dataWriter.Put(y);
            return dataWriter.CopyData();
        }

        public void FromBytes(DataReader dataReader)
        {
            x = dataReader.GetFloat();
            y = dataReader.GetFloat();
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}

