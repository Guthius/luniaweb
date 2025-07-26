namespace Lunia.V4.Application.Stages.Dtos;

public sealed record SquareStatusDto(
    string SquareName, 
    int ConnectionCount, 
    int Capacity, 
    int StageGroupHash, 
    int AccessLevel, 
    int OrderNumber);