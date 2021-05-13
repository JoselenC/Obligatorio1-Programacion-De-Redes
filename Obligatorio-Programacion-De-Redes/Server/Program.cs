using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic;
using ClientHandler;
using DataHandler;
using Domain;

namespace Server
{
    class Program
    {
        public static bool _exit = false;

        static async Task Main(string[] args)
        {
            HandleClient clientHandler = new HandleClient();
            await clientHandler.StartServerAsync();
        }

    }
}