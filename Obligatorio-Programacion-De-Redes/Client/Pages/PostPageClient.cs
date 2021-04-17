using System;
using System.Net.Sockets;
using Domain;
using DataHandler;

namespace Client
{
    public class PostPageClient
    {

        public void Menu(Socket socketClient,SocketHandler socketHandler)
        {
            Console.Clear();
            var exit = false;
            string[] _options = {"Add post", "Modify post", "Delete post", "Associate theme", "Back"};
            while (!exit)
            {
                var option = new MenuClient().ShowMenu(_options);
                switch (option)
                {
                    case 1:
                        socketHandler.SendData(1,socketClient);
                        AddPost(socketHandler,socketClient);
                        break;
                    case 2:
                        socketHandler.SendData(2,socketClient);
                        ModifyPost(socketHandler);
                        break;
                    case 3:
                        socketHandler.SendData(3,socketClient);
                        DeletePost(socketHandler);
                        break;
                    case 4:
                        socketHandler.SendData(4,socketClient);
                        AsociateTheme( socketHandler,socketClient);
                        break;
                    case 5:
                        exit = true;
                        new HomePageClient().Menu(socketClient,socketHandler);
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
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
        }

        private void ModifyPost(SocketHandler socketHandler)
        {
            Console.WriteLine("Name of the post to modify");
            string oldName = Console.ReadLine();
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Creation date: ");
            string creationDate = Console.ReadLine();
            string message = oldName + "#" + name + "#" + creationDate;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
            
        }

        public void AddPost(SocketHandler socketHandler,Socket socketClient)
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
            var exit = false;
            string[] _options = {"Add theme to post", "Back"};
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
            while (!exit)
            {
                var option = new MenuClient().ShowMenu(_options);
                switch (option)
                {
                    case 1:
                        socketHandler.SendData(4,socketClient);
                        AddThemeToPost(socketHandler,name);
                        break;
                    case 2:
                        exit = true;
                        Menu(socketClient,socketHandler);
                        break;
                }
            }
           
        }

        private void AddThemeToPost(SocketHandler socketHandler,string postName)
        {
            Console.Write("Theme name: ");
            string themeName = Console.ReadLine();
            string message = postName + "#" + themeName;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
            
        }

        public void AsociateTheme(SocketHandler socketHandler,Socket SocketClient)
        {
            Console.Write("Name post: ");
            string namePost = Console.ReadLine();
            Console.Write("Name theme: ");
            string nameTheme = Console.ReadLine();
            string message = namePost + "#" + nameTheme;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            string[] messageArray = socketHandler.ReceiveMessage();
            Console.WriteLine(messageArray[0]);
        }

        public void SearchPost(SocketHandler socketHandler,Socket SocketClient)
        {
            socketHandler.SendData(9,SocketClient);
            Console.Write("Name post: ");
            string namePost = Console.ReadLine();
            string message = namePost;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            string[] messageArray = socketHandler.ReceiveMessage();
            if (message.Length == 2)
            {
                string name = messageArray[0];
                Console.WriteLine("name:" + name);
                string creationDate = messageArray[1];
                Console.WriteLine("Creation date:" + creationDate);
            }
            else
            {
                string messageReceive = messageArray[0];
                Console.WriteLine("name:" + messageReceive);
            }


        }
    }
}