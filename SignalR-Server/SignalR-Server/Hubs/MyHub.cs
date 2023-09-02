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
            /*
             Clients.Caller --> Sadece server'e bildirim gönderir (Mesajı sadece gönderen kişi görür)
             Clients.Other --> Server'a bildirim gönderen dışında bütün clientlar ile iletişim kurar.
             Clients.All --> Sunucudaki herkese bildirim gönderir.

             Clients.AllExcept(connecetionIds) --> Gönderilen connectionId'ler dışındaki bütün kullanıcılara mesaj gönderir.
             Clients.Client(connectionId) --> connectionId'si gönderilen kullanıcıya mesaj gönderir.
             Clients.Clients(connectionIds) --> Gönderilen connectionId'lere mesaj gönderir. (AllExcept'in tam tersi)

             !! Clients.Group(groupName).SendAsync("receiveMessage", message); --> Belirtilen gruptaki bütün Clientlar'a mesaj gönderir fakat öncelikle bir grup oluşturup bu gruba bir kaç tane client subscribe etmeliyiz.
             Clients.GroupExcept(groupName, connectionIds).SendAsync("receiveMessage, message) --> Grup ismi ve mesaj göndermek istemediğimiz kişilerin connectionIds lerini gönderiyoruz. O kişiler hariç gruptaki herkese mesaj gider.
             Clients.Groups(groupNames) --> Birden fazla gruba mesaj göndermek için kullanılır.
             Clients.OthersInGroup(groupName) --> İlgili gruptaki herkese mesaj gönderir (mesajı gönderen kişi hariç) 

             (User ve Users methodlarının kullanılabilmesi için authentication yapısı kurulmalı)
             Clients.User(userId) --> Yalnızca Id'si verilen kullanıcıya mesaj gönderir.
             Clients.Users(usersId) --> Yalnızca Id'leri verilen kullanıcılara mesaj gönderir
             */
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

        public async Task AddGroup(string connectionId, string groupName)
        {
           await Groups.AddToGroupAsync(connectionId, groupName);
        }
    }
}
