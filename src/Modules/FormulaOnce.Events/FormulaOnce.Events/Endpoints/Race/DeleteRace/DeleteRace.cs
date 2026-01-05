using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Events.Services.RaceService;

namespace FormulaOnce.Events.Endpoints.Race.DeleteRace;

public class DeleteRace : Endpoint<DeleteRaceRequest>
{
    private readonly IRaceService _raceService;

    public DeleteRace(IRaceService raceService)
    {
        _raceService = raceService;
    }

    public override void Configure()
    {
        Delete("/events/races/{Id}");
        Policies("AdminOnly");
        Summary(s =>
        {
            s.Summary = "Delete a race weekend";
            s.Description = "Removes the race and all its scheduled sessions (Practice, Quali, etc.).";
        });
    }

    public override async Task HandleAsync(DeleteRaceRequest req, CancellationToken ct)
    {
        var result = await _raceService.DeleteAsync(req.Id, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}