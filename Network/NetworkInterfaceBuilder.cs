namespace Network;

public class NetworkInterfaceBuilder
{
    private static readonly string SERVER_ADDRESS = "ip+port";
    private static readonly string SERVER_URL = $"http://{SERVER_ADDRESS}";
    private static WebApplication app;
    public void Start(List<string> clients)
    {
        Environment.SetEnvironmentVariable("TERRA_IMPERIUM_SERVER_ADDRESS", SERVER_ADDRESS);

        WebSocketServer.OnAllClientsDisconnected += Stop;
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls(SERVER_URL);
        builder.Services.AddControllers();

        app = builder.Build();
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

    public void Stop()
    {
        app.StopAsync().Wait();
    }
}
