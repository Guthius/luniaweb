namespace Lunia.V4.Application.Characters.Dtos;

public sealed record CharacterDto(
    string CharacterName,
    int VirtualIdCode,
    int ClassNumber,
    int StageLevel,
    int StageExp,
    int PvpLevel,
    int PvpExp,
    int WarLevel,
    int WarExp,
    DateTimeOffset? LastLoggedAt,
    int InstantStateFlags,
    int RebirthCount,
    int StoredLevel);