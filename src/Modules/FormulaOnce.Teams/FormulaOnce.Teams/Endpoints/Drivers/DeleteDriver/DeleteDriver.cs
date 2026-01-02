using FastEndpoints;
using FormulaOnce.Teams.Services;

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
        var driver = await _driverService.GetByIdAsync(req.Id, ct);

        if (driver is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await _driverService.DeleteDriverAsync(req.Id, ct);

        await _driverService.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}