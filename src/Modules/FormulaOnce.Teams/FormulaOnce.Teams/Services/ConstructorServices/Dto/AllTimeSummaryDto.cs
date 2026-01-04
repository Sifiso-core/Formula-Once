namespace FormulaOnce.Teams.Services.ConstructorServices.Dto;

public record AllTimeSummaryDto(
    int GrandPrixEntered,
    int TeamPoints,
    string HighestRaceFinish,
    int Podiums,
    string HighestGridPosition,
    int PolePositions,
    int WorldChampionships);