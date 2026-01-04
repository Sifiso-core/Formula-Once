using Ardalis.Result;
using FastEndpoints;
using FormulaOnce.Teams.Services.ConstructorServices;

namespace FormulaOnce.Teams.Endpoints.Constructors.DeleteConstructor;

internal class DeleteConstructor : Endpoint<DeleteConstructorRequest>
{
    private readonly IConstructorService _constructorService;

    public DeleteConstructor(IConstructorService constructorService)
    {
        _constructorService = constructorService;
    }

    public override void Configure()
    {
        Delete("/teams/constructors/{Id:guid}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteConstructorRequest req, CancellationToken ct)
    {
        var result = await _constructorService.DeleteConstructorAsync(req.Id, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}