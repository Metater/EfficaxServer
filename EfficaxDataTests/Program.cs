using System;
using EfficaxData.Packets.Chat;
using EfficaxData.Packets.Player;
using EfficaxData;

namespace EfficaxDataTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            PrintResults(PacketTest("ChatSendPacket"));
            PrintResults(PacketTest("ChatReceivePacket"));
            PrintResults(PacketTest("PlayerJoinPacket"));
        }
        private static void PrintResults((string, bool) results)
        {
            Console.WriteLine($"{results.Item1} Test passed: {results.Item2}");
        }
        private static (string, bool) PacketTest(string packetName)
        {
            switch (packetName)
            {
                case "ChatSendPacket":
                    return (packetName, ChatSendPacket());
                case "ChatReceivePacket":
                    return (packetName, ChatReceivePacket());
                case "PlayerJoinPacket":
                    return (packetName, PlayerJoinPacket());
                default:
                    return ("UnamedPacket", false);
            }
        }
        private static DataReader GetDataReader(byte[] data)
        {
            DataReader dataReader = new DataReader(data);
            dataReader.SkipBytes(2);
            return dataReader;
        }
        private static bool ChatSendPacket()
        {
            string message = "Test message";
            ChatSendPacket packet = new ChatSendPacket(message);
            ChatSendPacket recoveredPacket = new ChatSendPacket(GetDataReader(packet.ToPacket()));
            return (recoveredPacket.message == message);
        }
        private static bool ChatReceivePacket()
        {
            string message = "Test message";
            ChatReceivePacket packet = new ChatReceivePacket(message);
            ChatReceivePacket recoveredPacket = new ChatReceivePacket(GetDataReader(packet.ToPacket()));
            return (recoveredPacket.message == message);
        }
        private static bool PlayerJoinPacket()
        {
            int id = 2322;
            string name = "Metater";
            PositionData pos = new PositionData(32.423f, 12.3232f);
            PlayerJoinPacket packet = new PlayerJoinPacket(id, name, pos);
            PlayerJoinPacket recoveredPacket = new PlayerJoinPacket(GetDataReader(packet.ToPacket()));
            return (recoveredPacket.playerId == id && recoveredPacket.playerName == name && recoveredPacket.playerPos.x == pos.x && recoveredPacket.playerPos.y == pos.y);
        }
    }
}
