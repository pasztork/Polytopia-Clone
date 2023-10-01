namespace Client;
using Docker.DotNet;
using Docker.DotNet.Models;

public static class ClientManager
{
    public static async Task StartClients(List<string> clients)
    {
        foreach (var client in clients)
        {
            await StartContainer(client);
        }
    }

    private static async Task StartContainer(string name)
    {
        using (DockerClient client = new DockerClientConfiguration().CreateClient())
        {
            await client.Containers.StartContainerAsync(name, new ContainerStartParameters());
        }
    }
}