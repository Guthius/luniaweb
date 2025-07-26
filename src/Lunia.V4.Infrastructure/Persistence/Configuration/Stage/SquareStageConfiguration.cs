using Lunia.V4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lunia.V4.Infrastructure.Persistence.Configuration.Stage;

internal sealed class SquareStageConfiguration : IEntityTypeConfiguration<SquareStage>
{
    public void Configure(EntityTypeBuilder<SquareStage> builder)
    {
        builder.HasKey(stage => new
        {
            stage.ServerName,
            stage.RoomNumber
        });

        builder.Property(stage => stage.ServerName).HasMaxLength(50);
        builder.Property(stage => stage.SquareName).HasMaxLength(50);

        builder.HasData(
            new SquareStage {ServerName = "Square Server", RoomNumber = 0, SquareName = "Main Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 0},
            new SquareStage {ServerName = "Square Server", RoomNumber = 1, SquareName = "Myth Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 11},
            new SquareStage {ServerName = "Square Server", RoomNumber = 2, SquareName = "PVP Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 12},
            new SquareStage {ServerName = "Square Server", RoomNumber = 3, SquareName = "Beginners Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 3},
            new SquareStage {ServerName = "Square Server", RoomNumber = 4, SquareName = "Slime Racing Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 2},
            new SquareStage {ServerName = "Square Server", RoomNumber = 5, SquareName = "Episode 1 Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 4},
            new SquareStage {ServerName = "Square Server", RoomNumber = 6, SquareName = "Episode 2 Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 5},
            new SquareStage {ServerName = "Square Server", RoomNumber = 7, SquareName = "Episode 3 Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 6},
            new SquareStage {ServerName = "Square Server", RoomNumber = 8, SquareName = "Episode 4 Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 7},
            new SquareStage {ServerName = "Square Server", RoomNumber = 9, SquareName = "Episode 5 Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 8},
            new SquareStage {ServerName = "Square Server", RoomNumber = 10, SquareName = "Episode 6 Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 9},
            new SquareStage {ServerName = "Square Server", RoomNumber = 11, SquareName = "Episode 7 Square", Capacity = 70, StageGroupHash = 53518598, AccessLevel = 10});
    }
}