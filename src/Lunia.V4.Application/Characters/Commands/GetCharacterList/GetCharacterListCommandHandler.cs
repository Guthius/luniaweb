using LanguageExt;
using LanguageExt.Common;
using Lunia.V4.Application.Characters.Dtos;
using Lunia.V4.Application.Common;
using Lunia.V4.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lunia.V4.Application.Characters.Commands.GetCharacterList;

internal sealed class GetCharacterListCommandHandler(LobbyDbContext context) : ICommandHandler<GetCharacterListCommand, GetCharacterListResult>
{
    public async Task<Either<Error, GetCharacterListResult>> Handle(GetCharacterListCommand command, CancellationToken cancellationToken)
    {
        var slotCount = await context.Accounts
            .Where(predicate: account =>
                account.AccountName == command.AccountName)
            .Select(selector: account =>
                account.SlotCount)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        var classNumbers = await context.CharacterLicenses
            .Where(predicate: characterLicense =>
                characterLicense.AccountName == command.AccountName)
            .Select(selector: characterLicense =>
                characterLicense.ClassNumber)
            .Distinct()
            .ToListAsync(cancellationToken: cancellationToken);

        var characterDtos = await context.Characters
            .Where(predicate: character =>
                character.AccountName == command.AccountName &&
                character.IsDeleted == false)
            .GroupJoin(inner: context.CharacterRebirths,
                outerKeySelector: x => x.CharacterName,
                innerKeySelector: y => y.CharacterName,
                resultSelector: (x, y) => new
                {
                    Character = x,
                    CharacterRebirths = y
                })
            .SelectMany(collectionSelector: x => x.CharacterRebirths
                    .DefaultIfEmpty()
                    .DefaultIfEmpty(),
                resultSelector: (x, rebirth) => new CharacterDto(
                    x.Character.CharacterName,
                    x.Character.VirtualIdCode,
                    x.Character.ClassNumber,
                    x.Character.StageLevel,
                    x.Character.StageExp,
                    x.Character.PvpLevel,
                    x.Character.PvpExp,
                    x.Character.WarLevel,
                    x.Character.WarExp,
                    x.Character.LastLoggedAt,
                    x.Character.InstantStateFlags,
                    rebirth != null ? rebirth.RebirthCount : 0,
                    rebirth != null ? rebirth.StoredLevel : 0))
            .ToListAsync(cancellationToken: cancellationToken);

        var characterLicenseDtos = await context.Characters
            .Where(predicate: character =>
                character.AccountName == command.AccountName &&
                character.IsDeleted == false)
            .Join(inner: context.Licenses,
                outerKeySelector: character => character.CharacterName,
                innerKeySelector: license => license.CharacterName,
                resultSelector: (character, license) => new CharacterLicenseDto(
                    character.CharacterName,
                    license.StageGroupHash,
                    license.AccessLevel))
            .ToListAsync(cancellationToken: cancellationToken);

        var characterItemDtos = await context.Characters
            .Where(predicate: character =>
                character.AccountName == command.AccountName &&
                character.IsDeleted == false)
            .Join(inner: context.Items.Where(predicate: item =>
                    item.IsOnBank == false &&
                    item.BagNumber >= 0 &&
                    item.BagNumber <= 99),
                outerKeySelector: character => character.CharacterName,
                innerKeySelector: item => item.CharacterName,
                resultSelector: (character, item) => new CharacterItemDto(
                    character.CharacterName,
                    item.BagNumber,
                    item.PositionNumber,
                    item.ItemHash,
                    item.Instance))
            .ToListAsync(cancellationToken: cancellationToken);

        return new GetCharacterListResult(
            SlotCount: slotCount,
            AccountName: command.AccountName,
            ClassNumbers: classNumbers,
            Characters: characterDtos,
            Licenses: characterLicenseDtos,
            Items: characterItemDtos);
    }
}