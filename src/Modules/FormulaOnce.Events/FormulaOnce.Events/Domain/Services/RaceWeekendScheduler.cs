using Ardalis.Result;
using FormulaOnce.Events.Domain.Race;

namespace FormulaOnce.Events.Domain.Services;

public class RaceWeekendScheduler
{
    public Result ScheduleWeekend(Domain.Race.Race race, DateTime raceStart, bool isSprint)
    {
        var sessionsToSchedule = new List<(SessionType Type, DateTime Time)>();

        if (isSprint)
        {
            // Friday: FP1 and Sprint Qualifying (Shootout)
            var sqStart = raceStart.AddDays(-2);
            sessionsToSchedule.Add((SessionType.Fp1, sqStart.AddHours(-4)));
            sessionsToSchedule.Add((SessionType.Sq1, sqStart));
            sessionsToSchedule.Add((SessionType.Sq2, sqStart.AddMinutes(25)));
            sessionsToSchedule.Add((SessionType.Sq3, sqStart.AddMinutes(45)));

            // Saturday: Sprint Race and Main Qualifying
            var qStart = raceStart.AddDays(-1);
            sessionsToSchedule.Add((SessionType.Sprint, qStart.AddHours(-4)));
            sessionsToSchedule.Add((SessionType.Q1, qStart));
            sessionsToSchedule.Add((SessionType.Q2, qStart.AddMinutes(25)));
            sessionsToSchedule.Add((SessionType.Q3, qStart.AddMinutes(50)));
        }
        else
        {
            // Friday: Practice
            sessionsToSchedule.Add((SessionType.Fp1, raceStart.AddDays(-2).AddHours(-6)));
            sessionsToSchedule.Add((SessionType.Fp2, raceStart.AddDays(-2).AddHours(-2)));

            // Saturday: FP3 and Qualifying
            var qStart = raceStart.AddDays(-1).AddHours(-1);
            sessionsToSchedule.Add((SessionType.Fp3, qStart.AddHours(-4)));
            sessionsToSchedule.Add((SessionType.Q1, qStart));
            sessionsToSchedule.Add((SessionType.Q2, qStart.AddMinutes(25)));
            sessionsToSchedule.Add((SessionType.Q3, qStart.AddMinutes(50)));
        }

        // Sunday: The Grand Prix
        sessionsToSchedule.Add((SessionType.Race, raceStart));

        foreach (var session in sessionsToSchedule)
        {
            var result = race.ScheduleSession(session.Type, session.Time);
            if (!result.IsSuccess) return result;
        }

        return Result.Success();
    }
}