using System.ComponentModel.DataAnnotations;

namespace BowlingApi.Models;

public class Team
{
    [Key]
    public int TeamID { get; set; }
    // TeamName values are used by the API filter (Marlins/Sharks).
    public string TeamName { get; set; } = string.Empty;
    public int? CaptainID { get; set; }
}
