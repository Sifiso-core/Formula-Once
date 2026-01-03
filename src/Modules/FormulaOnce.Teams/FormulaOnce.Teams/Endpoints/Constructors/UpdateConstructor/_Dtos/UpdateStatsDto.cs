namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

public record UpdateStatsDto(
    UpdateAllTimeSummaryDto AllTimeSummary,
    UpdateSeasonStatsDto SeasonStats);