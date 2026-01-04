using FastEndpoints;
using FormulaOnce.Teams.Services.ConstructorServices;

namespace FormulaOnce.Teams.Endpoints.Constructors.GetAllConstructors;

internal class GetAllConstructors : Endpoint<GetAllConstructorResponse>
{
    private readonly IConstructorService _constructorService;

    public GetAllConstructors(IConstructorService constructorService)
    {
        _constructorService = constructorService;
    }

    public override void Configure()
    {
        Get("/teams/constructors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllConstructorResponse req, CancellationToken ct)
    {
        var result = await _constructorService.GetAllConstructorsAsync(ct);

        await Send.OkAsync(new GetAllConstructorResponse
        {
            Constructors = result.Value.ToList()
        }, ct);
    }
}