using BowlingApi.Data;
using BowlingApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BowlingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BowlersController(BowlingLeagueContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BowlerDto>>> GetBowlers()
    {
        // Assignment requirement: only include bowlers on Marlins or Sharks.
        var bowlers = await context.Bowlers
            .Include(b => b.Team)
            .Where(b => b.Team != null && (b.Team.TeamName == "Marlins" || b.Team.TeamName == "Sharks"))
            .OrderBy(b => b.BowlerLastName)
            .ThenBy(b => b.BowlerFirstName)
            // Project only the fields the assignment asks the UI to display.
            .Select(b => new BowlerDto
            {
                BowlerFirstName = b.BowlerFirstName ?? string.Empty,
                BowlerMiddleInit = b.BowlerMiddleInit,
                BowlerLastName = b.BowlerLastName ?? string.Empty,
                TeamName = b.Team!.TeamName,
                BowlerAddress = b.BowlerAddress,
                BowlerCity = b.BowlerCity,
                BowlerState = b.BowlerState,
                BowlerZip = b.BowlerZip,
                BowlerPhoneNumber = b.BowlerPhoneNumber
            })
            .ToListAsync();

        return Ok(bowlers);
    }
}
