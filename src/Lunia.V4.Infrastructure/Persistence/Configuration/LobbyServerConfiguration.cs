using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration;

internal sealed class LobbyServerConfiguration : IEntityTypeConfiguration<LobbyServer>
{
    public void Configure(EntityTypeBuilder<LobbyServer> builder)
    {
        builder.HasKey(server => server.ServerName);

        builder.Property(server => server.ServerName)
            .UseCollation("case_insensitive")
            .HasMaxLength(50);

        builder.Property(server => server.Ip).HasMaxLength(50);
    }
}