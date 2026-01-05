using System.Security.Claims;
using FastEndpoints;
using FormulaOnce.Events.Services.RaceService;

namespace FormulaOnce.Events.Endpoints.Race.GetAllRaces;

public class GetAllRaces : EndpointWithoutRequest<GetAllRacesResponse>
{
    private readonly IRaceService _raceService;

    public GetAllRaces(IRaceService raceService)
    {
        _raceService = raceService;
    }

    public override void Configure()
    {
        Get("/events/races");
        Claims(ClaimTypes.NameIdentifier);
        Summary(s =>
        {
            s.Summary = "Get all scheduled races";
            s.Description = "Returns a list of all race weekends, including their full session schedules.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _raceService.GetAllAsync(ct);

        // Since it's a list, even an empty collection is a Success (200 OK)
        var response = new GetAllRacesResponse
        {
            Races = result.Value
        };

        await Send.OkAsync(response, cancellation: ct);
    }
}