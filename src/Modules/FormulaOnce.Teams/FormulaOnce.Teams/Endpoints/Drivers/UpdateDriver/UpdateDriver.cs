using FastEndpoints;
using FormulaOnce.Teams.Domain;
using FormulaOnce.Teams.Services;

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
        var driverToUpdate = await _driverService.GetByIdAsync(req.Id, ct);

        if (driverToUpdate is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var driverDto = new DriverDto()
        {
            Id = req.Id,
            Acronym = req.Acronym,
            RacingNumber = req.RacingNumber,
            Nationality = req.Nationality,
            ConstructorId = req.ConstructorId,
            FullName = req.FullName
        };
        await _driverService.UpdateDriverAsync(driverDto, ct);

        await _driverService.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}