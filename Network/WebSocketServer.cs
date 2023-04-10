using System.Net.WebSockets;
using System.Text;

namespace Network;

public class WebSocketServer
{
    private static readonly ISet<WebSocket> webSockets = new HashSet<WebSocket>();

    public static bool Register(WebSocket webSocket) => webSockets.Add(webSocket);

    public static async void Broadcast(string message)
    {
        foreach (WebSocket ws in webSockets)
            await ws.SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                WebSocketMessageType.Text,
                WebSocketMessageFlags.EndOfMessage,
                CancellationToken.None);
    }
}