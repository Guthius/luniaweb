using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration;

internal sealed class LobbyConnectionConfiguration : IEntityTypeConfiguration<LobbyConnection>
{
    public void Configure(EntityTypeBuilder<LobbyConnection> builder)
    {
        builder.HasKey(connection => new
        {
            connection.ServerName,
            connection.AccountName
        });

        builder.Property(connection => connection.ServerName)
            .UseCollation("case_insensitive")
            .HasMaxLength(50);

        builder.Property(connection => connection.AccountName).HasMaxLength(50);

        builder.HasOne<LobbyServer>().WithMany()
            .HasPrincipalKey(server => server.ServerName)
            .HasForeignKey(connection => connection.ServerName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Account>().WithMany()
            .HasPrincipalKey(account => account.AccountName)
            .HasForeignKey(connection => connection.AccountName)
            .OnDelete(DeleteBehavior.Cascade);
    }
}