using Controller;
using LogView;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Network;

public class WebSocketServer
{
	public static event Action? OnAllClientsDisconnected;
	public static string MapFilePath { get; set; } = string.Empty;
	public static int PlayerCount { private get; set; } = 2;
	public static WebSocket CurrentSocketPlayer
	{
		get => s_playerToWebSocketsDict[Model.GameManager
			.Get<Model.TurnManagerBase>().CurrentPlayer];
	}

	private static readonly IList<WebSocket> s_webSockets = new List<WebSocket>();
	private static readonly IDictionary<Model.Player, WebSocket> s_playerToWebSocketsDict = new Dictionary<Model.Player, WebSocket>();

	static WebSocketServer() =>
		Model.GameManager.Get<Model.TurnManagerBase>().OnTurnStarted += StartTurnMessage;

	public static void Start()
	{
		GameManager.NewGameWithSavedMap(MapFilePath);
		JsonLogger.Init();
	}

	public static bool Register(WebSocket webSocket)
	{
		if (s_webSockets.Count >= PlayerCount ||
			s_webSockets.Contains(webSocket)) { return false; }

		s_webSockets.Add(webSocket);
		return true;
	}

	public static void Remove(WebSocket webSocket)
	{
		s_webSockets.Remove(webSocket);
		if(s_webSockets.Count == 0)
		{
			OnAllClientsDisconnected?.Invoke();
		}
	}

	public static async Task Broadcast(WebSocket from, string message)
	{
		foreach (var ws in s_webSockets)
		{
			if (ws == from) { continue; }
			await ws.SendAsync(
				new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
				WebSocketMessageType.Text,
				WebSocketMessageFlags.EndOfMessage,
				CancellationToken.None);
		}
	}

	public static void CreatePlayer(string playerJson, WebSocket webSocket)
	{
		var webPlayer = JsonSerializer.Deserialize<WebPlayer>(playerJson) ??
			 throw new ArgumentNullException(nameof(playerJson));
		var player = new Model.Player(webPlayer.Name);
		s_playerToWebSocketsDict[player] = webSocket;

		if (s_playerToWebSocketsDict.Count == PlayerCount) { GameManager.StartNew(); }
	}

    private static async void StartTurnMessage(Model.Player player)
	{
		var message = new { Type = "StartTurn", player.Name };
		string json = JsonSerializer.Serialize(message);
		await s_playerToWebSocketsDict[player].SendAsync(
				new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)),
				WebSocketMessageType.Text,
				WebSocketMessageFlags.EndOfMessage,
				CancellationToken.None);
	}

    private class WebPlayer
	{
		public string Name { get; set; } = string.Empty;
	}
}
