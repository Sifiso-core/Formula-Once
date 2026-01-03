using Ardalis.GuardClauses;

namespace FormulaOnce.Teams.Domain.Constructor;

public record GrandPrixStats
{
    private GrandPrixStats()
    {
    }

    public GrandPrixStats(int grandPrixRaces, int grandPrixPoints, int grandPrixWins, int grandPrixPodiums,
        int grandPrixPoles, int grandPrixTop10S)
    {
        GrandPrixRaces = Guard.Against.Negative(grandPrixRaces);
        GrandPrixPoints = Guard.Against.Negative(grandPrixPoints);
        GrandPrixWins = Guard.Against.Negative(grandPrixWins);
        GrandPrixPodiums = Guard.Against.Negative(grandPrixPodiums);
        GrandPrixPoles = Guard.Against.Negative(grandPrixPoles);
        GrandPrixTop10s = Guard.Against.Negative(grandPrixTop10S);
    }

    public int GrandPrixRaces { get; private set; }
    public int GrandPrixPoints { get; private set; }
    public int GrandPrixWins { get; private set; }
    public int GrandPrixPodiums { get; private set; }
    public int GrandPrixPoles { get; private set; }
    public int GrandPrixTop10s { get; private set; }
}