using Workouts.RabbitMQ;

const string EXCHANGE_NAME = "workout";
const string ROUTING_KEY = "workout_key";


Thread[] threads = new Thread[35];
for (int i = 0; i < 35; i++)
{
    threads[i] = new Thread(() =>
    {
        RabbitMQPusblisher.Instance.Publish(EXCHANGE_NAME, ROUTING_KEY, "Selam");
    });
    threads[i].Start();
}

foreach (var thread in threads)
{
    thread.Join();
}

Console.ReadLine();