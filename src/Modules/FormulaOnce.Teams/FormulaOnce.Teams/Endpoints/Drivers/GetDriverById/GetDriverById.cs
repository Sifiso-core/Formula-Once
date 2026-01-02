using FastEndpoints;
using FormulaOnce.Teams.Services;

namespace FormulaOnce.Teams.Endpoints.Drivers.GetDriverById;

internal class GetDriverById : Endpoint<GetDriverByIdRequest>
{
    private readonly IDriverService _driverService;

    public GetDriverById(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public override void Configure()
    {
        Get("/drivers/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDriverByIdRequest req, CancellationToken ct)
    {
        var driver = await _driverService.GetByIdAsync(req.Id, ct);
        if (driver is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(driver, ct);
    }
}