using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Data;
public class ResultItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public int Value { get; set; }

    [Required]
    public int TournamentResultId { get; set; }

}
