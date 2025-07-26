using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration.Lobby;

internal sealed class SelectedCharacterConfiguration : IEntityTypeConfiguration<SelectedCharacter>
{
    public void Configure(EntityTypeBuilder<SelectedCharacter> builder)
    {
        builder.HasKey(selectedCharacter => new
        {
            selectedCharacter.AccountName,
            selectedCharacter.CharacterName
        });

        builder.Property(selectedCharacter => selectedCharacter.AccountName).HasMaxLength(50);
        builder.Property(selectedCharacter => selectedCharacter.CharacterName).HasMaxLength(50);

        builder.HasOne<Account>().WithMany()
            .HasPrincipalKey(account => account.AccountName)
            .HasForeignKey(selectedCharacter => selectedCharacter.AccountName)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Character>().WithMany()
            .HasPrincipalKey(character => character.CharacterName)
            .HasForeignKey(selectedCharacter => selectedCharacter.CharacterName)
            .OnDelete(DeleteBehavior.Cascade);
    }
}