namespace FormulaOnce.Teams.Endpoints.Constructors._Dtos;

public record AllTimeSummaryDto(
    int GrandPrixEntered,
    int TeamPoints,
    string HighestRaceFinish,
    int Podiums,
    string HighestGridPosition,
    int PolePositions,
    int WorldChampionships);