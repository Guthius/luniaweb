namespace Lunia.V4.Application.Characters.Dtos;

public sealed record CharacterLicenseDto(
    string CharacterName, 
    int StageGroupHash, 
    int AccessLevel);