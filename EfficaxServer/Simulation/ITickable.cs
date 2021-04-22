using System;
using System.Collections.Generic;
using System.Text;

namespace EfficaxServer.Simulation
{
    public interface ITickable
    {
        public void Tick(long id);
    }
}
