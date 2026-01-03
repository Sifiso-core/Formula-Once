namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

public record UpdateSprintStatsDto(
    int SprintRaces,
    int SprintPoints,
    int SprintWins,
    int SprintPodiums,
    int SprintPoles,
    int SprintTop10s);