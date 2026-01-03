using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Teams.Mappings;
using FormulaOnce.Teams.Services.DriverServices;

namespace FormulaOnce.Teams.Endpoints.Drivers.UpdateDriver;

internal class UpdateDriver : Endpoint<UpdateDriverRequest>
{
    private readonly IDriverService _driverService;

    public UpdateDriver(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public override void Configure()
    {
        Put("/drivers/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateDriverRequest req, CancellationToken ct)
    {
        var result = await _driverService.UpdateDriverAsync(req.ToDto(), ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}