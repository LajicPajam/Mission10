using System.ComponentModel.DataAnnotations;

namespace BowlingApi.Models;

public class Team
{
    [Key]
    public int TeamID { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public int? CaptainID { get; set; }
}
