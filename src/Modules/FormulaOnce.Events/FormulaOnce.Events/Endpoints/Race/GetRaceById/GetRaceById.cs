using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Events.Services.RaceService;
using FormulaOnce.Events.Services.RaceService.Dto;

namespace FormulaOnce.Events.Endpoints.Race.GetRaceById;

public class GetRaceById : Endpoint<GetRaceRequest, RaceDto>
{
    private readonly IRaceService _raceService;

    public GetRaceById(IRaceService raceService)
    {
        _raceService = raceService;
    }

    public override void Configure()
    {
        Get("/events/races/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetRaceRequest req, CancellationToken ct)
    {
        var result = await _raceService.GetByIdAsync(req.Id, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(result.Value, ct);
    }
}