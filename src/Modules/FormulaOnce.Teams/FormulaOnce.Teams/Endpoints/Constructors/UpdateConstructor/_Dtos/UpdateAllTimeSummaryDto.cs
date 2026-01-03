namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

public record UpdateAllTimeSummaryDto(
    int GrandPrixEntered,
    int TeamPoints,
    string HighestRaceFinish,
    int Podiums,
    string HighestGridPosition,
    int PolePositions,
    int WorldChampionships);