using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Teams.Mappings;
using FormulaOnce.Teams.Services.ConstructorServices;

namespace FormulaOnce.Teams.Endpoints.Constructors.UpdateConstructor;

internal class UpdateConstructor : Endpoint<UpdateConstructorRequest>
{
    private readonly IConstructorService _constructorService;

    public UpdateConstructor(IConstructorService constructorService)
    {
        _constructorService = constructorService;
    }

    public override void Configure()
    {
        Put("/teams/constructors/{Id:guid}");
        Policies("AdminOnly");
    }

    public override async Task HandleAsync(UpdateConstructorRequest req, CancellationToken ct)
    {
        var result = await _constructorService.UpdateConstructorAsync(req.ToDto(), ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}