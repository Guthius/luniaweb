namespace Lunia.V4.Domain.Entities;

public sealed record CharacterRebirth
{
    public string CharacterName { get; set; } = string.Empty;
    public int RebirthCount { get; set; }
    public int StoredLevel { get; set; }
    public int SkillPoint { get; set; }
    public DateTimeOffset LastRebirthAt { get; set; }
    public int GmLevel { get; set; }
}