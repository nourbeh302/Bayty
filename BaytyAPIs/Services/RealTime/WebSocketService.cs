using Models.Entities;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace BaytyAPIs.Services.RealTime
{
    public class WebSocketService : IWebSocketService
    {
        public ConcurrentDictionary<string, WebSocket> ConnectedUsers { get; set; } = new ConcurrentDictionary<string, WebSocket>();

        public bool ConnectUser(string userId, WebSocket ws) => ConnectedUsers.TryAdd(userId, ws);

        public WebSocket GetUser(string userId)
        {
            ConnectedUsers.TryGetValue(userId, out WebSocket ws);
            return ws;
        }

        public bool UpdateUser(string userId, WebSocket ws) => ConnectedUsers
            .TryUpdate(userId, ws, ConnectedUsers.FirstOrDefault(cu => cu.Key == userId).Value);
    }
}
