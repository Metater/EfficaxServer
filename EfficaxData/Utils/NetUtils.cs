using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace EfficaxData.Utils
{
    public static class NetUtils
    {
        public static IPEndPoint MakeEndPoint(string hostStr, int port, bool ipv6)
        {
            return new IPEndPoint(ResolveAddress(hostStr, ipv6), port);
        }
        public static IPAddress ResolveAddress(string hostStr, bool ipv6)
        {
            if (hostStr == "localhost")
                return IPAddress.Loopback;

            IPAddress ipAddress;
            if (!IPAddress.TryParse(hostStr, out ipAddress))
            {
                if (ipv6)
                    ipAddress = ResolveAddress(hostStr, AddressFamily.InterNetworkV6);
                if (ipAddress == null)
                    ipAddress = ResolveAddress(hostStr, AddressFamily.InterNetwork);
            }
            if (ipAddress == null)
                throw new ArgumentException("Invalid address: " + hostStr);

            return ipAddress;
        }
        private static IPAddress ResolveAddress(string hostStr, AddressFamily addressFamily)
        {
            IPAddress[] addresses = ResolveAddresses(hostStr);
            foreach (IPAddress ip in addresses)
            {
                if (ip.AddressFamily == addressFamily)
                {
                    return ip;
                }
            }
            return null;
        }
        private static IPAddress[] ResolveAddresses(string hostStr)
        {
#if NETSTANDARD || NETCOREAPP
            var hostTask = Dns.GetHostEntryAsync(hostStr);
            hostTask.GetAwaiter().GetResult();
            var host = hostTask.Result;
#else
            var host = Dns.GetHostEntry(hostStr);
#endif
            return host.AddressList;
        }
    }
}
