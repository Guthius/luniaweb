namespace Lunia.V4.Domain.Entities;

public sealed record SelectedCharacter
{
    public string AccountName { get; set; } = string.Empty;
    public string CharacterName { get; set; } = string.Empty;
}