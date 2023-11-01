using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Data;

public class TournamentResult
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    public string MapFileName { get; set; } = string.Empty;

    [Required]
    public string TournamentType { get; set; } = string.Empty;
    
    [Required]
    public bool Finished { get; set; } = false;

    public ICollection<ResultItem> ResultItems { get; set; } = new List<ResultItem>();
}
