using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BusinessLogic;
using Protocol;
using DataHandler;
using Server;

namespace Client
{
    class Program
    {


        public static bool _exit = false;

        private static readonly Socket SocketClient =
            new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {

            SocketClient.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0));
            SocketClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30000));
            SocketHandler socketHandler = new SocketHandler(SocketClient);
            MemoryRepository repository = new MemoryRepository();
            try
            {

                var threadClient = new Thread(x=>new ServerHandler().HandleClientMethod(SocketClient, repository, _exit));
                threadClient.Start();
                
            }
            catch (SocketException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The connection to the server was lost: " + e.Message);
            }

        }
    }
}