using System;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Managers;
using LogsServerInterface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LogsServer
{
    public class RabbitHelper
    {
        private readonly IModel _channel;
        public RabbitHelper(ManagerLogRepository managerLogRepository)
        {
            var connectionFactory = new ConnectionFactory { HostName = "localhost" };
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
        }
        
        public async Task ReceiveMessagesAsync(Func<string, Task> addLog)
        {
            _channel.QueueDeclare(queue: "logs",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await addLog(message);
              
            };
            _channel.BasicConsume(queue: "logs",
                autoAck: true,
                consumer: consumer);
            Console.WriteLine($"Listening queue logs");
            Console.ReadLine();
        }

    }
}