using Lunia.V4.Application.Common;

namespace Lunia.V4.Application.Lobby.Commands.StartLobby;

public sealed record StartLobbyCommand(string ServerName, string Address, int Port, int Capacity) : ICommand;