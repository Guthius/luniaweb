namespace Lunia.V4.Domain.Entities;

public sealed record SquareStage
{
    public string ServerName { get; set; } = string.Empty;
    public int RoomNumber { get; set; }
    public string SquareName { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int StageGroupHash { get; set; }
    public int AccessLevel { get; set; }
    public int ConnectionCount { get; set; }
    public int OrderNumber { get; set; }
}