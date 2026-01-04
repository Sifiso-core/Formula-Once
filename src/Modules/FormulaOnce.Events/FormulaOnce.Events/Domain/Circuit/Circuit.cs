using Ardalis.GuardClauses;

namespace FormulaOnce.Events.Domain.Circuit;

public class Circuit
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Location Location { get; private set; } = null!;
    public double LengthKm { get; private set; }
    public CircuitRecord? LapRecord { get; private set; }

    // The landmarks shown in your image
    private readonly List<TrackLandmark> _landmarks = new();
    public IReadOnlyCollection<TrackLandmark> Landmarks => _landmarks.AsReadOnly();

    private Circuit()
    {
    }

    public static class Factory
    {
        public static Circuit Create(string name, Location location, double length)
        {
            return new Circuit
            {
                Id = Guid.NewGuid(),
                Name = Guard.Against.NullOrEmpty(name, nameof(name)),
                Location = location,
                LengthKm = length
            };
        }
    }

    public void AddLandmark(string label, LandmarkType type, int nearTurn)
    {
        // Business logic: Don't allow duplicate specific zones if necessary
        _landmarks.Add(new TrackLandmark(label, type, nearTurn));
    }

    public void UpdateLapRecord(TimeSpan time, string driverName, int year)
    {
        LapRecord = new CircuitRecord(time, driverName, year);
    }
}