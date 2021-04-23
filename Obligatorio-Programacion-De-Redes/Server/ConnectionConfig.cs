using System;
using System.Net;
using System.Net.Sockets;
using  System.Configuration;

namespace Server
{
    public class ConnectionConfig
    {

        public Socket Connect()
        {
            var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketServer.Bind(new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ServerIp"]), Int32.Parse(ConfigurationManager.AppSettings["ServerPort"])));
            socketServer.Listen(Int32.Parse(ConfigurationManager.AppSettings["ListenPort"]));
            return socketServer;
        }
    }
}