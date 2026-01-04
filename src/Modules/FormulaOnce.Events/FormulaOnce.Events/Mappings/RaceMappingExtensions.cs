using FormulaOnce.Events.Domain.Race;
using FormulaOnce.Events.Services.RaceService.Dto;

namespace FormulaOnce.Events.Mappings;

public static class RaceMappingExtensions
{
    public static RaceDto ToDto(this Race race)
    {
        return new RaceDto(
            race.Id,
            race.Title,
            race.Season,
            race.Round,
            race.CircuitId,
            race.NumberOfLaps,
            race.RaceDistanceKm,
            race.Sessions.Select(s => s.ToDto()).ToList()
        );
    }

    public static SessionDto ToDto(this Session session)
    {
        return new SessionDto(
            session.Id,
            session.ScheduledStart,
            session.SessionType.ToString()
        );
    }
}