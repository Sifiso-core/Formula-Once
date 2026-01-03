using FormulaOnce.Teams.Endpoints.Drivers._Dtos;

namespace FormulaOnce.Teams.Endpoints.Drivers.GetAllDrivers;

internal class GetAllDriversResponse
{
    public required List<DriverDto> Drivers { get; set; } = [];
}