using System;
using System.Net.Sockets;
using DataHandler;

namespace Client
{
    public class FilePageClient
    {
        
        public void UploadFile(SocketHandler socketHandler, Socket SocketClient)
        {
            socketHandler.SendData(8,SocketClient);
            Console.WriteLine("Name of the associated post"); 
            string name = Console.ReadLine();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(name);
            byte[] dataLength = BitConverter.GetBytes(data.Length);
            socketHandler.Send(dataLength);
            socketHandler.Send(data);
            //Aca deberia ir el send del protocolo de archivos
        }
    }
}