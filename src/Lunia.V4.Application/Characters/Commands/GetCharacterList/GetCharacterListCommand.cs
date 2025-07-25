using Lunia.V4.Application.Common;

namespace Lunia.V4.Application.Characters.Commands.GetCharacterList;

public sealed record GetCharacterListCommand(string AccountName) : ICommand<GetCharacterListResult>;