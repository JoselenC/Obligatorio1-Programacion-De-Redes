using System.Text;
using BusinessLogic.Managers;
using LogsServerInterface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LogsServer
{
    public class RabbitHelper
    {
        private readonly IModel _channel;
        private readonly ILogService _logService;
        public RabbitHelper(ManagerLogRepository managerLogRepository)
        {
            _logService = new LogService(managerLogRepository);
            var connectionFactory = new ConnectionFactory { HostName = "localhost" };
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
        }
        
        public void ReceiveMessages()
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
                 _logService.AddLog(message);
              
            };
            _channel.BasicConsume(queue: "logs",
                autoAck: true,
                consumer: consumer);
        }

    }
}