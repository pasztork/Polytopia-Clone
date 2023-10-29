using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServer.Areas.Identity.Constants;
using WebServer.Data;

namespace WebServer.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterModel(
        UserManager<User> userManager, 
        SignInManager<User> signInManager, 
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string ReturnUrl { get; set; } = string.Empty;

    public class InputModel
    {
        [Required]
        [Display(Name = "Your name")]
        [DataType(DataType.Text)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(6, ErrorMessage = "Neptun code must be exactly 6 characters long.", MinimumLength = 6)]
        [Display(Name = "Neptun code")]
        public string NeptunCode { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }


    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl ?? string.Empty;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (ModelState.IsValid)
        {
            User user = new User() { UserName = Input.UserName, NeptunCode = Input.NeptunCode, Email = Input.Email };
            IdentityResult result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                User newUser = await _userManager.FindByEmailAsync(Input.Email);
                await _userManager.AddToRoleAsync(newUser, Roles.UserRole);

                if (_userManager.Options.SignIn.RequireConfirmedEmail)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                }
                else
                {
                    return LocalRedirect(returnUrl);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}
