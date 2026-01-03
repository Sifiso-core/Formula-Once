namespace FormulaOnce.Teams.Endpoints.Constructors._Dtos;

public record SprintStatsDto(
    int SprintRaces,
    int SprintPoints,
    int SprintWins,
    int SprintPodiums,
    int SprintPoles,
    int SprintTop10s);