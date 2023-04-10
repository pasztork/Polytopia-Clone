using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Network.Controllers;

public class WebSocketController : ControllerBase
{
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await Echo(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private static async Task Echo(WebSocket webSocket)
    {
        // create buffer for messages arriving from client
        byte[] buffer = new byte[1024 * 4];
        WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        while (webSocket.State == WebSocketState.Open)
        {
            // send info back
            await webSocket.SendAsync(
                new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                receiveResult.MessageType,
                receiveResult.EndOfMessage,
                CancellationToken.None);

            // recieve next message
            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        // close websocket
        await webSocket.CloseAsync(
            WebSocketCloseStatus.NormalClosure,
            "WebSocket connection closed",
            CancellationToken.None);
    }
}
