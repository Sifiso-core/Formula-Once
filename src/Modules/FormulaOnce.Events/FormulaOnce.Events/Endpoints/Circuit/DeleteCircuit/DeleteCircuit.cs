using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Events.Services.CircuitService;

namespace FormulaOnce.Events.Endpoints.Circuit.DeleteCircuit;

public class DeleteCircuitEndpoint : Endpoint<DeleteCircuitRequest>
{
    private readonly ICircuitService _circuitService;

    public DeleteCircuitEndpoint(ICircuitService circuitService)
    {
        _circuitService = circuitService;
    }

    public override void Configure()
    {
        Delete("/events/circuits/{Id}");
        Policies("AdminOnly");
        Summary(s =>
        {
            s.Summary = "Delete a circuit";
            s.Description = "Removes a circuit and its associated landmarks. Will fail if races are linked.";
        });
    }

    public override async Task HandleAsync(DeleteCircuitRequest req, CancellationToken ct)
    {
        var result = await _circuitService.DeleteAsync(req.Id, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}

public record DeleteCircuitRequest
{
    public Guid Id { get; init; }
}