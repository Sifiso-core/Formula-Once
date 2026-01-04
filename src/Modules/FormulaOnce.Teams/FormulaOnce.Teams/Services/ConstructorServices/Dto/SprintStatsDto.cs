namespace FormulaOnce.Teams.Services.ConstructorServices.Dto;

public record SprintStatsDto(
    int SprintRaces,
    int SprintPoints,
    int SprintWins,
    int SprintPodiums,
    int SprintPoles,
    int SprintTop10s);