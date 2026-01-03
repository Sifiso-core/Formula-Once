using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Teams.Services.DriverServices;

namespace FormulaOnce.Teams.Endpoints.Drivers.DeleteDriver;

internal class DeleteDriver : Endpoint<DeleteDriverRequest>
{
    private readonly IDriverService _driverService;

    public DeleteDriver(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public override void Configure()
    {
        Delete("/drivers/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteDriverRequest req, CancellationToken ct)
    {
        var result = await _driverService.DeleteDriverAsync(req.Id, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}