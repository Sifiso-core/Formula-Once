namespace FormulaOnce.Events.Services.CircuitService.Dto;

public record CircuitDto(
    Guid Id,
    string Name,
    double LengthKm,
    LocationDto Location,
    CircuitRecordDto? LapRecord,
    List<LandmarkDto> Landmarks);