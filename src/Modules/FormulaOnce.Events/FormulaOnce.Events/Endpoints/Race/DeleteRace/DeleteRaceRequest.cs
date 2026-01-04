namespace FormulaOnce.Events.Endpoints.Race.DeleteRace;

public record DeleteRaceRequest
{
    public Guid Id { get; init; }
}