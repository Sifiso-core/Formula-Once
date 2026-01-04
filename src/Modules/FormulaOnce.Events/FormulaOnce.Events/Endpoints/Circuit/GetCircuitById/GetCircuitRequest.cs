namespace FormulaOnce.Events.Endpoints.Circuit.GetCircuitById;

public record GetCircuitRequest
{
    public Guid Id { get; init; }
}