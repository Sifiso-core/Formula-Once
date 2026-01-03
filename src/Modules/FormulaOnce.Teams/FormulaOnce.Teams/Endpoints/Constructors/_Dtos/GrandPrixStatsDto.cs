namespace FormulaOnce.Teams.Endpoints.Constructors._Dtos;

public record GrandPrixStatsDto(
    int GrandPrixRaces,
    int GrandPrixPoints,
    int GrandPrixWins,
    int GrandPrixPodiums,
    int GrandPrixPoles,
    int GrandPrixTop10s);