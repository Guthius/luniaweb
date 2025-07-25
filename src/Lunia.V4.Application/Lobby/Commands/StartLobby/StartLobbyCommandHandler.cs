using LanguageExt;
using LanguageExt.Common;
using Lunia.V4.Application.Common;
using Lunia.V4.Domain.Entities;
using Lunia.V4.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lunia.V4.Application.Lobby.Commands.StartLobby;

internal sealed class StartLobbyCommandHandler(ILogger<StartLobbyCommandHandler> logger, LobbyDbContext context) : ICommandHandler<StartLobbyCommand>
{
    public async Task<Option<Error>> Handle(StartLobbyCommand request, CancellationToken cancellationToken)
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

        context.LobbyServers.Add(new LobbyServer
        {
            ServerName = request.ServerName
        });

        await context.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Lobby {ServerName} has started", request.ServerName);

        return Option<Error>.None;
    }
}