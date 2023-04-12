namespace Network;

public class NetworkInterfaceBuilder
{
    public void Start()
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();

        WebApplication app = builder.Build();
        WebSocketOptions webSocketOptions = new()
        {
            KeepAliveInterval = TimeSpan.FromMinutes(2)
        };
        app.UseWebSockets(webSocketOptions);
        app.MapControllers();
        app.Run();
    }
}
