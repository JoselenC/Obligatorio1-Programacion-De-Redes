using System;
using System.Text;
using BusinessLogic.Managers;
using BusinessLogic.Services;
using LogsServer;
using LogsServerInterface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebAPI
{
    public class RabbitHelper
    {
        private readonly string _queueName;
        
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private ILogService _logService;
        public RabbitHelper(ManagerLogRepository managerLogRepository)
        {
            _logService = new LogService(managerLogRepository);
            _queueName = "ProgramacionRedes";
            var connectionFactory = new ConnectionFactory { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        

        public void QueueDeclare()
        {
            _channel.QueueDeclare("logs", false, false, false, null);
        }

        public void ReceiveMessages()
        {
            var factory = new ConnectionFactory() {HostName = "localhost"};
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "logs",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                 _logService.AddLog(message);
                Console.WriteLine(" [x] Received {0}", message);
              
            };
            channel.BasicConsume(queue: "logs",
                autoAck: true,
                consumer: consumer);
        }

    }
}