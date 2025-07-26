namespace Lunia.V4.Domain.Entities;

public sealed record StageServer
{
    public string ServerName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Port { get; set; }
    public int RoomCount { get; set; }
    public bool IsSquare { get; set; }
    public int CongestionLevel { get; set; }
    public DateTimeOffset LastUpAt { get; set; } = DateTimeOffset.UtcNow;
}