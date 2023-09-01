using Microsoft.AspNetCore.SignalR; // signalR namespace'i
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR_Server.Hubs
{
    public class MyHub : Hub
    {
        static List<string> clients = new();

        // Buraya POST işlemi yapıyorum.
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }

        public override async Task OnConnectedAsync() // Hub'a yeni  bir Client bağlandı.
        {
            clients.Add(Context.ConnectionId); // Kullanıcı girdiği için listeye ekledik.
            await Clients.All.SendAsync("clients", clients); // listeyi ön tarafa gönderiyoruz.

            await Clients.All.SendAsync("userJoined", Context.ConnectionId); // bağlanan kullanıcının ConnectionId'sini bütün kullanıcılara iletiyoruz.
        }

        public override async Task OnDisconnectedAsync(Exception exception) // Hub'dan bir Client ayrıldı.
        {
            clients.Remove(Context.ConnectionId); // Kullanıcı çıktığı için listeden sildik.
            await Clients.All.SendAsync("clients", clients); // listeyi ön tarafa gönderiyoruz.

            await Clients.All.SendAsync("userLeft", Context.ConnectionId); // bağlanan kopan ConnectionId'sini bütün kullanıcılara iletiyoruz.
        }
    }
}
