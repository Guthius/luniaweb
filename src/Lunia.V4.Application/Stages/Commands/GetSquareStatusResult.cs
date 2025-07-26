using Lunia.V4.Application.Stages.Dtos;

namespace Lunia.V4.Application.Stages.Commands;

public sealed record GetSquareStatusResult(List<SquareStatusDto> Squares);