namespace Network;

public class NetworkInterfaceBuilder
{
    private static readonly string? SERVER_ADDRESS = Environment.GetEnvironmentVariable("TERRA_IMPERIUM_SERVER_ADDRESS");
    private static readonly string SERVER_URL = $"http://{SERVER_ADDRESS}";
    private WebApplication? app;
    public void Start(List<string> clients)
    {
        if(SERVER_ADDRESS == null)
        {
            Console.WriteLine("Server address needs to be set as an environment variable");
            return;
        }
        WebSocketServer.OnEverythingFinished += Stop;
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
            Client.ClientManager.StartClients(SERVER_ADDRESS, clients).Wait();
        });
        app.UseWebSockets(webSocketOptions);
        app.MapControllers();
        app.Run();
    }

    public void Stop()
    {
        Client.ClientManager.RemoveClients().Wait();
        app!.StopAsync().Wait();
    }
}
