using Lunia.V4.Application.Characters.Dtos;

namespace Lunia.V4.Application.Characters.Commands.GetCharacterList;

public sealed record GetCharacterListResult(
    int SlotCount, 
    string AccountName,
    List<int> ClassNumbers,
    List<CharacterDto> Characters,
    List<CharacterLicenseDto> Licenses,
    List<CharacterItemDto> Items);