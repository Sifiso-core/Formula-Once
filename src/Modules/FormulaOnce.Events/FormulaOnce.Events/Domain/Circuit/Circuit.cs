using Ardalis.GuardClauses;

namespace FormulaOnce.Events.Domain.Circuit;

public class Circuit
{
    private readonly List<TrackLandmark> _landmarks = new();

    private Circuit()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Location Location { get; private set; } = null!;
    public double LengthKm { get; private set; }
    public CircuitRecord? LapRecord { get; private set; }
    public IReadOnlyCollection<TrackLandmark> Landmarks => _landmarks.AsReadOnly();

    public void AddLandmark(string label, LandmarkType type, int nearTurn)
    {
        _landmarks.Add(new TrackLandmark(label, type, nearTurn));
    }

    public void UpdateLapRecord(TimeSpan time, string driverName, int year)
    {
        LapRecord = new CircuitRecord(time, driverName, year);
    }

    public static class Factory
    {
        public static Circuit Create(string name, Location location, double length)
        {
            return new Circuit
            {
                Id = Guid.NewGuid(),
                Name = Guard.Against.NullOrEmpty(name),
                Location = location,
                LengthKm = length
            };
        }
    }
}