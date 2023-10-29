using Microsoft.AspNetCore.Identity;

namespace WebServer.Data;

public class User : IdentityUser
{
    public string NeptunCode { get; set; } = string.Empty;
}
