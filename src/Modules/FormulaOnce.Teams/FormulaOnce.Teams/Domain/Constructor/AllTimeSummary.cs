using Ardalis.GuardClauses;

namespace FormulaOnce.Teams.Domain.Constructor;

public record AllTimeSummary
{
    private AllTimeSummary()
    {
    }

    public AllTimeSummary(int grandPrixEntered, int teamPoints, string highestRaceFinish, int podiums,
        string highestGridPosition, int polePositions, int worldChampionships)
    {
        GrandPrixEntered = Guard.Against.Negative(grandPrixEntered);
        TeamPoints = Guard.Against.Negative(teamPoints);
        HighestRaceFinish = Guard.Against.NullOrEmpty(highestRaceFinish);
        Podiums = Guard.Against.Negative(podiums);
        HighestGridPosition = Guard.Against.NullOrEmpty(highestGridPosition);
        PolePositions = Guard.Against.Negative(polePositions);
        WorldChampionships = Guard.Against.Negative(worldChampionships);
    }

    public int GrandPrixEntered { get; private set; }
    public int TeamPoints { get; private set; }
    public string HighestRaceFinish { get; private set; } // "1 (x249)"
    public int Podiums { get; set; }
    public string HighestGridPosition { get; private set; } // "1 (x254)"
    public int PolePositions { get; private set; }
    public int WorldChampionships { get; private set; }
}