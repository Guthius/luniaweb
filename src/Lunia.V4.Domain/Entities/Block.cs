namespace Lunia.V4.Domain.Entities;

public sealed record Block
{
    public long Id { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
    public string RemoteIp { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTimeOffset? ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}