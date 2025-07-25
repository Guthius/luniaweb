using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration;

internal sealed class CharacterLicenseConfiguration : IEntityTypeConfiguration<CharacterLicense>
{
    public void Configure(EntityTypeBuilder<CharacterLicense> builder)
    {
        builder.HasKey(characterLicense => new
        {
            characterLicense.AccountName,
            characterLicense.ClassNumber
        });

        builder.Property(characterLicense => characterLicense.AccountName).HasMaxLength(50);
    }
}