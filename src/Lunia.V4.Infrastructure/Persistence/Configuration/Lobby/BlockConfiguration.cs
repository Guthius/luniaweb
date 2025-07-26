using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration.Lobby;

internal sealed class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.HasKey(block => block.Id);

        builder.Property(block => block.AccountName)
            .UseCollation("case_insensitive")
            .HasMaxLength(50);
        
        builder.Property(block => block.CharacterName).HasMaxLength(50);
        builder.Property(block => block.RemoteIp).HasMaxLength(50);
    }
}