using Ardalis.Result;
using FormulaOnce.Teams.Services.ConstructorServices.Dto;

namespace FormulaOnce.Teams.Services.ConstructorServices;

internal interface IConstructorService
{
    Task<Result<IEnumerable<ConstructorDto>>> GetAllConstructorsAsync(CancellationToken ct = default);

    Task<Result<ConstructorWithDriversDto>> GetConstructorByIdAsync(Guid id, CancellationToken ct = default);

    Task<Result<ConstructorDto>> CreateConstructorAsync(ConstructorDto dto, CancellationToken ct = default);

    Task<Result> UpdateConstructorAsync(ConstructorDto dto, CancellationToken ct = default);

    Task<Result> DeleteConstructorAsync(Guid id, CancellationToken ct = default);
}