using RabbitMQ.Client;

namespace Workouts.RabbitMQ
{
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
