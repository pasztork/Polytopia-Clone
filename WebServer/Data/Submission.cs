using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Data;

public class Submission
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(UserId))]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string OriginalFileName { get; set; } = string.Empty;

    [Required]
    public string FileName { get; set; } = string.Empty;
}
