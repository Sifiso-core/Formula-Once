using Ardalis.GuardClauses;

namespace FormulaOnce.Teams.Domain.Constructor;

public class SeasonStats
{
    private SeasonStats()
    {
    }

    public SeasonStats(int seasonPosition, int seasonPoints, GrandPrixStats grandPrixStats, SprintStats sprintStats,
        int dhlFastestLaps,
        int dnfs)
    {
        SeasonPosition = Guard.Against.NegativeOrZero(seasonPosition);
        SeasonPoints = Guard.Against.Negative(seasonPoints);
        GrandPrixStats = Guard.Against.Null(grandPrixStats);
        SprintStats = Guard.Against.Null(sprintStats);
        DhlFastestLaps = Guard.Against.Negative(dhlFastestLaps);
        DNFs = Guard.Against.Negative(dnfs);
    }

    public int SeasonPosition { get; private set; }
    public int SeasonPoints { get; private set; }
    public GrandPrixStats GrandPrixStats { get; private set; }
    public SprintStats SprintStats { get; private set; }
    public int DhlFastestLaps { get; private set; }
    public int DNFs { get; private set; }
}