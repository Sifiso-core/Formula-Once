using FastEndpoints;
using FormulaOnce.Teams.Endpoints.Constructors._Dtos;
using FormulaOnce.Teams.Services.ConstructorServices;

namespace FormulaOnce.Teams.Endpoints.Constructors.GetConstructorById;

internal class GetConstructorById : Endpoint<ConstructorDto>
{
    private readonly IConstructorService _constructorService;

    public GetConstructorById(IConstructorService constructorService)
    {
        _constructorService = constructorService;
    }

    public override void Configure()
    {
        Get("constructors/{Id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ConstructorDto req, CancellationToken ct)
    {
        var result = await _constructorService.GetConstructorByIdAsync(req.Id, ct);

        if (!result.IsSuccess)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(result.Value, ct);
    }
}