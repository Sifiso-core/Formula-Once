using FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor._Dtos;

namespace FormulaOnce.Teams.Endpoints.Constructors.CreateConstructor;

public record CreateStatsDto(
    UpdateAllTimeSummaryDto AllTimeSummary,
    UpdateSeasonStatsDto SeasonStats);