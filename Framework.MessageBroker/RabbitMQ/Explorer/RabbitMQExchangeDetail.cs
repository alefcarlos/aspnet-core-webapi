namespace Framework.MessageBroker.RabbitMQ.Explorer
{
    public class RabbitMQExchangeDetail
    {
        public bool auto_delete { get; set; }
        public bool durable { get; set; }
        public bool _internal { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string user_who_performed_action { get; set; }
        public string vhost { get; set; }
    }
}
