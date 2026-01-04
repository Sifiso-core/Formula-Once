using FastEndpoints;
using FormulaOnce.Events.Services.CircuitService;

namespace FormulaOnce.Events.Endpoints.Circuit.GetAllCircuits;

public class GetCircuitsEndpoint : EndpointWithoutRequest<GetAllCircuitsResponse>
{
    private readonly ICircuitService _circuitService;

    public GetCircuitsEndpoint(ICircuitService circuitService)
    {
        _circuitService = circuitService;
    }

    public override void Configure()
    {
        Get("/events/circuits");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _circuitService.GetAllAsync(ct);

        var response = new GetAllCircuitsResponse()
        {
            Circuits = result.Value
        };

        await Send.OkAsync(response, ct);
    }
}