namespace WebServer.Data;

using Microsoft.AspNetCore.Components.Forms;

public class Submission
{
    public string Name { get; set; } = string.Empty;

    public string Neptun { get; set; } = string.Empty;

    public IBrowserFile? File { get; set; }
}
