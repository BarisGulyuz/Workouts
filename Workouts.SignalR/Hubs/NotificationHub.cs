using Microsoft.AspNetCore.SignalR;

namespace Workouts.SignalR.Hubs
{
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
