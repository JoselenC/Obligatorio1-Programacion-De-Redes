using System;
using System.Net;
using System.Net.Sockets;
using Library;

namespace Client
{
    class Program
    {
        private static readonly HeaderHandler HeaderHandler = new HeaderHandler();

        private static readonly Socket SocketClient =
            new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            var exit = false;
            SocketClient.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0));
            SocketClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 30000));
            Console.WriteLine("Bienvenido al client");
            Console.WriteLine("1-Posts");
            Console.WriteLine("2-Themes");
            Console.WriteLine("3-Archive");
            Console.WriteLine("4-Search post");
            Console.WriteLine("5-Asociate post");
            Console.WriteLine("6-Exit");
            try
            {
                while (!exit)
                {
                    var option = Console.ReadLine();
                    switch (option)
                    {
                        case "uno":
                            new PostPage().ShowMenu();
                            break;
                        case "dos":
                            new ThemePage().ShowMenu();
                            break;
                        case "tres":
                            new ArchivePage().UploadArchive();
                            break;
                        case "Cuatro":
                            new PostPage().SearchPost(); //Metodo buscar
                            break;
                        case "Cinco":
                            new PostPage().AsociateTheme(); //Metodo buscar
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
            catch (SocketException e)
            {
                Console.WriteLine("Se perdió la conexión con el servidor: " + e.Message);
            }
        }

     }
}