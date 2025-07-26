using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration.Stage;

internal sealed class StageServerConfiguration : IEntityTypeConfiguration<StageServer>
{
    public void Configure(EntityTypeBuilder<StageServer> builder)
    {
        builder.HasKey(server => server.ServerName);

        builder.Property(server => server.ServerName)
            .UseCollation("case_insensitive")
            .HasMaxLength(50);

        builder.Property(server => server.Address).HasMaxLength(50);

        builder.HasData(new StageServer
        {
            ServerName = "Square Server",
            Address = "192.168.0.30",
            Port = 15554,
            RoomCount = 50,
            IsSquare = true,
            LastUpAt = new DateTimeOffset(2021, 3, 18, 2, 20, 37, TimeSpan.Zero)
        });
    }
}