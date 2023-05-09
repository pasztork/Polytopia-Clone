using LogView.LogTransformer;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace Network.Controllers;

public class WebSocketController : ControllerBase
{
    private readonly NetworkCommandProcessor _commandProcessor = new();
    private bool _gameEnded = false;

    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            if (!WebSocketServer.Register(webSocket))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                return;
            }

            Model.GameManager.Get<Model.TurnManagerBase>().OnWinnerDecided += CloseConnection;
            await HandleWebSocketCommunication(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private async Task HandleWebSocketCommunication(WebSocket webSocket)
    {
        var buffer = new byte[4096];
        await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer),
            CancellationToken.None);

        WebSocketServer.CreatePlayer(
            TrimBufferString(Encoding.UTF8.GetString(buffer)), webSocket);

        while (webSocket.State == WebSocketState.Open && !_gameEnded)
        {
            Array.Clear(buffer);
            await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None);

            string receivedString = TrimBufferString(Encoding.UTF8.GetString(buffer));
            if (WebSocketServer.CurrentSocketPlayer == webSocket)
            {
                if (receivedString.Contains("GetGameState"))
                {
                    await SendGameStateTo(webSocket);
                }
                else if (_commandProcessor.Process(receivedString))
                {
                    Console.WriteLine(receivedString);
                    await WebSocketServer.Broadcast(webSocket, receivedString);
                }
            }
        }

        await webSocket.CloseAsync(
            WebSocketCloseStatus.NormalClosure,
            "WebSocket connection closed",
            CancellationToken.None);
    }

    private void CloseConnection(Model.Player player) => _gameEnded = true;

    private static async Task SendGameStateTo(WebSocket webSocket)
    {
        var logTransformer = new LogTransformer();
        var gameState = logTransformer.Transform();
        await webSocket.SendAsync(
            new ArraySegment<byte>(Encoding.UTF8.GetBytes(gameState)),
            WebSocketMessageType.Text,
            WebSocketMessageFlags.EndOfMessage,
            CancellationToken.None);
    }

    private static string TrimBufferString(string bufferString)
    {
        var nullIndex = bufferString.IndexOf('\0');
        if (nullIndex < 0) { return bufferString; }

        return bufferString[..nullIndex];
    }
}
