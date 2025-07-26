namespace Lunia.V4.Domain.Entities;

public sealed record LobbyServer
{
    public string ServerName { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty; // TODO: Rename to Address
    public int Port { get; set; } = 9060;
}