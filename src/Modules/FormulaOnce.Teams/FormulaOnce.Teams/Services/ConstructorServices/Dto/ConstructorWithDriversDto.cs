using FormulaOnce.Teams.Services.DriverServices.Dto;

namespace FormulaOnce.Teams.Services.ConstructorServices.Dto;

public record ConstructorWithDriversDto(
    Guid Id,
    string Name,
    string BaseLocation,
    int EstablishedYear,
    List<DriverDto> Drivers,
    ConstructorStatsDto Stats);