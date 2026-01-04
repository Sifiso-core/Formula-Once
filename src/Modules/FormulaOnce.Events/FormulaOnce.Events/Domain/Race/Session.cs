namespace FormulaOnce.Events.Domain.Race;

public class Session
{
    private Session()
    {
    }

    public Session(Guid id, Guid raceId, DateTime scheduledStart, SessionType sessionType)
    {
        Id = id;
        RaceId = raceId;
        ScheduledStart = scheduledStart;
        SessionType = sessionType;
    }

    public Guid Id { get; private set; }
    public Guid RaceId { get; private set; }
    public DateTime ScheduledStart { get; private set; }
    public SessionType SessionType { get; private set; }
}