using Microsoft.AspNetCore.Identity;

namespace WebServer.Areas.Identity.Data;

public class User : IdentityUser
{
    public string NeptunCode { get; set; } = string.Empty;
}
