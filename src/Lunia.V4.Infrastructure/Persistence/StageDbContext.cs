using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lunia.V4.Infrastructure.Persistence;

public sealed class StageDbContext(DbContextOptions<StageDbContext> options) : DbContext(options)
{
    public const string SchemaName = "stage";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaName);

        modelBuilder.HasCollation("case_insensitive",
            locale: "und-u-ks-level2",
            provider: "icu",
            deterministic: false);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(StageDbContext).Assembly,
            type => type.Namespace != null &&
                    type.Namespace.EndsWith(".Stage"));
    }

    public DbSet<SquareStage> SquareStages => Set<SquareStage>();
    public DbSet<StageServer> StageServers => Set<StageServer>();
}