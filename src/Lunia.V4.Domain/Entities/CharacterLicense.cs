namespace Lunia.V4.Domain.Entities;

public sealed record CharacterLicense
{
    public string AccountName { get; set; } = string.Empty;
    public int ClassNumber { get; set; }
}