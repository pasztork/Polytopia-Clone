using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Network;

public class WebSocketServer
{
    private static readonly IList<WebSocket> s_webSockets = new List<WebSocket>();
    //private static readonly IDictionary<WebSocket, Model.Player> s_webSocketsToPlayer = new Dictionary<WebSocket, Model.Player>();
    private static readonly IDictionary<Model.Player, WebSocket> s_playerToWebSockets = new Dictionary<Model.Player, WebSocket>();
    private static readonly int maxPlayerCount = 3;

    public static bool Register(WebSocket webSocket)
    {
        if(s_webSockets.Count > maxPlayerCount || s_webSockets.Contains(webSocket))
        {
            return false;
        }

        //csak egyszer iratkozzon fel ezekre
        if(s_webSockets.Count == 0)
        {
            Model.GameManager.Get<Model.TurnManagerBase>().OnTurnStarted += EndTurnMessage;
        }
        s_webSockets.Add(webSocket);
        return true;
    }
        

    public static async void Broadcast(string message)
    {
        foreach(var ws in s_webSockets)
        {
            await ws.SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                WebSocketMessageType.Text,
                WebSocketMessageFlags.EndOfMessage,
                CancellationToken.None);
        }
    }

    public static void CreatePlayer(string playerJson, WebSocket webSocket)
    {
        WebPlayer webPlayer = JsonSerializer.Deserialize<WebPlayer>(playerJson);
        Model.Player player = new Model.Player(webPlayer.name);
        s_playerToWebSockets[player] = webSocket;

        if (s_playerToWebSockets.Count == maxPlayerCount)
        {
            StartGame();
        }
    }

    private static void StartGame()
    {

    }

    private async void EndTurnMessage(Model.Player player)
    {
        string message = "{\"Action\": \"StartTurn\", \"Name\": \"" + player.Name + "\"}";
        await s_playerToWebSockets[player].SendAsync(
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
                WebSocketMessageType.Text,
                WebSocketMessageFlags.EndOfMessage,
                CancellationToken.None);
    }

    public static bool IsCurrentPlyer(WebSocket webSocket)
    {
        return s_playerToWebSockets[Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer] == webSocket;
    }

    private class WebPlayer
    {
        public string name;
    }
}