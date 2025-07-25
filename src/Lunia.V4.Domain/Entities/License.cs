namespace Lunia.V4.Domain.Entities;

public sealed record License
{
    public string CharacterName { get; set; } = string.Empty;
    public int StageGroupHash { get; set; }
    public int AccessLevel { get; set; }
}