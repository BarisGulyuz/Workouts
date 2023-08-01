using Workouts.BackgroundServices;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddHostedService<RabbitMQWorker>();
    })
    .Build();

await host.RunAsync();
