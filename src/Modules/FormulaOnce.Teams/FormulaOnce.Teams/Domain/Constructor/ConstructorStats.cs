using Ardalis.GuardClauses;

namespace FormulaOnce.Teams.Domain.Constructor;

public class ConstructorStats
{
    // EF Core needs this
    private ConstructorStats()
    {
    }

    public ConstructorStats(AllTimeSummary allTimeSummary, SeasonStats seasonStats)
    {
        AllTimeSummary = Guard.Against.Default(allTimeSummary);
        SeasonStats = Guard.Against.Default(seasonStats);
    }

    public AllTimeSummary AllTimeSummary { get; set; } = null!;
    public SeasonStats SeasonStats { get; set; } = null!;
}