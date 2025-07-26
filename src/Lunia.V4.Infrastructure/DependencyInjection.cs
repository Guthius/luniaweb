using Lunia.V4.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lunia.V4.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(TimeProvider.System);

        services.AddDbContext<LobbyDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("Database"),
                o => o.MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: LobbyDbContext.SchemaName))
            .UseSnakeCaseNamingConvention());

        services.AddDbContext<StageDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("Database"),
                o => o.MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: StageDbContext.SchemaName))
            .UseSnakeCaseNamingConvention());
    }
}