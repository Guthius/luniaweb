using Lunia.V4.Application.Accounts.Commands.Auth;
using Lunia.V4.Application.Characters.Commands.GetCharacterList;
using Lunia.V4.Application.Lobby.Commands.StartLobby;
using Lunia.V4.Application.Lobby.Commands.StopLobby;
using Lunia.V4.Helpers;
using MediatR;

namespace Lunia.V4.Endpoints;

internal static class Lobby
{
    public const string RoutePrefix = "/v4/lobby";

    public static void MapLobbyEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup(RoutePrefix);

        group.MapGet("Auth.asp", async (Request request, ISender sender) =>
        {
            if (request.Parameters.Count < 2)
            {
                return Response.Error("not enough parameter");
            }

            var result = await sender.Send(
                new AuthCommand(
                    ServerName: request.ServerName,
                    AccountName: request.GetString(0),
                    Password: request.GetString(1)));

            return result.Match(value => Response.Ok(value.AccountName), Response.Error);
        });

        group.MapGet("ListCharacter.asp", async (Request request, ISender sender) =>
        {
            if (request.Parameters.Count < 1)
            {
                return Response.Error("not enough parameter");
            }

            var result = await sender.Send(
                new GetCharacterListCommand(
                    AccountName: request.GetString(1)));

            return result.Match(
                value =>
                {
                    var response = new ResponseBuilder();

                    response.Write(value.SlotCount);
                    response.Write(value.AccountName);
                    response.Write(field =>
                    {
                        foreach (var classNumber in value.ClassNumbers)
                        {
                            field.Write(classNumber);
                        }
                    });

                    foreach (var character in value.Characters)
                    {
                        response.Write(character.CharacterName);
                        response.Write(character.VirtualIdCode);
                        response.Write(character.ClassNumber);
                        response.Write(character.StageLevel);
                        response.Write(character.StageExp);
                        response.Write(character.PvpLevel);
                        response.Write(character.PvpExp);
                        response.Write(character.WarLevel);
                        response.Write(character.WarExp);
                        response.WriteDateTime(character.LastLoggedAt);
                        response.Write(character.InstantStateFlags);
                        response.Write(character.RebirthCount);
                        response.Write(character.StoredLevel);

                        response.Write(field =>
                        {
                            var licenses = value.Licenses.Where(x => x.CharacterName.Equals(character.CharacterName));
                            foreach (var license in licenses)
                            {
                                field.Write(license.StageGroupHash);
                                field.Write(license.AccessLevel);
                            }
                        });

                        response.Write(field =>
                        {
                            var items = value.Items.Where(x => x.CharacterName.Equals(character.CharacterName));
                            foreach (var item in items)
                            {
                                field.Write(item.BagNumber);
                                field.Write(item.PositionNumber);
                                field.Write(item.ItemHash);
                                field.Write(item.Instance);
                            }
                        });
                    }

                    return response.ToResponse();
                },
                Response.Error);
        });

        group.MapGet("Start.asp", async (Request request, ISender sender) =>
        {
            if (!request.TryGetInt32(1, out var port) ||
                !request.TryGetInt32(2, out var capacity))
            {
                return Response.Error("invalid parameter");
            }

            var result = await sender.Send(
                new StartLobbyCommand(
                    ServerName: request.ServerName,
                    Address: request.GetString(0, "(any)"),
                    Port: port,
                    Capacity: capacity));

            return result.Match(Response.Ok, Response.Error);
        });

        group.MapGet("Stop.asp", async (Request request, ISender sender) =>
        {
            var result = await sender.Send(
                new StopLobbyCommand(
                    ServerName: request.GetString(0, request.ServerName)));

            return result.Match(Response.Ok, Response.Error);
        });
    }
}