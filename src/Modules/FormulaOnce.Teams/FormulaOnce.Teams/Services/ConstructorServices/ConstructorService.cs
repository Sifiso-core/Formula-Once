using Ardalis.Result;
using FormulaOnce.Teams.Endpoints.Constructors._Dtos;
using FormulaOnce.Teams.Infrastructure.ConstructorRepository;
using FormulaOnce.Teams.Mappings;

namespace FormulaOnce.Teams.Services.ConstructorServices;

internal class ConstructorService : IConstructorService
{
    private readonly IConstructorRepository _repository;

    public ConstructorService(IConstructorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ConstructorDto>>> GetAllConstructorsAsync(CancellationToken ct = default)
    {
        var constructors = await _repository.GetAllConstructorsAsync(ct);
        return Result.Success(constructors.Select(c => c.ToDto()));
    }

    public async Task<Result<ConstructorWithDriversDto>> GetConstructorByIdAsync(Guid id,
        CancellationToken ct = default)
    {
        var constructor = await _repository.GetConstructorByIdAsync(id, ct);

        if (constructor is null) return Result.NotFound();

        return Result.Success(constructor.ToConstructorWithDriversDto());
    }

    public async Task<Result<ConstructorDto>> CreateConstructorAsync(ConstructorDto dto, CancellationToken ct = default)
    {
        var constructor = dto.ToEntity();

        await _repository.CreateConstructorAsync(constructor, ct);
        await _repository.SaveChangesAsync(ct);

        return Result.Success(constructor.ToDto());
    }

    public async Task<Result> UpdateConstructorAsync(ConstructorDto dto, CancellationToken ct = default)
    {
        
        var exists = await _repository.GetConstructorByIdAsync(dto.Id, ct);
        if (exists is null) return Result.NotFound();

        
        await _repository.UpdateConstructorAsync(dto.ToEntity(), ct);
        await _repository.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result> DeleteConstructorAsync(Guid id, CancellationToken ct = default)
    {
        
        var deleted = await _repository.DeleteConstructorAsync(id, ct);
    
        if (!deleted) 
        {
            return Result.NotFound();
        }

        await _repository.SaveChangesAsync(ct);
        return Result.Success();
    }
}