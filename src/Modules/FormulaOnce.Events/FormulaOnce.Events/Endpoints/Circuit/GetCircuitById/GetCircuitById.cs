using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Events.Services.CircuitService;
using FormulaOnce.Events.Services.CircuitService.Dto;

namespace FormulaOnce.Events.Endpoints.Circuit.GetCircuitById;

public class GetCircuitById : Endpoint<GetCircuitRequest, CircuitDto>
{
    private readonly ICircuitService _circuitService;

    public GetCircuitById(ICircuitService circuitService)
    {
        _circuitService = circuitService;
    }

    public override void Configure()
    {
        Get("/events/circuits/{Id:guid}");
        Claims(ClaimTypes.NameIdentifier);
    }

    public override async Task HandleAsync(GetCircuitRequest req, CancellationToken ct)
    {
        var result = await _circuitService.GetByIdAsync(req.Id, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(result.Value, ct);
    }
}