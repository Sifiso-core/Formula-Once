using FastEndpoints;
using FormulaOnce.Teams.Mappings;
using FormulaOnce.Teams.Services.ConstructorServices;
using FormulaOnce.Teams.Services.ConstructorServices.Dto;

namespace FormulaOnce.Teams.Endpoints.Constructors.CreateConstructor;

internal class CreateConstructor : Endpoint<CreateConstructorRequest, ConstructorDto>
{
    private readonly IConstructorService _constructorService;

    public CreateConstructor(IConstructorService constructorService)
    {
        _constructorService = constructorService;
    }

    public override void Configure()
    {
        Post("/teams/constructors");
        Policies("AdminOnly");
    }

    public override async Task HandleAsync(CreateConstructorRequest req, CancellationToken ct)
    {
        var result = await _constructorService.CreateConstructorAsync(req.ToDto(), ct);

        await Send.CreatedAtAsync<GetConstructorById.GetConstructorById>(
            new { result.Value.Id },
            result.Value,
            cancellation: ct);
    }
}