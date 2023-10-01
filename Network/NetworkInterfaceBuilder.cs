using Microsoft.AspNetCore.Hosting;

namespace Network;

public class NetworkInterfaceBuilder
{
    private static readonly string SERVER_URL = "http://localhost:53658";
    public void Start(List<string> clients)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls(SERVER_URL);
        builder.Services.AddControllers();

        WebApplication app = builder.Build();
        WebSocketOptions webSocketOptions = new()
        {
            KeepAliveInterval = TimeSpan.FromMinutes(2)
        };
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            Client.ClientManager.StartClients(clients).Wait();
        });
        app.UseWebSockets(webSocketOptions);
        app.MapControllers();
        app.Run();
    }
}
