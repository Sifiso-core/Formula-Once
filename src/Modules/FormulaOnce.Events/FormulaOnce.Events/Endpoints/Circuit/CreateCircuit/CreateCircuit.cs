using FastEndpoints;
using FluentValidation;
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
        AllowAnonymous();
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

public class CreateCircuitValidator : Validator<CreateCircuitRequest>
{
    public CreateCircuitValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Circuit name is required")
            .MaximumLength(100);

        RuleFor(x => x.LengthKm)
            .GreaterThan(0).WithMessage("Track length must be a positive value")
            .LessThan(15).WithMessage("Track length seems unrealistic (exceeds 15km)");

        RuleFor(x => x.Location.Country).NotEmpty();
        RuleFor(x => x.Location.City).NotEmpty();

        RuleFor(x => x.Location.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90");

        RuleFor(x => x.Location.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180");

        RuleForEach(x => x.Landmarks).ChildRules(landmark =>
        {
            landmark.RuleFor(l => l.Label).NotEmpty();

            landmark.RuleFor(l => l.NearTurn).GreaterThanOrEqualTo(0);
        });
    }
}