using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Workouts.BackgroundServices
{
    public class RabbitMQWorker : BackgroundService
    {
        private readonly ILogger<RabbitMQWorker> _logger;

        private IConnection _connection;
        private IModel _channel;

        const string QUEUE_NAME = "workout_queue";

        public RabbitMQWorker(ILogger<RabbitMQWorker> logger)
        {
            _logger = logger;
            _connection = GetConnectionFactory().CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Started");

            await Task.Delay(1000);

            var consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(QUEUE_NAME, true, consumer);
            consumer.Received += Consumer_Received;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopped");

            return base.StopAsync(cancellationToken);
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation(Encoding.UTF8.GetString(e.Body.ToArray()));
        }

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
    }
}