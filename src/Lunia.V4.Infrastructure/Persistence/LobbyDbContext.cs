using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lunia.V4.Infrastructure.Persistence;

public sealed class LobbyDbContext(DbContextOptions<LobbyDbContext> options) : DbContext(options)
{
    public const string SchemaName = "lobby";
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaName);

        modelBuilder.HasCollation("case_insensitive",
            locale: "und-u-ks-level2",
            provider: "icu",
            deterministic: false);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LobbyDbContext).Assembly);
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Block> Blocking => Set<Block>();
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<CharacterLicense> CharacterLicenses => Set<CharacterLicense>();
    public DbSet<CharacterRebirth> CharacterRebirths => Set<CharacterRebirth>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<License> Licenses => Set<License>();
    public DbSet<LobbyConnection> LobbyConnections => Set<LobbyConnection>();
    public DbSet<LobbyServer> LobbyServers => Set<LobbyServer>();
    public DbSet<SelectedCharacter> SelectedCharacters => Set<SelectedCharacter>();
}