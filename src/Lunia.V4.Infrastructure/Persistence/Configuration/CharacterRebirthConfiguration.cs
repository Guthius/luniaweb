using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration;

internal sealed class CharacterRebirthConfiguration : IEntityTypeConfiguration<CharacterRebirth>
{
    public void Configure(EntityTypeBuilder<CharacterRebirth> builder)
    {
        builder.HasKey(characterRebirth => characterRebirth.CharacterName);
        
        builder.Property(characterRebirth => characterRebirth.CharacterName).HasMaxLength(50);
        
        builder.HasData(
            new CharacterRebirth
            {
                CharacterName = "Mordio",
                RebirthCount = 1,
                StoredLevel = 100,
                SkillPoint = 5,
                LastRebirthAt = new DateTimeOffset(2020, 3, 18, 2, 42, 52, TimeSpan.Zero)
            },
            new CharacterRebirth
            {
                CharacterName = "YamYam",
                RebirthCount = 3,
                StoredLevel = 102,
                SkillPoint = 15,
                LastRebirthAt = new DateTimeOffset(2021, 3, 18, 2, 38, 21, TimeSpan.Zero)
            });
    }
}