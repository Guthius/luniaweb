namespace Lunia.V4.Domain.Entities;

public sealed record Account
{
    public string AccountName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int SlotCount { get; set; } = 4;
    public int CharacterCount { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? LastLoggedAt { get; set; }
}