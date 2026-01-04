using FormulaOnce.Teams.Services.DriverServices.Dto;

namespace FormulaOnce.Teams.Endpoints.Drivers.GetAllDrivers;

internal class GetAllDriversResponse
{
    public required List<DriverDto> Drivers { get; set; } = [];
}