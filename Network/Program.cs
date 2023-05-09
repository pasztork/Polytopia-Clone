namespace Network;

public class Program
{
    public static void Main()
    {
        WebSocketServer.MapFilePath =
            $"{Directory.GetCurrentDirectory()}\\Maps\\2023-04-01_00-20-33.json";
        WebSocketServer.Start();
        var builder = new NetworkInterfaceBuilder();
        builder.Start();
    }
}