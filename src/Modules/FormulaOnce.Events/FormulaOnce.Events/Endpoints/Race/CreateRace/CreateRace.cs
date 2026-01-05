using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;
using FormulaOnce.Events.Services.RaceService;
using FormulaOnce.Events.Services.RaceService.Dto;

namespace FormulaOnce.Events.Endpoints.Race.CreateRace;

public class CreateRace : Endpoint<CreateRaceRequest, RaceDto>
{
    private readonly IRaceService _raceService;

    public CreateRace(IRaceService raceService)
    {
        _raceService = raceService;
    }

    public override void Configure()
    {
        Post("/events/races");
        Policies("AdminOnly");
        Summary(s =>
        {
            s.Summary = "Create a new race weekend";
            s.Description =
                "Generates a full F1 weekend schedule (Practice, Qualifying, and Race) based on a start date.";
        });
    }

    public override async Task HandleAsync(CreateRaceRequest req, CancellationToken ct)
    {
        var result = await _raceService.CreateAsync(req, ct);

        if (result.IsSuccess)
        {
            await Send.CreatedAtAsync<GetRaceById.GetRaceById>(
                new { result.Value.Id },
                result.Value,
                generateAbsoluteUrl: true,
                cancellation: ct);
            return;
        }

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        foreach (var error in result.Errors) AddError(error);

        await Send.ErrorsAsync(cancellation: ct);
    }
}