namespace Network.Client;
using Docker.DotNet;
using Docker.DotNet.Models;
using System.Diagnostics;
using System.IO.Compression;

public static class ClientManager
{
    private static readonly string? ROOT_FOLDER_PATH = Environment.GetEnvironmentVariable("TERRA_IMPERIUM_PROJECT_ROOT");
    private static readonly string COMP_FOLDER_PATH = Path.Combine(ROOT_FOLDER_PATH!, "Network\\Client\\ClientFiles\\Compressed");
    private static readonly string EXTR_FOLDER_PATH = Path.Combine(ROOT_FOLDER_PATH!, "Network\\Client\\ClientFiles\\Extracted");
    private static readonly int defaultPort = 53657;
    private static List<string> _clients = new List<string>();

    public static async Task StartClients(string serverAddress, List<string> clients)
    {
        if(clients.Count == 0)
        {
            Console.WriteLine("No clients connected automatically, waiting for connections");
            return;
        }
        _clients = clients;
        for(int i = 0; i < _clients.Count; i++)
        {
            Unzip(_clients[i]);
            CreateDockerContainer(_clients[i], serverAddress, defaultPort + i);
        }
        await StartContainers();
    }

    public static async Task RemoveClients()
    {
        await RemoveContainers();
    }

    private static void CreateDockerContainer(string folderName, string serverAddress, int port)
    {
        string buildCommand = $"docker build -t {folderName} .";
        RunShellCommand(buildCommand, folderName);

        string playerName = AssemblePlayerName(folderName);
        string runCommand = $"docker create -p {port}:53658 -e SERVER_ADDRESS={serverAddress} -e PLAYER_NAME=\"{playerName}\" {folderName}";
        RunShellCommand(runCommand, folderName);
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
            if (_clients.Contains(container.Image))
            {
                await client.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());
            }
        }
    }

    private static async Task RemoveContainers()
    {
        using DockerClient client = new DockerClientConfiguration().CreateClient();
        var containers = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true });

        foreach(var container in containers)
        {
            if (_clients.Contains(container.Image))
            {
                await client.Containers.StopContainerAsync(container.ID, new ContainerStopParameters());
                await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters());
            }
        }
        _clients.Clear();
    }

    public static async Task RemoveImages(List<string> imageNames)
    {
        using DockerClient client = new DockerClientConfiguration().CreateClient();

        foreach(var imageName in imageNames)
        {
            await client.Images.DeleteImageAsync(imageName, new ImageDeleteParameters());
        }
    }

    private static void Unzip(string zipFileName)
    {
        if(Directory.Exists(Path.Combine(EXTR_FOLDER_PATH, zipFileName)))
        {
            return;
        }
        string zipFilePath = Path.Combine(COMP_FOLDER_PATH, zipFileName + ".zip");

        try
        {
            ZipFile.ExtractToDirectory(zipFilePath, Path.Combine(EXTR_FOLDER_PATH, zipFileName));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static string AssemblePlayerName(string folderName)
    {
        string[] words = folderName.Split('_');
        for(int i = 0; i < words.Length; i++)
        {
            char[] charArray = words[i].ToCharArray();
            charArray[0] = char.ToUpper(charArray[0]);
            words[i] = new(charArray);
        }
        return string.Join(" ", words);
    }
}