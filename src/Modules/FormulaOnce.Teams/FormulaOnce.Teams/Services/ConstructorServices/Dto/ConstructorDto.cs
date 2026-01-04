namespace FormulaOnce.Teams.Services.ConstructorServices.Dto;

public record ConstructorDto(
    Guid Id,
    string Name,
    string BaseLocation,
    int EstablishedYear,
    ConstructorStatsDto Stats);