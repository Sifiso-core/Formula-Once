using FormulaOnce.Teams.Endpoints.Drivers._Dtos;

namespace FormulaOnce.Teams.Endpoints.Constructors._Dtos;

public record ConstructorWithDriversDto(
    Guid Id,
    string Name,
    string BaseLocation,
    int EstablishedYear,
    List<DriverDto> Drivers,
    ConstructorStatsDto Stats);