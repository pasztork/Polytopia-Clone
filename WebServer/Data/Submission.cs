namespace WebServer.Data;

using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

public class Submission
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Neptun code must be exactly 6 characters long.")]
    public string Neptun { get; set; } = string.Empty;

    [Required(ErrorMessage = "File is required")]
    [FileExtensions(Extensions = ".zip", ErrorMessage = "File extension must be zip.")]
    public IBrowserFile? File { get; set; }
}
