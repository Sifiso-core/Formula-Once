namespace FormulaOnce.Teams.Endpoints.Constructors._Dtos;

public record ConstructorDto(
    Guid Id,
    string Name,
    string BaseLocation,
    int EstablishedYear,
    ConstructorStatsDto Stats);