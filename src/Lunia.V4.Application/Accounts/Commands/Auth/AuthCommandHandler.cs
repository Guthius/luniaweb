using LanguageExt;
using LanguageExt.Common;
using Lunia.V4.Application.Common;
using Lunia.V4.Domain.Entities;
using Lunia.V4.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lunia.V4.Application.Accounts.Commands.Auth;

internal sealed class AuthCommandHandler(LobbyDbContext context, TimeProvider timeProvider) : ICommandHandler<AuthCommand, AuthResult>
{
    private static class Errors
    {
        public static readonly Error AccountNotExist = Error.New(1, "Lobby::FromServer::Auth::AccountNotExist");
        public static readonly Error PasswordMismatch = Error.New(2, "Lobby::FromServer::Auth::PasswordMismatch");
        public static readonly Error BlockedAccount = Error.New(4, "Lobby::FromServer::Auth::BlockedAccount");
        public static readonly Error AlreadyConnectedAtLobby = Error.New(5, "Lobby::FromServer::Auth::AlreadyConnectedAtLobby");
    }

    public async Task<Either<Error, AuthResult>> Handle(AuthCommand command, CancellationToken cancellationToken)
    {
        /* Check to make sure the account is not blocked. */
        var blocked = await IsAccountBlocked(command.AccountName, cancellationToken);
        if (blocked)
        {
            return Errors.BlockedAccount;
        }

        var account = await context.Accounts
            .Where(x => x.AccountName == command.AccountName)
            .FirstOrDefaultAsync(cancellationToken);

        /* Check if the account exists and the password is valid. */
        if (account is null) return Errors.AccountNotExist;
        if (!account.PasswordHash.Equals(command.Password, StringComparison.OrdinalIgnoreCase))
        {
            return Errors.PasswordMismatch;
        }

        /* Check to make sure the account is not already connected to a lobby. */
        var connected = await context.LobbyConnections
            .AnyAsync(connection => connection.AccountName == command.AccountName,
                cancellationToken);

        if (connected)
        {
            return Errors.AlreadyConnectedAtLobby;
        }

        /* Clear the last selected character of the account. */
        await context.SelectedCharacters
            .Where(selectedCharacter =>
                selectedCharacter.AccountName == command.AccountName)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);

        /* Register the lobby connection. */
        context.LobbyConnections.Add(new LobbyConnection
        {
            ServerName = command.ServerName,
            AccountName = account.AccountName,
        });

        /* Update the last connected date of the account. */
        account.LastLoggedAt = DateTimeOffset.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return new AuthResult(account.AccountName);
    }

    private async Task<bool> IsAccountBlocked(string accountName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(accountName))
        {
            return false;
        }

        var asOf = timeProvider.GetUtcNow();

        return await context.Blocking
            .Where(block =>
                block.ExpiresAt == null || block.ExpiresAt > asOf)
            .AnyAsync(block =>
                    block.AccountName == accountName,
                cancellationToken);
    }
}