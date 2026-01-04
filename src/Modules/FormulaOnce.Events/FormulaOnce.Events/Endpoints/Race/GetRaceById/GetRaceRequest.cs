namespace FormulaOnce.Events.Endpoints.Race.GetRaceById;

public record GetRaceRequest
{
    public Guid Id { get; init; }
}