using FastEndpoints;
using FormulaOnce.Teams.Services;


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
        Get("/drivers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var drivers = await _driverService.GetAllAsync(ct);

        var response = new GetAllDriversResponse()
        {
            Drivers = drivers
        };

        await Send.OkAsync(response, ct);
    }
}