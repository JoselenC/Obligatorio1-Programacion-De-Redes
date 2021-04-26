using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using BusinessLogic;
using DataHandler;
using Server;

namespace Client
{
    class Program
    {


        public bool _exit = false;

        private static readonly Socket SocketClient =
            new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            new ConnectionConfig(SocketClient);
            SocketHandler socketHandler = new SocketHandler(SocketClient);
            try
            {
                new HomePageClient().Menu(SocketClient, socketHandler);
            }
            catch (SocketException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The connection to the server was lost: " + e.Message);
            }

        }
    }
}