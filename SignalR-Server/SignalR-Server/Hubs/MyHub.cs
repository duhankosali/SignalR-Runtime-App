using Microsoft.AspNetCore.SignalR; // signalR namespace'i
using System.Threading.Tasks;

namespace SignalR_Server.Hubs
{
    public class MyHub : Hub
    {
        // Buraya POST işlemi yapıyorum.
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
