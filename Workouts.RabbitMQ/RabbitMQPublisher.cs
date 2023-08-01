using RabbitMQ.Client;
using System.Text;

namespace Workouts.RabbitMQ
{
    /// <summary>
    /// using  Workouts.RabbitMQ.Program.cs;
    /// </summary>
    public sealed class RabbitMQPusblisher : IDisposable
    {
        private static readonly Lazy<RabbitMQPusblisher> _instance = new Lazy<RabbitMQPusblisher>(() => new RabbitMQPusblisher());
        public static RabbitMQPusblisher Instance => _instance.Value;
        private RabbitMQPusblisher()
        {
            Console.WriteLine("RabbitMQPusblisher Nesnesi Üretildi");
        }

        private IConnection _connection;
        private IConnection Connection
        {
            get
            {
                if (_connection == null || (_connection == null && _connection.IsOpen == false))
                {
                    Console.WriteLine("Connection Açıldı");
                    return _connection = GetConnectionFactory().CreateConnection();
                }

                return _connection;
            }
        }

        private IModel _channel;
        public IModel Channel
        {
            get
            {
                if (_channel == null || (_channel == null && _channel.IsOpen == false))
                {
                    Console.WriteLine("Channel Açıldı");
                    return _channel = Connection.CreateModel();
                }
                return _channel;
            }
        }

        public void Publish(string exchangeName, string routingKey, string message)
        {
            try
            {
                Channel.BasicPublish(exchangeName, routingKey, null, Encoding.UTF8.GetBytes(message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void GetNewChannel() => Connection.CreateModel();

        private ConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory()
            {
                Uri = new Uri(@""),
                UserName = "",
                Password = "",
                Port = 5671
            };
        }

        public void Dispose()
        {
            if (_connection != null) _connection.Dispose();
            if (_channel != null) _channel.Dispose();
        }
    }
    public sealed class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQClientService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (!(_channel is not null && _channel.IsOpen))
            {
                _channel = _connection.CreateModel();
            }

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
