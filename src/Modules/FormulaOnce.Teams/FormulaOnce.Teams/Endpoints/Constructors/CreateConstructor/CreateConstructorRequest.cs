namespace FormulaOnce.Teams.Endpoints.Constructors.CreateConstructor;

public record CreateConstructorRequest
{
    public string Name { get; init; } = string.Empty;
    public string BaseLocation { get; init; } = string.Empty;
    public int EstablishedYear { get; init; }
    public CreateStatsDto Stats { get; init; } = null!;
}