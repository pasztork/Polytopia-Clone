using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServer.Data;

namespace WebServer.Areas.Identity.Pages.Account.Manage;

public class IndexModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public IndexModel(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public DataModel Data { get; set; } = new();

    public class DataModel
    {
        public string Username { get; set; } = string.Empty;
        public string NeptunCode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    private void Load(User user)
    {
        Data = new DataModel
        {
            Username = user.UserName,
            NeptunCode = user.NeptunCode,
            Email = user.Email
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        Load(user);
        return Page();
    }
}