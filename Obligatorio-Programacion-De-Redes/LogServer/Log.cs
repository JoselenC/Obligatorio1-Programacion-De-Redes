using System.Collections.Generic;

namespace LogServer
{
    public class Log
    {
        private readonly RabbitHelper _rabbitHelper;

        public Log(RabbitHelper rabbitQueueHelper)
        {
            _rabbitHelper = rabbitQueueHelper;
        }

        public Log()
        {
        }

        public void SaveLog(string log)
        {
            _rabbitHelper.SendMessage(log);
        }

        public string Level { get; set; }
        public string Message { get; set; }

    }
}