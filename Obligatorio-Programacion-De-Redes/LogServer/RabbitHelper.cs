using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LogServer
{
    public class RabbitHelper
    {
        private readonly string _queueName;

        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitHelper()
        {
            _queueName = "ProgramacionRedes";
            var connectionFactory = new ConnectionFactory { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        
        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        public void QueueDeclare()
        {
            _channel.QueueDeclare(_queueName, false, false, false, null);
        }
        public void ReceiveMessages()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Log log = JsonSerializer.Deserialize<Log>(message);
            };
            _channel.BasicConsume(_queueName, true, consumer);
        }
        
        public void SendMessage(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish(exchange: "",
                    routingKey: "log_queue",
                    basicProperties: null,
                    body: body);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}