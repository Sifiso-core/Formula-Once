using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Teams.Mappings;
using FormulaOnce.Teams.Services.DriverServices;
using FormulaOnce.Teams.Services.DriverServices.Dto;
using Microsoft.AspNetCore.Http;

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
        Post("/teams/drivers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateDriverRequest req, CancellationToken ct)
    {
        var result = await _driverService.AddDriverAsync(req.ToDto(), ct);

        if (result.IsSuccess)
        {
            await Send.CreatedAtAsync<GetDriverById.GetDriverById>(
                new { result.Value.Id },
                result.Value,
                cancellation: ct);
            return;
        }

        if (result.Status == ResultStatus.Invalid)
        {
            foreach (var error in result.ValidationErrors) AddError(error.ErrorMessage);
            await Send.ErrorsAsync(StatusCodes.Status400BadRequest, ct);
            return;
        }

        if (result.Status == ResultStatus.NotFound) await Send.NotFoundAsync(ct);
    }
}