using System;
using System.Net;
using System.Net.Sockets;
using  System.Configuration;


namespace Client
{
    public class ConnectionConfig
    {
        public ConnectionConfig(Socket SocketClient)
        {
            try
            {
                SocketClient.Bind(new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ClientIp"]),
                    Int32.Parse(ConfigurationManager.AppSettings["ClientPort"])));
                SocketClient.Connect(new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ClientIp"]), Int32.Parse(ConfigurationManager.AppSettings["ConnectPort"])));
                
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.ToString());
            }
        }
    }
}