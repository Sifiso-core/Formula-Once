using FormulaOnce.Teams.Domain.Constructor;
using FormulaOnce.Teams.Endpoints.Constructors.CreateConstructor;
using FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor;
using FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;
using FormulaOnce.Teams.Services.ConstructorServices.Dto;

namespace FormulaOnce.Teams.Mappings;

internal static class ConstructorMappingExtensions
{
    public static ConstructorDto ToDto(this Constructor constructor)
    {
        return new ConstructorDto(
            constructor.Id,
            constructor.Name,
            constructor.BaseLocation,
            constructor.EstablishedYear,
            constructor.Stats.ToDto());
    }

    public static ConstructorWithDriversDto ToConstructorWithDriversDto(this Constructor constructor)
    {
        return new ConstructorWithDriversDto(
            constructor.Id,
            constructor.Name,
            constructor.BaseLocation,
            constructor.EstablishedYear,
            constructor.Drivers.Select(d => d.ToDto()).ToList(),
            constructor.Stats.ToDto());
    }

    private static ConstructorStatsDto ToDto(this ConstructorStats stats)
    {
        return new ConstructorStatsDto(
            stats.AllTimeSummary.ToDto(),
            stats.SeasonStats.ToDto());
    }

    private static AllTimeSummaryDto ToDto(this AllTimeSummary s)
    {
        return new AllTimeSummaryDto(
            s.GrandPrixEntered, s.TeamPoints, s.HighestRaceFinish,
            s.Podiums, s.HighestGridPosition, s.PolePositions, s.WorldChampionships);
    }

    private static SeasonStatsDto ToDto(this SeasonStats s)
    {
        return new SeasonStatsDto(
            s.SeasonPosition, s.SeasonPoints,
            s.GrandPrixStats.ToDto(), s.SprintStats.ToDto(),
            s.DhlFastestLaps, s.DNFs);
    }

    private static GrandPrixStatsDto ToDto(this GrandPrixStats s)
    {
        return new GrandPrixStatsDto(
            s.GrandPrixRaces, s.GrandPrixPoints, s.GrandPrixWins,
            s.GrandPrixPodiums, s.GrandPrixPoles, s.GrandPrixTop10s);
    }

    private static SprintStatsDto ToDto(this SprintStats s)
    {
        return new SprintStatsDto(
            s.SprintRaces, s.SprintPoints, s.SprintWins,
            s.SprintPodiums, s.SprintPoles, s.SprintTop10s);
    }

    public static ConstructorDto ToDto(this CreateConstructorRequest req)
    {
        return new ConstructorDto(Guid.Empty, req.Name, req.BaseLocation, req.EstablishedYear, req.Stats.ToDto());
    }

    public static ConstructorDto ToDto(this UpdateConstructorRequest req)
    {
        return new ConstructorDto(req.Id, req.Name, req.BaseLocation, req.EstablishedYear, req.Stats.ToDto());
    }

    private static ConstructorStatsDto ToDto(this CreateStatsDto s)
    {
        return new ConstructorStatsDto(s.AllTimeSummary.ToDto(), s.SeasonStats.ToDto());
    }

    private static ConstructorStatsDto ToDto(this UpdateStatsDto s)
    {
        return new ConstructorStatsDto(s.AllTimeSummary.ToDto(), s.SeasonStats.ToDto());
    }

    private static AllTimeSummaryDto ToDto(this UpdateAllTimeSummaryDto s)
    {
        return new AllTimeSummaryDto(
            s.GrandPrixEntered, s.TeamPoints, s.HighestRaceFinish,
            s.Podiums, s.HighestGridPosition, s.PolePositions, s.WorldChampionships);
    }

    private static SeasonStatsDto ToDto(this UpdateSeasonStatsDto s)
    {
        return new SeasonStatsDto(
            s.SeasonPosition, s.SeasonPoints,
            s.GrandPrixStats.ToDto(), s.SprintStats.ToDto(),
            s.DhlFastestLaps, s.DNFs);
    }

    private static GrandPrixStatsDto ToDto(this UpdateGrandPrixStatsDto s)
    {
        return new GrandPrixStatsDto(
            s.GrandPrixRaces, s.GrandPrixPoints, s.GrandPrixWins,
            s.GrandPrixPodiums, s.GrandPrixPoles, s.GrandPrixTop10s);
    }

    private static SprintStatsDto ToDto(this UpdateSprintStatsDto s)
    {
        return new SprintStatsDto(
            s.SprintRaces, s.SprintPoints, s.SprintWins,
            s.SprintPodiums, s.SprintPoles, s.SprintTop10s);
    }

    public static Constructor ToEntity(this ConstructorDto dto)
    {
        var stats = new ConstructorStats(
            new AllTimeSummary(
                dto.Stats.AllTimeSummary.GrandPrixEntered, dto.Stats.AllTimeSummary.TeamPoints,
                dto.Stats.AllTimeSummary.HighestRaceFinish, dto.Stats.AllTimeSummary.Podiums,
                dto.Stats.AllTimeSummary.HighestGridPosition, dto.Stats.AllTimeSummary.PolePositions,
                dto.Stats.AllTimeSummary.WorldChampionships),
            new SeasonStats(
                dto.Stats.SeasonStats.SeasonPosition, dto.Stats.SeasonStats.SeasonPoints,
                new GrandPrixStats(
                    dto.Stats.SeasonStats.GrandPrixStats.GrandPrixRaces,
                    dto.Stats.SeasonStats.GrandPrixStats.GrandPrixPoints,
                    dto.Stats.SeasonStats.GrandPrixStats.GrandPrixWins,
                    dto.Stats.SeasonStats.GrandPrixStats.GrandPrixPodiums,
                    dto.Stats.SeasonStats.GrandPrixStats.GrandPrixPoles,
                    dto.Stats.SeasonStats.GrandPrixStats.GrandPrixTop10s),
                new SprintStats(
                    dto.Stats.SeasonStats.SprintStats.SprintRaces, dto.Stats.SeasonStats.SprintStats.SprintPoints,
                    dto.Stats.SeasonStats.SprintStats.SprintWins, dto.Stats.SeasonStats.SprintStats.SprintPodiums,
                    dto.Stats.SeasonStats.SprintStats.SprintPoles, dto.Stats.SeasonStats.SprintStats.SprintTop10s),
                dto.Stats.SeasonStats.DhlFastestLaps, dto.Stats.SeasonStats.DNFs));

        return Constructor.Factory.Create(dto.Name, dto.BaseLocation, dto.EstablishedYear, stats,
            dto.Id == Guid.Empty ? null : dto.Id);
    }
}