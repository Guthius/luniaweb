using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration;

internal sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(item => new
        {
            item.CharacterName,
            item.BagNumber,
            item.PositionNumber
        });

        builder.Property(item => item.CharacterName).HasMaxLength(50);

        builder.HasData(
            new Item
            {
                CharacterName = "YamYam",
                ItemHash = 30957193,
                BagNumber = 60,
                PositionNumber = 0,
                StackedCount = 249,
                ItemSerial = 4505093047646365909
            },
            new Item
            {
                CharacterName = "YamYam",
                ItemHash = 40470131,
                BagNumber = 1,
                PositionNumber = 0,
                StackedCount = 1,
                ItemSerial = 4505093047647042256,
                Instance = 100663296
            },
            new Item
            {
                CharacterName = "YamYam",
                ItemHash = 31536369,
                BagNumber = 1,
                PositionNumber = 1,
                StackedCount = 1,
                ItemSerial = 4505093047647042688,
                Instance = 134217728
            },
            new Item
            {
                CharacterName = "YamYam",
                ItemHash = 39720869,
                BagNumber = 1,
                PositionNumber = 2,
                StackedCount = 1,
                ItemSerial = 4505093047647041772
            },
            new Item
            {
                CharacterName = "YamYam",
                ItemHash = 30757234,
                BagNumber = 1,
                PositionNumber = 3,
                StackedCount = 1,
                ItemSerial = 4505093047647043083,
                Instance = 33554432
            },
            new Item
            {
                CharacterName = "YamYam",
                ItemHash = 22096338,
                BagNumber = 1,
                PositionNumber = 4,
                StackedCount = 1,
                ItemSerial = 4505093047647043489,
                Instance = 100663296
            },
            new Item
            {
                CharacterName = "YamYam",
                ItemHash = 30992610,
                BagNumber = 1,
                PositionNumber = 5,
                StackedCount = 1,
                ItemSerial = 4505093047647077184
            });
    }
}