namespace FormulaOnce.Teams.Services.ConstructorServices.Dto;

public record GrandPrixStatsDto(
    int GrandPrixRaces,
    int GrandPrixPoints,
    int GrandPrixWins,
    int GrandPrixPodiums,
    int GrandPrixPoles,
    int GrandPrixTop10s);