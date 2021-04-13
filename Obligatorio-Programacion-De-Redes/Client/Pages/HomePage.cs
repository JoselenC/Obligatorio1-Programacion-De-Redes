﻿using System;
using System.Net.Sockets;
using Library;
using Protocol;

namespace Client
{
    public class HomePage
    {
        
        public void ShowMenu(Socket SocketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            Console.WriteLine("----Menu----");
            Console.WriteLine("1-Posts");
            Console.WriteLine("2-Themes");
            Console.WriteLine("3-File");
            Console.WriteLine("4-Search post");
            Console.WriteLine("5-Asociate post");
            Console.WriteLine("6-Exit");
            var exit = false;
            while (!exit)
            {
                var option = Console.ReadLine();
                switch (option)
                {
                    case "uno":
                        SendData(1,SocketClient);
                        new PostPage().ShowMenu(SocketClient, socketHandler);
                        break;
                    case "dos":
                        SendData(2,SocketClient);
                        new ThemePage().ShowMenu(SocketClient,socketHandler);
                        break;
                    case "tres":
                        SendData(3,SocketClient);
                        new FilePage().UploadFile(socketHandler);
                        break;
                    case "Cuatro":
                        SendData(4,SocketClient);
                        new PostPage().SearchPost(socketHandler); 
                        break;
                    case "Cinco":
                        SendData(5,SocketClient);
                        new PostPage().AsociateTheme(socketHandler); 
                        break;
                    case "Seis":
                        SendData(6,SocketClient);
                        exit = true;
                        SocketClient.Shutdown(SocketShutdown.Both);
                        SocketClient.Close();
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
        private static byte[] ConvertDataToHeader(short command, int data)
        {
            return HeaderHandler.EncodeHeader(command, data);
        }

        private static void SendData(short command,Socket SocketClient)
        {
            if (SocketClient.Send(ConvertDataToHeader(command, new Random().Next())) == 0)
            {
                throw new SocketException();
            }
        }
    }
}