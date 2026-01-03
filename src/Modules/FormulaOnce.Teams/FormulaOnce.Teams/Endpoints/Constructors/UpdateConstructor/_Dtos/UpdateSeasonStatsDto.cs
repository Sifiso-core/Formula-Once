namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

public record UpdateSeasonStatsDto(
    int SeasonPosition,
    int SeasonPoints,
    UpdateGrandPrixStatsDto GrandPrixStats,
    UpdateSprintStatsDto SprintStats,
    int DhlFastestLaps,
    int DNFs);