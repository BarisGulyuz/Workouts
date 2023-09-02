using Workouts.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection servies = builder.Services;
servies.AddSingleton<NotificationHub>();

servies.AddSignalR();
servies.AddCors(opt =>
{
    opt.AddPolicy("Hub", policy =>
    {
        policy.AllowCredentials();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.SetIsOriginAllowed(x => true);
    });
});

var app = builder.Build();

app.MapPost("/notificate", async (string notificationMessage, IServiceProvider sp) =>
{
    NotificationHub notificationHub = sp.GetRequiredService<NotificationHub>();

    await notificationHub.SendNotificationAsync(notificationMessage);

    return new { Success = true };
});

app.UseCors("Hub");
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationHub");
});

app.Run();




