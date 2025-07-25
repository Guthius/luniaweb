namespace Lunia.V4.Application.Characters.Dtos;

public sealed record CharacterItemDto(
    string CharacterName, 
    int BagNumber, 
    int PositionNumber, 
    int ItemHash, 
    long Instance);