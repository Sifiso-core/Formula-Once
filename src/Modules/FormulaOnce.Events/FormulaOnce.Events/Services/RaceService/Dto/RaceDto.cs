namespace FormulaOnce.Events.Services.RaceService.Dto;

public record RaceDto(
    Guid Id,
    string Title,
    int Season,
    int Round,
    Guid CircuitId,
    int NumberOfLaps,
    double RaceDistanceKm,
    List<SessionDto> Sessions);