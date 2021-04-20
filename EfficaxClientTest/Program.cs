using System;
using System.Threading;
using LiteNetLib;
using System.Text;
using LiteNetLib.Utils;
using EfficaxClient;
using EfficaxData;
using EfficaxData.Packets;

namespace EfficaxClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager client = new NetManager(listener);

            EfficaxClientContainer clientContainer = new EfficaxClientContainer();

            client.Start();

            client.Connect("localhost" /* host ip or name */, 9050 /* port */, "Gamer" /* text key or NetDataWriter */);
            listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
            {

                dataReader.Recycle();
            };

            while (!Console.KeyAvailable)
            {
                client.PollEvents();
                Thread.Sleep(15);
            }

            client.Stop();
        }
    }
}
