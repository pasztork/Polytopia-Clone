namespace Network;

public class Program
{
    public static void Main(string[] args)
    {
        // args = new string[] { "2023-04-01_00-20-33.json", "admin1_20231108094714", "admin1_20231108094718" };
        WebSocketServer.MapFilePath = $"{Directory.GetParent(Directory.GetCurrentDirectory())?.FullName}\\Network\\Maps\\{args[0]}";
        WebSocketServer.Start();
        var builder = new NetworkInterfaceBuilder();
        builder.Start(args.ToList().Skip(1).ToList());
    }
}