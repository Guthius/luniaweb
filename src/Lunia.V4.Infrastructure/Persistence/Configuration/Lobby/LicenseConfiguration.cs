using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration.Lobby;

internal sealed class LicenseConfiguration : IEntityTypeConfiguration<License>
{
    public void Configure(EntityTypeBuilder<License> builder)
    {
        builder.HasKey(license => new
        {
            license.CharacterName,
            license.StageGroupHash
        });

        builder.Property(license => license.CharacterName).HasMaxLength(50);
    }
}