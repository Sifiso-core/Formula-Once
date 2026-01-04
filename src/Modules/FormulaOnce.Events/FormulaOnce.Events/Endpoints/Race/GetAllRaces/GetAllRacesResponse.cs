using FormulaOnce.Events.Services.RaceService.Dto;

namespace FormulaOnce.Events.Endpoints.Race.GetAllRaces;

public class GetAllRacesResponse
{
    public required IEnumerable<RaceDto> Races { get; set; } = [];
}