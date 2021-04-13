using System;
using System.Net;
using System.Net.Sockets;
using Protocol;
using DataHandler;

namespace Client
{
    class Program
    {
    

        private static readonly Socket SocketClient =
            new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            
           SocketClient.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0));
           SocketClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30000));
           SocketHandler socketHandler = new SocketHandler(SocketClient);
           try
            {
                new HomePageClient().ShowMenu(SocketClient,socketHandler);
            }
            catch (SocketException e)
            {
                Console.WriteLine("The connection to the server was lost: " + e.Message);
            }
        }

     }
}