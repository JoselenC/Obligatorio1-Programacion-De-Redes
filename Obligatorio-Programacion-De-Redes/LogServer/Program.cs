using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace LogServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitClient = new RabbitHelper();
            rabbitClient.QueueDeclare();
            rabbitClient.ReceiveMessages();
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
        
    }

}
