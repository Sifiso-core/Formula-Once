namespace FormulaOnce.Events.Domain.Circuit;

public record CircuitRecord
{
    public TimeSpan Time { get; private set; }
    public string DriverName { get; private set; }
    public int Year { get; private set; }

    public CircuitRecord(TimeSpan time, string driverName, int year)
    {
        Time = time;
        DriverName = driverName;
        Year = year;
    }

    private CircuitRecord()
    {
    }
};