namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

public record UpdateGrandPrixStatsDto(
    int GrandPrixRaces,
    int GrandPrixPoints,
    int GrandPrixWins,
    int GrandPrixPodiums,
    int GrandPrixPoles,
    int GrandPrixTop10s);