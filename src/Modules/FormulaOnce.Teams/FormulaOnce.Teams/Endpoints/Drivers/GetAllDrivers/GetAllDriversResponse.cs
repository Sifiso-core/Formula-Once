using FormulaOnce.Teams.Domain;

namespace FormulaOnce.Teams.Endpoints.Drivers.GetAllDrivers;

internal class GetAllDriversResponse
{
    public List<DriverDto> Drivers { get; set; } = [];
}