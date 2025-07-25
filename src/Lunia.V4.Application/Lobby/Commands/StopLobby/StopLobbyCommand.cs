using Lunia.V4.Application.Common;

namespace Lunia.V4.Application.Lobby.Commands.StopLobby;

public sealed record StopLobbyCommand(string ServerName) : ICommand;