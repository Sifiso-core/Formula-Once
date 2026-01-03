using Ardalis.GuardClauses;

namespace FormulaOnce.Teams.Domain.Constructor;

public record SprintStats
{
    private SprintStats()
    {
    }

    public SprintStats(int sprintRaces, int sprintPoints, int sprintWins, int sprintPodiums, int sprintPoles,
        int sprintTop10S)
    {
        SprintRaces = Guard.Against.Negative(sprintRaces);
        SprintPoints = Guard.Against.Negative(sprintPoints);
        SprintWins = Guard.Against.Negative(sprintWins);
        SprintPodiums = Guard.Against.Negative(sprintPodiums);
        SprintPoles = Guard.Against.Negative(sprintPoles);
        SprintTop10s = Guard.Against.Negative(sprintTop10S);
    }

    public int SprintRaces { get; private set; }
    public int SprintPoints { get; private set; }
    public int SprintWins { get; private set; }
    public int SprintPodiums { get; private set; }
    public int SprintPoles { get; private set; }
    public int SprintTop10s { get; private set; }
}