using Microsoft.AspNetCore.SignalR; // signalR namespace'i
using SignalR_Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR_Server.Hubs
{
    public class MyHub : Hub<IMessageClient>
    {
        static List<string> clients = new();

        public override async Task OnConnectedAsync() // Hub'a yeni  bir Client bağlandı.
        {
            clients.Add(Context.ConnectionId); // Kullanıcı girdiği için listeye ekledik.
            await Clients.All.Clients(clients); // listeyi ön tarafa gönderiyoruz.
            await Clients.All.UserJoined(Context.ConnectionId); // bağlanan kullanıcının ConnectionId'sini bütün kullanıcılara iletiyoruz.
        }

        public override async Task OnDisconnectedAsync(Exception exception) // Hub'dan bir Client ayrıldı.
        {
            clients.Remove(Context.ConnectionId); // Kullanıcı çıktığı için listeden sildik.
            await Clients.All.Clients(clients); // listeyi ön tarafa gönderiyoruz.

            await Clients.All.UserLeft(Context.ConnectionId); // bağlanan kopan ConnectionId'sini bütün kullanıcılara iletiyoruz.
        }
    }
}
