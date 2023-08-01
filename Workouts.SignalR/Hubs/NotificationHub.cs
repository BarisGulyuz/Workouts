using Microsoft.AspNetCore.SignalR;

namespace Workouts.SignalR.Hubs
{
    /// <summary>
    /// Workouts.SignalR.Hubs.Program.cs /notificate enpoint
    /// UI/signalR.html
    /// </summary>
    public class NotificationHub : Hub
    {
        const string NOTIFICATIN_METHOD_NAME = "ReceiveNotification";
        public async Task SendNotificationAsync(string notificationMessage)
        {
            if (Clients != null)
            {
                await Clients.All.SendAsync(NOTIFICATIN_METHOD_NAME, notificationMessage);
            }
        }
    }
}
