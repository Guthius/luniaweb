using LanguageExt;
using LanguageExt.Common;
using Lunia.V4.Application.Common;
using Lunia.V4.Application.Stages.Dtos;
using Lunia.V4.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lunia.V4.Application.Stages.Commands;

internal sealed class GetSquareStatusCommandHandler(ILogger<GetSquareStatusCommandHandler> logger, StageDbContext context) : ICommandHandler<GetSquareStatusCommand, GetSquareStatusResult>
{
    public async Task<Either<Error, GetSquareStatusResult>> Handle(GetSquareStatusCommand request, CancellationToken cancellationToken)
    {
        var squareStatusDtos = await context.SquareStages
            .Join(context.StageServers,
                square => square.ServerName,
                server => server.ServerName,
                (square, server) => square)
            .OrderBy(square => square.OrderNumber)
            .ThenBy(square => square.SquareName)
            .Select(square => new SquareStatusDto(
                square.SquareName,
                square.ConnectionCount,
                square.Capacity,
                square.StageGroupHash,
                square.AccessLevel,
                square.OrderNumber))
            .ToListAsync(cancellationToken);

        logger.LogInformation("Retrieved status of {SquareCount} squares", squareStatusDtos.Count);

        return new GetSquareStatusResult(squareStatusDtos);
    }
}