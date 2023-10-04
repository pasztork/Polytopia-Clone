namespace Network.Client;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Diagnostics;
using System.IO.Compression;

public static class ClientManager
{
    private static readonly string ROOT_FOLDER_PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
    private static readonly string COMP_FOLDER_PATH = Path.Combine(ROOT_FOLDER_PATH, "Network\\Client\\ClientFiles\\Compressed");
    private static readonly string EXTR_FOLDER_PATH = Path.Combine(ROOT_FOLDER_PATH, "Network\\Client\\ClientFiles\\Extracted");
    private static int defaultPort = 53657;
    public static async Task StartClients(string serverAddress, List<string> clients)
    {
        if(clients.Count == 0)
        {
            Console.WriteLine("No clients connected automatically, waiting for connections");
            return;
        }

        for(int i = 0; i < clients.Count; i++)
        {
            Unzip(clients[i]);
            CreateDockerContainer(clients[i], serverAddress, defaultPort + i);
        }
        await StartContainers();
    }

    public static async Task RemoveClients()
    {
        await RemoveContainers();
    }

    private static void CreateDockerContainer(string name, string serverAddress, int port)
    {
        string buildCommand = $"docker build -t {name} .";
        RunShellCommand(buildCommand, name);

        string runCommand = $"docker create -p {port}:53658 -e SERVER_ADDRESS={serverAddress} {name}";
        RunShellCommand(runCommand, name);
    }

    private static void RunShellCommand(string command, string name)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = Path.Combine(EXTR_FOLDER_PATH, name)
        };

        Process process = new Process { StartInfo = psi };
        process.Start();
        process.StandardInput.WriteLine(command);
        process.StandardInput.WriteLine("exit");
        process.WaitForExit();
        process.Close();
    }

    private static async Task StartContainers()
    {
        using DockerClient client = new DockerClientConfiguration().CreateClient();

        var containers = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true });

        foreach (var container in containers)
        {
            await client.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());
        }
    }

    private static async Task RemoveContainers()
    {
        using DockerClient client = new DockerClientConfiguration().CreateClient();

        var containers = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true });
        Console.WriteLine("Konténerek száma: "+ containers.Count);
        foreach(var container in containers)
        {
            await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters());
        }
    }

    private static void Unzip(string fileName)
    {
        if(Directory.Exists(Path.Combine(EXTR_FOLDER_PATH, fileName)))
        {
            return;
        }
        string zipFilePath = Path.Combine(COMP_FOLDER_PATH, fileName + ".zip");

        try
        {
            ZipFile.ExtractToDirectory(zipFilePath, Path.Combine(EXTR_FOLDER_PATH, fileName));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}