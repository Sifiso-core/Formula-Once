namespace FormulaOnce.Teams.Services.ConstructorServices.Dto;

public record SeasonStatsDto(
    int SeasonPosition,
    int SeasonPoints,
    GrandPrixStatsDto GrandPrixStats,
    SprintStatsDto SprintStats,
    int DhlFastestLaps,
    int DNFs);