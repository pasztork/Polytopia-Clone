namespace Network;

public class Program
{
    public static void Main(string[] args)
    {
        WebSocketServer.MapFilePath = $"{Directory.GetParent(Directory.GetCurrentDirectory())?.FullName}\\Network\\Maps\\{args[0]}";
        WebSocketServer.Start();
        var builder = new NetworkInterfaceBuilder();
        builder.Start(args.ToList().Skip(1).ToList());
    }
}