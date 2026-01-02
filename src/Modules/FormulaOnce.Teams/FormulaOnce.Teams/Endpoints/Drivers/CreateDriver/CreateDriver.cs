using FastEndpoints;
using FormulaOnce.Teams.Domain;
using FormulaOnce.Teams.Services;

namespace FormulaOnce.Teams.Endpoints.Drivers.CreateDriver;

internal class CreateDriver : Endpoint<CreateDriverRequest, DriverDto>
{
    private readonly IDriverService _driverService;

    public CreateDriver(IDriverService driverService)
    {
        _driverService = driverService;
    }

    public override void Configure()
    {
        Post("/drivers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateDriverRequest req, CancellationToken ct)
    {
        var driverDto = new DriverDto()
        {
            Acronym = req.Acronym,
            RacingNumber = req.RacingNumber,
            ConstructorId = req.ConstructorId,
            Nationality = req.Nationality,
            FullName = req.FullName,
            DateOfBirth = req.DateOfBirth,
            Id = Guid.NewGuid(),
            CareerStats = new Stats(0, 0, 0, 0, 0),
        };

        await _driverService.AddDriverAsync(driverDto, ct);

        await _driverService.SaveChangesAsync(ct);

        await Send.CreatedAtAsync<GetDriverById.GetDriverById>(new { Id = driverDto.Id }, driverDto, cancellation: ct);
    }
}