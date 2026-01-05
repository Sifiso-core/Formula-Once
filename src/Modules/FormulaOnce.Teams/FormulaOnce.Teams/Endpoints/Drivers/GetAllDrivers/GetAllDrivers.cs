using System.Security.Claims;
using FastEndpoints;
using FormulaOnce.Teams.Services.DriverServices;

namespace FormulaOnce.Teams.Endpoints.Drivers.GetAllDrivers;

internal class GetAllDrivers : EndpointWithoutRequest<GetAllDriversResponse>
{
    private readonly IDriverService _driverService;

    public GetAllDrivers(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public override void Configure()
    {
        Get("/teams/drivers");
        Claims(ClaimTypes.NameIdentifier);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _driverService.GetAllAsync(ct);

        var response = new GetAllDriversResponse
        {
            Drivers = result.Value // result.Value is the List<DriverDto>
        };

        await Send.OkAsync(response, ct);
    }
}