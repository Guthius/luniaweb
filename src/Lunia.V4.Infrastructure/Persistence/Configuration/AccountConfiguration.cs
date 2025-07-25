using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration;

public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(account => account.AccountName);

        builder.Property(account => account.AccountName)
            .UseCollation("case_insensitive")
            .HasMaxLength(50);

        builder.HasMany<Character>().WithOne()
            .HasPrincipalKey(account => account.AccountName)
            .HasForeignKey(character => character.AccountName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<CharacterLicense>().WithOne()
            .HasPrincipalKey(account => account.AccountName)
            .HasForeignKey(characterLicense => characterLicense.AccountName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(new Account
        {
            AccountName = "kaneshaw",
            PasswordHash = "a7a59ae9ae908f86ff05250e113eda17",
            SlotCount = 4,
            CharacterCount = 2,
            CreatedAt = new DateTimeOffset(2021, 3, 18, 2, 47, 43, TimeSpan.Zero),
            LastLoggedAt = null
        });
    }
}