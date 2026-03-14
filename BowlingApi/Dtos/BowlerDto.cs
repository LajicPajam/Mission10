namespace BowlingApi.Dtos;

public class BowlerDto
{
    public string BowlerFirstName { get; set; } = string.Empty;
    public string? BowlerMiddleInit { get; set; }
    public string BowlerLastName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public string? BowlerAddress { get; set; }
    public string? BowlerCity { get; set; }
    public string? BowlerState { get; set; }
    public string? BowlerZip { get; set; }
    public string? BowlerPhoneNumber { get; set; }
}
