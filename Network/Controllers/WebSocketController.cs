using JsonLog;
using LogView.LogTransformer;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Network.Controllers;

public class WebSocketController : ControllerBase
{
	private static readonly NetworkCommandProcessor _commandProcessor = NetworkCommandProcessor.Instance;
    private static object _lock = new object();
	private WebSocket _webSocket;

    [Route("/ws")]
	public async Task Get()
	{
		if (HttpContext.WebSockets.IsWebSocketRequest)
		{
			using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
			_webSocket = webSocket;
			if (!WebSocketServer.Register(webSocket))
			{
				HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
				return;
			}

			Model.GameManager.Get<Model.TurnManagerBase>().OnWinnerDecided += CloseConnection;
			try
			{
				await HandleWebSocketCommunication(webSocket);
			}
			catch (Exception ex) 
			{
				Console.WriteLine($"Hiba: {ex.Message}");
			}
		}
		else
		{
			HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
		}
	}

	private async Task HandleWebSocketCommunication(WebSocket webSocket)
	{
		await SendSettingsTo(webSocket);

		var buffer = new byte[4096];
		await webSocket.ReceiveAsync(
			new ArraySegment<byte>(buffer),
			CancellationToken.None);

		WebSocketServer.CreatePlayer(
			TrimBufferString(Encoding.UTF8.GetString(buffer)), webSocket);

		while (webSocket.State == WebSocketState.Open)
		{
			Array.Clear(buffer);
			await webSocket.ReceiveAsync(
				new ArraySegment<byte>(buffer),
				CancellationToken.None);

			string receivedString = TrimBufferString(Encoding.UTF8.GetString(buffer));
			if (WebSocketServer.CurrentSocketPlayer == webSocket)
			{
				Console.WriteLine(receivedString);
				var json = JsonSerializer.Deserialize<JsonActionObject>(receivedString);
				if (json is null) { continue; }

				if (json.Action.Equals("GetGameState"))
				{
					await SendGameStateTo(webSocket);
				}
				else if (json.Action.Equals("GetActions"))
				{
                    string actionState;
                    lock (_lock)
                    {
                        actionState = _commandProcessor.GetAvailableActions(json);
                    }
                    await SendAvailableActionsTo(actionState, webSocket);
                }
				else
				{
					(bool, string) result;
                    string actionState;
                    lock (_lock)
					{
						result = _commandProcessor.Process(json);
                        actionState = _commandProcessor.GetAvailableActions(json);
                    }
                    if (!json.Action.Equals("EndTurn"))
					{
                        await SendAvailableActionsTo(actionState, webSocket);
                        await WebSocketServer.Broadcast(webSocket, receivedString);
					}
				}
			}
		}
	}

	private void CloseConnection(Model.Player player)
	{
        Console.WriteLine($"{player.Name} won the game!");
        _webSocket.CloseAsync(
            WebSocketCloseStatus.NormalClosure,
            "WebSocket connection closed",
            CancellationToken.None).Wait();
    }

    private async Task SendAvailableActionsTo(string actionState, WebSocket webSocket)
    {
        await webSocket.SendAsync(
            new ArraySegment<byte>(Encoding.UTF8.GetBytes(actionState)),
            WebSocketMessageType.Text,
            WebSocketMessageFlags.EndOfMessage,
            CancellationToken.None);
    }

    private static async Task SendGameStateTo(WebSocket webSocket)
	{
		var logTransformer = new LogTransformer();
		var gameState = logTransformer.TransformGameState();
		await webSocket.SendAsync(
			new ArraySegment<byte>(Encoding.UTF8.GetBytes(gameState)),
			WebSocketMessageType.Text,
			WebSocketMessageFlags.EndOfMessage,
			CancellationToken.None);
	}

	private static async Task SendResponseTo(WebSocket webSocket, string response)
	{
        await webSocket.SendAsync(
            new ArraySegment<byte>(Encoding.UTF8.GetBytes(response)),
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

	private static async Task SendSettingsTo(WebSocket webSocket)
	{
		var settingsFilepath = Model.GameManager.PropertiesSettingsFilepath;
		var settingsJson = System.IO.File.ReadAllText(settingsFilepath);
		var settings = JsonSerializer.Deserialize<Model.Settings>(settingsJson);

		var mapFilepath = WebSocketServer.MapFilePath;
		var mapJson = System.IO.File.ReadAllText(mapFilepath);
		var map = JsonSerializer.Deserialize<JsonTiles>(mapJson);

		var setup = new
		{
			Type = "Setup",
			Tiles = map,
			Settings = settings,
		};
		var setupJson = JsonSerializer.Serialize(setup);
		await webSocket.SendAsync(
			new ArraySegment<byte>(Encoding.UTF8.GetBytes(setupJson)),
			WebSocketMessageType.Text,
			WebSocketMessageFlags.EndOfMessage,
			CancellationToken.None);
	}

	private class JsonTiles
	{
		public List<List<string>> Tiles { get; set; } = new();
	}

}
