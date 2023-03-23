using Models.Entities;
using System.Net.WebSockets;

namespace BaytyAPIs.Services.RealTime
{
    public interface IWebSocketService
    {
        bool ConnectUser(string userId, WebSocket ws);
        bool UpdateUser(string userId, WebSocket ws);
        WebSocket GetUser(string userId);
    }
}
