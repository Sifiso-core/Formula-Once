using FastEndpoints;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;
using FormulaOnce.Events.Services.CircuitService;
using FormulaOnce.Events.Services.CircuitService.Dto;

namespace FormulaOnce.Events.Endpoints.Circuit.CreateCircuit;

public class CreateCircuitEndpoint : Endpoint<CreateCircuitRequest, CircuitDto>
{
    private readonly ICircuitService _circuitService;

    public CreateCircuitEndpoint(ICircuitService circuitService)
    {
        _circuitService = circuitService;
    }

    public override void Configure()
    {
        Post("/events/circuits");
        Policies("AdminOnly");
        Summary(s =>
        {
            s.Summary = "Create a new race circuit";
            s.Description = "Registers a circuit with its geographic location and landmarks (DRS zones, etc.)";
        });
    }

    public override async Task HandleAsync(CreateCircuitRequest req, CancellationToken ct)
    {
        var result = await _circuitService.CreateAsync(req, ct);

        if (result.IsSuccess)
        {
            await Send.CreatedAtAsync<GetCircuitById.GetCircuitById>(
                new { result.Value.Id },
                result.Value,
                generateAbsoluteUrl: true,
                cancellation: ct);
            return;
        }

        foreach (var error in result.Errors) AddError(error);

        await Send.ErrorsAsync(cancellation: ct);
    }
}