using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace RabitMQ.Common
{
    public class RabbitMQFactory
    {
        private ConnectionFactory factory;

        private IModel channel;
        private IModel _channel => channel ?? CreateChannel();

        private IConnection connection;
        private IConnection _connection => connection ?? CreateConnection();

        public RabbitMQFactory()
        {
            factory = new ConnectionFactory();
            factory.HostName = RabbitMQConfiguration.HostName;

            //more configuration and ctor
        }
        public IConnection CreateConnection()
        {
            if (connection is null || connection is not null && !connection.IsOpen)
                this.connection = factory.CreateConnection();
            return connection;
        }
        public IModel CreateChannel()
        {
            channel = _connection.CreateModel();
            return channel;
        }
        public void CreateQueue(string queueName)
        {
            _channel.QueueDeclare(queueName, false, false, false, null);
        }
        public void PublishMessage(string exchangeName, string routingKey, string message)
        {
            CreateQueue(routingKey);
            //_channel.QueueBind(routingKey, exchangeName, routingKey); //direct exhange routingKey = queueName

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchangeName, routingKey, null, body);
        }
        public void ConsumeMessages(IModel channel, string queueName, Action<string> onMessageReceived)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                onMessageReceived(message);
            };

            _channel.BasicConsume(queueName, true, consumer);
        }

    }
}
