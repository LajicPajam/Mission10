using BowlingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingApi.Data;

public class BowlingLeagueContext(DbContextOptions<BowlingLeagueContext> options) : DbContext(options)
{
    public DbSet<Bowler> Bowlers => Set<Bowler>();
    public DbSet<Team> Teams => Set<Team>();
}
