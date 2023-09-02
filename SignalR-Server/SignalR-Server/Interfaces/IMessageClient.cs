using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR_Server.Interfaces
{
    public interface IMessageClient
    {
        // isimlendirme kurallarına uyulmalı
        Task Clients(List<string> clients);
        Task UserJoined(string message);
        Task UserLeft(string message);
    }
}
