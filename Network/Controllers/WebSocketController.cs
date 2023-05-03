using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using ViewUtil;

namespace Network.Controllers;

public class WebSocketController : ControllerBase
{
    private NetworkCommandProcessor _commandProcessor = new();

   
    private bool gameEnd = false;
   
    //bolean ahhoz hogy mikor induljon el -> manualresetevent de az szálakhoz van
    //async while ciklus amig nincsennek meg a jatekosok
    
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            var succes =  WebSocketServer.Register(webSocket);
           

            if (succes)
            {
                Model.GameManager.Get<Model.TurnManagerBase>().OnWinnerDecided += CloseConnection;
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            }
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private async Task Echo(WebSocket webSocket)
    { 

        // create buffer for messages arriving from client
        byte[] buffer = new byte[4096];
        WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        WebSocketServer.CreatePlayer(Encoding.UTF8.GetString(buffer), webSocket);

        // process messages from client
        while (webSocket.State == WebSocketState.Open && !gameEnd)
        {
            // wait for next message
            await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            string receivedString = Encoding.UTF8.GetString(buffer);

            if (WebSocketServer.IsCurrentPlyer(webSocket))
            {
                bool succes = _commandProcessor.Process(receivedString);
                if (succes)
                {
                    WebSocketServer.Broadcast(receivedString);
                    Console.WriteLine(receivedString);
                }
            }
            Array.Clear(buffer);
        }

        // close websocket
        await webSocket.CloseAsync(
            WebSocketCloseStatus.NormalClosure,
            "WebSocket connection closed",
            CancellationToken.None);
    }

    public void CloseConnection(Model.Player player)
    {
        gameEnd = true;
    }
}
