using FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor;

public record UpdateConstructorRequest
{
    public Guid Id { get; init; }

    // Core Team Info
    public string Name { get; init; } = string.Empty;
    public string BaseLocation { get; init; } = string.Empty;
    public int EstablishedYear { get; init; }

    // Statistics snapshot from the UI
    public UpdateStatsDto Stats { get; init; } = null!;
}