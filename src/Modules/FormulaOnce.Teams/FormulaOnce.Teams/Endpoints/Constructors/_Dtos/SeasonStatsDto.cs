namespace FormulaOnce.Teams.Endpoints.Constructors._Dtos;

public record SeasonStatsDto(
    int SeasonPosition,
    int SeasonPoints,
    GrandPrixStatsDto GrandPrixStats,
    SprintStatsDto SprintStats,
    int DhlFastestLaps,
    int DNFs);