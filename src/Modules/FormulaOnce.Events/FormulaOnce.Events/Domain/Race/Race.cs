namespace FormulaOnce.Events.Domain.Race;

public class Race
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public int Season { get; private set; }
    public int Round { get; private set; }
    public Guid CircuitId { get; private set; }
    public int NumberOfLaps { get; private set; }
    public double RaceDistanceKm { get; private set; }

    private readonly List<Session> _sessions = new();
    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    private Race()
    {
    }

    public static class Factory
    {
        public static Race Create(string title, int season, int round, Circuit.Circuit circuit, int laps)
        {
            // F1 races (except Monaco) must exceed 305km. 
            // We can bake this "Sporting Regulation" into the factory.
            double totalDistance = Math.Round(laps * circuit.LengthKm, 3);

            return new Race
            {
                Id = Guid.NewGuid(),
                Title = title,
                Season = season,
                Round = round,
                CircuitId = circuit.Id,
                NumberOfLaps = laps,
                RaceDistanceKm = totalDistance
            };
        }
    }
}