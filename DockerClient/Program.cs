namespace Client;
using Docker.DotNet;
using Docker.DotNet.Models;

public class Program
{
    public static async Task Main()
    {
        await StartContainer("container_name");
        await StartContainer("container_name");
    }

    private static async Task StartContainer(string name)
    {
        using (DockerClient client = new DockerClientConfiguration().CreateClient())
        {
            await client.Containers.StartContainerAsync(name, new ContainerStartParameters());
        }
    }
}