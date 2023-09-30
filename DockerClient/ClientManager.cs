namespace Client;
using Docker.DotNet;
using Docker.DotNet.Models;

public static class ClientManager
{
    public static async Task StartClients()
    {
        await StartContainer("390498cdb1ccf066248b589ae001f13094e0d16707b2afbd69988c6f214b53cd");
        await StartContainer("2848ecf499e9da73b0911ce297cc7d46c755a879063ea39a2031daf0e418b6a3");
    }

    private static async Task StartContainer(string name)
    {
        using (DockerClient client = new DockerClientConfiguration().CreateClient())
        {
            await client.Containers.StartContainerAsync(name, new ContainerStartParameters());
        }
    }
}