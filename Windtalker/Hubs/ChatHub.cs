using Microsoft.AspNet.SignalR;

namespace Windtalker.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.displayMessage(Context.User.Identity.Name + ": " + message);
        }
    }
}