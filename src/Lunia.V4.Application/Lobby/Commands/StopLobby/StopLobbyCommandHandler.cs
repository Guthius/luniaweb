using LanguageExt;
using LanguageExt.Common;
using Lunia.V4.Application.Common;
using Lunia.V4.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lunia.V4.Application.Lobby.Commands.StopLobby;

internal sealed class StopLobbyCommandHandler(ILogger<StopLobbyCommandHandler> logger, LobbyDbContext context) : ICommandHandler<StopLobbyCommand>
{
    public async Task<Option<Error>> Handle(StopLobbyCommand request, CancellationToken cancellationToken)
    {
        await context.SelectedCharacters
            .Where(character => context.LobbyConnections
                .Where(connection =>
                    connection.ServerName == request.ServerName)
                .Select(connection =>
                    connection.AccountName)
                .Contains(character.AccountName))
            .ExecuteDeleteAsync(cancellationToken);

        await context.LobbyConnections
            .Where(connection =>
                connection.ServerName == request.ServerName)
            .ExecuteDeleteAsync(cancellationToken);

        await context.LobbyServers
            .Where(server =>
                server.ServerName == request.ServerName)
            .ExecuteDeleteAsync(cancellationToken);
        
        // TODO: Set all family members offline if there are no connections left...

        logger.LogInformation("Lobby {ServerName} has stopped", request.ServerName);

        return Option<Error>.None;
    }
}