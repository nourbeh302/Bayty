using Server.Security;
using Server.Services.RealTime;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace Server.Controllers
{

    [ApiController]
    [Route("/ws/send/{currentId?}")]
    public class ChatController : ControllerBase
    {
        private readonly IWebSocketService _wsService;
        public ChatController(IWebSocketService wsService)
        {
            _wsService = wsService;
        }

        [HttpGet]
        public async Task SendMessage(string currentId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var ws = await HttpContext.WebSockets.AcceptWebSocketAsync();

                if (_wsService.UpdateUser(currentId, ws))
                    Console.WriteLine($"User With Id:{currentId} Connection Updated Successfully");
                else
                {
                    _wsService.ConnectUser(currentId, ws);
                    Console.WriteLine($"User With Id:{currentId} Connected Successfully");
                }

                var msg = new byte[4096];

                var receivedData = await ws.ReceiveAsync(new ArraySegment<byte>(msg), CancellationToken.None);

                while (!receivedData.CloseStatus.HasValue)
                {
                    var result = await ws.ReceiveAsync(new ArraySegment<byte>(msg), CancellationToken.None);

                    var message = ConvertArrayOfBytesToMessage(msg.Take(result.Count).ToArray());

                    var receiverSocket = _wsService.GetUser(message.ReceiverId);

                    if (receiverSocket != null)
                    {
                        await receiverSocket.SendAsync(new ArraySegment<byte>(msg), WebSocketMessageType.Text, false, CancellationToken.None);
                    }
                    await ws.SendAsync(new ArraySegment<byte>(msg), WebSocketMessageType.Text, false, CancellationToken.None);
                }
            }
        }



        private Message ConvertArrayOfBytesToMessage(byte[] msg) => JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(msg));
    }
}
