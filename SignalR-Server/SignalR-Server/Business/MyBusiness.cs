using Microsoft.AspNetCore.SignalR;
using SignalR_Server.Hubs;
using System.Threading.Tasks;

namespace SignalR_Server.Business
{
    public class MyBusiness
    {
        readonly IHubContext<MyHub> _hubContext;
        public MyBusiness(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // POST
        public async Task SendMessageAsync(string message, string connectionId)
        {
            await _hubContext.Clients.All.SendAsync("receiveMessage", message, connectionId);
        }
    }
}
