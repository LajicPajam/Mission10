using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BowlingApi.Models;

public class Bowler
{
    [Key]
    public int BowlerID { get; set; }
    public string? BowlerLastName { get; set; }
    public string? BowlerFirstName { get; set; }
    public string? BowlerMiddleInit { get; set; }
    public string? BowlerAddress { get; set; }
    public string? BowlerCity { get; set; }
    public string? BowlerState { get; set; }
    public string? BowlerZip { get; set; }
    public string? BowlerPhoneNumber { get; set; }

    // Foreign key relationship back to Teams.TeamID.
    [ForeignKey("Team")]
    public int? TeamID { get; set; }
    // Navigation property used for team joins in queries.
    public Team? Team { get; set; }
}
