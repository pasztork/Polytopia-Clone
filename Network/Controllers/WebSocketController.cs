using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace Network.Controllers;

public class WebSocketController : ControllerBase
{
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            WebSocketServer.Register(webSocket);
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
        byte[] buffer = new byte[4096];
        WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        // it is possible to call other methods while in this loop
        // thanks to await <3
        while (webSocket.State == WebSocketState.Open)
        {
            ArraySegment<byte> receivedData = new ArraySegment<byte>(buffer, 0, receiveResult.Count);
            WebSocketServer.Broadcast(Encoding.UTF8.GetString(receivedData.ToArray()));

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
