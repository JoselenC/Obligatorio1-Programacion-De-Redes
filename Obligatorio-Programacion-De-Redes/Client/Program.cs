using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using DataHandler;
using Server;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverHandler = new ServerHandler();
            await serverHandler.StartClientAsync();
        }
    }
}