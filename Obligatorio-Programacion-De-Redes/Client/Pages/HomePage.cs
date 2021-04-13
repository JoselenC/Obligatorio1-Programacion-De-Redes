using System;
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
            Console.WriteLine("Bienvenido al client");
            Console.WriteLine("1-Posts");
            Console.WriteLine("2-Themes");
            Console.WriteLine("3-Archive");
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
                        new ThemePage().ShowMenu(SocketClient,socketHandler);
                        break;
                    case "tres":
                        new ArchivePage().UploadArchive(SocketClient,socketHandler);
                        break;
                    case "Cuatro":
                        new PostPage().SearchPost(SocketClient,socketHandler); //Metodo buscar
                        break;
                    case "Cinco":
                        new PostPage().AsociateTheme(SocketClient,socketHandler); //Metodo buscar
                        break;
                    case "Seis":
                        exit = true;
                        SocketClient.Shutdown(SocketShutdown.Both);
                        SocketClient.Close();
                        break;
                    default:
                        Console.WriteLine("Opcion invalida...");
                        break;
                }
            }
        }
        private static byte[] ConvertDataToHeader(short command, int data)
        {
            return HeaderHandler.EncodeHeader(command, data);
        }

        //Capaz poner el send y el received en una clase que se llame protocolo perono este el de la clase ultima
        private static void SendData(short command,Socket SocketClient)
        {
            if (SocketClient.Send(ConvertDataToHeader(command, new Random().Next())) == 0)
            {
                throw new SocketException();
            }
        }
    }
}