using Lunia.V4.Application.Common;

namespace Lunia.V4.Application.Accounts.Commands.Auth;

public sealed record AuthCommand(string ServerName, string AccountName, string Password) : ICommand<AuthResult>;