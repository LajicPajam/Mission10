using BowlingApi.Controllers;
using BowlingApi.Data;
using BowlingApi.Dtos;
using BowlingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BowlingApi.Tests;

public class BowlersControllerTests
{
    [Fact]
    public async Task GetBowlers_ReturnsOnlyMarlinsAndSharks()
    {
        await using var context = await CreateContextWithSeedData();
        var controller = new BowlersController(context);

        var result = await controller.GetBowlers();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var payload = Assert.IsAssignableFrom<IEnumerable<BowlerDto>>(okResult.Value);

        Assert.NotEmpty(payload);
        Assert.All(payload, b => Assert.Contains(b.TeamName, new[] { "Marlins", "Sharks" }));
    }

    [Fact]
    public async Task GetBowlers_ReturnsRequiredAssignmentFields()
    {
        await using var context = await CreateContextWithSeedData();
        var controller = new BowlersController(context);

        var result = await controller.GetBowlers();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var payload = Assert.IsAssignableFrom<IEnumerable<BowlerDto>>(okResult.Value).ToList();

        var first = Assert.Single(payload, b => b.BowlerLastName == "Fournier");

        Assert.Equal("Barbara", first.BowlerFirstName);
        Assert.Equal("Fournier", first.BowlerLastName);
        Assert.Equal("Marlins", first.TeamName);
        Assert.Equal("67 Willow Drive", first.BowlerAddress);
        Assert.Equal("Bothell", first.BowlerCity);
        Assert.Equal("WA", first.BowlerState);
        Assert.Equal("98123", first.BowlerZip);
        Assert.Equal("(206) 555-9876", first.BowlerPhoneNumber);
    }

    private static async Task<BowlingLeagueContext> CreateContextWithSeedData()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<BowlingLeagueContext>()
            .UseSqlite(connection)
            .Options;

        var context = new BowlingLeagueContext(options);
        await context.Database.EnsureCreatedAsync();

        context.Teams.AddRange(
            new Team { TeamID = 1, TeamName = "Marlins" },
            new Team { TeamID = 2, TeamName = "Sharks" },
            new Team { TeamID = 3, TeamName = "Terrapins" }
        );

        context.Bowlers.AddRange(
            new Bowler
            {
                BowlerID = 1,
                BowlerFirstName = "Barbara",
                BowlerLastName = "Fournier",
                BowlerAddress = "67 Willow Drive",
                BowlerCity = "Bothell",
                BowlerState = "WA",
                BowlerZip = "98123",
                BowlerPhoneNumber = "(206) 555-9876",
                TeamID = 1
            },
            new Bowler
            {
                BowlerID = 2,
                BowlerFirstName = "Neil",
                BowlerLastName = "Patterson",
                BowlerAddress = "16 Maple Lane",
                BowlerCity = "Auburn",
                BowlerState = "WA",
                BowlerZip = "98002",
                BowlerPhoneNumber = "(206) 555-3487",
                TeamID = 2
            },
            new Bowler
            {
                BowlerID = 3,
                BowlerFirstName = "Blocked",
                BowlerLastName = "Team",
                TeamID = 3
            }
        );

        await context.SaveChangesAsync();
        return context;
    }
}
