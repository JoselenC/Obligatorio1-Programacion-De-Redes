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
            Console.Write("Select option");
            Console.WriteLine("1-Add post");
            Console.WriteLine("2-Modify post");
            Console.WriteLine("3-Delete post");
            Console.WriteLine("4-Associate theme");
            Console.WriteLine("5-Back");
            bool exit = false;
            while (!exit)
            {

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AddPost(socketHandler);
                        break;
                    case "2":
                        ModifyPost(socketHandler);
                        break;
                    case "3":
                        DeletePost(socketHandler);
                        break;
                    case "4":
                        AddTheme( socketHandler);
                        break;
                    case "5":
                        exit = true;
                        new HomePage().ShowMenu(socketClient,socketHandler);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

       
        private void DeletePost(SocketHandler socketHandler)
        {
            Console.WriteLine("Name of the post to delete"); 
            string name = Console.ReadLine();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(name);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            Console.WriteLine("Message sent to the server");
        }

        private void ModifyPost(SocketHandler socketHandler)
        {
            Console.WriteLine("Name of the post to modify");
            string name = Console.ReadLine();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(name);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            Console.WriteLine("Message sent to the server");
        }

        public void AddPost(SocketHandler socketHandler)
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

        private void AddTheme(SocketHandler socketHandler)
        {
            throw new NotImplementedException();
        }
        
        public void AsociateTheme(SocketHandler socketHandler)
        {
                
        }
        
        public void SearchPost(SocketHandler socketHandler)
        {
                
        }
        
    }
}