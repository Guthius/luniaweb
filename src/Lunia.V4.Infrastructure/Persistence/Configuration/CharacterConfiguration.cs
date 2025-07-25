using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration;

internal sealed class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(character => character.CharacterName);

        builder.Property(character => character.AccountName).HasMaxLength(50);
        builder.Property(character => character.CharacterName).HasMaxLength(50);

        builder.HasIndex(character => character.IsDeleted);

        builder.HasOne<CharacterRebirth>().WithOne()
            .HasPrincipalKey<Character>(character => character.CharacterName)
            .HasForeignKey<CharacterRebirth>(characterRebirth => characterRebirth.CharacterName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Item>().WithOne()
            .HasPrincipalKey(character => character.CharacterName)
            .HasForeignKey(item => item.CharacterName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<License>().WithOne()
            .HasPrincipalKey(character => character.CharacterName)
            .HasForeignKey(license => license.CharacterName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Character
            {
                AccountName = "kaneshaw",
                CharacterName = "YamYam",
                VirtualIdCode = 2130706433,
                ClassNumber = 0,
                StageLevel = 99,
                PvpLevel = 1,
                WarLevel = 1,
                GameMoney = 10999999,
                SkillPoint = 200,
                CreatedAt = new DateTimeOffset(2021, 3, 18, 2, 5, 35, TimeSpan.Zero)
            },
            new Character
            {
                AccountName = "kaneshaw",
                CharacterName = "Mordio",
                VirtualIdCode = 2130706434,
                ClassNumber = 13,
                StageLevel = 99,
                PvpLevel = 1,
                WarLevel = 1,
                SkillPoint = 6,
                CreatedAt = new DateTimeOffset(2021, 3, 18, 2, 40, 13, TimeSpan.Zero)
            });
    }
}