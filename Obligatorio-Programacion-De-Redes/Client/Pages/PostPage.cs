using System;
using System.Net.Sockets;
using Domain;
using Protocol;

namespace Client
{
    public class PostPage
    {

        public void ShowMenu(Socket socketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            Console.WriteLine("1-Dar de alta");
            Console.WriteLine("2-Modificar");
            Console.WriteLine("3-Borrar");
            Console.WriteLine("4-Asociar a un tema");
            Console.WriteLine("5-Volver");
            bool exit = false;
            while (!exit)
            {

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AddPost(socketClient,socketHandler);
                        break;
                    case "2":
                        ModifyPost(socketClient, socketHandler);
                        break;
                    case "3":
                        DeletePost(socketClient, socketHandler);
                        break;
                    case "4":
                        AddTheme(socketClient, socketHandler);
                        break;
                    case "5":
                        exit = true;
                        new HomePage().ShowMenu(socketClient,socketHandler);
                        break;
                    default:
                        Console.WriteLine("Opcion invalida...");
                        break;
                }
            }
        }

        private void AddTheme(Socket socketClient, SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }

        private void DeletePost(Socket socketClient, SocketHandler socketHandler)
        {
            Console.WriteLine("Nombre del post a borrar"); 
            //SendString(socketHandler);
        }

        private void ModifyPost(Socket socketClient, SocketHandler socketHandler)
        {
            Console.WriteLine("Nombre del post a modificar");
           // SendString(socketHandler);
        }

        public void AddPost(Socket socketClient,SocketHandler socketHandler)
        {
            
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Creation date: ");
            string creationDate = Console.ReadLine();
            string message = name + "#" + creationDate;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            Console.WriteLine("Message sent to the server");
        }

        public void AsociateTheme(Socket SocketClient,SocketHandler socketHandler)
        {
                
        }
        
        public void SearchPost(Socket SocketClient,SocketHandler socketHandler)
        {
                
        }
        
    }
}