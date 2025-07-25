namespace Lunia.V4.Domain.Entities;

public sealed record LobbyConnection
{
    public string ServerName { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
}