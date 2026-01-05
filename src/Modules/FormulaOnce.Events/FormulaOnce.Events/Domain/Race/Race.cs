using Ardalis.Result;

namespace FormulaOnce.Events.Domain.Race;

public class Race
{
    private readonly List<Session> _sessions = new();

    private Race()
    {
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public int Season { get; private set; }
    public int Round { get; private set; }
    public Guid CircuitId { get; private set; }
    public Circuit.Circuit Circuit { get; set; }
    public int NumberOfLaps { get; private set; }
    public double RaceDistanceKm { get; private set; }
    public IReadOnlyCollection<Session> Sessions => _sessions.AsReadOnly();

    public Result ScheduleSession(SessionType type, DateTime startTime)
    {
        if (_sessions.Any(s => s.SessionType == type))
            return Result.Conflict($"Session {type} is already scheduled for this race.");

        _sessions.Add(new Session(Guid.NewGuid(), Id, startTime, type));
        return Result.Success();
    }

    public static class Factory
    {
        public static Race Create(string title, int season, int round, Circuit.Circuit circuit, int laps)
        {
            // F1 races (except Monaco) must exceed 305km. 
            var totalDistance = Math.Round(laps * circuit.LengthKm, 3);

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