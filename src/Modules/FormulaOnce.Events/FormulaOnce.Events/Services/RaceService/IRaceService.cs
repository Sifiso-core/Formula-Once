using Ardalis.Result;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;
using FormulaOnce.Events.Services.RaceService.Dto;

namespace FormulaOnce.Events.Services.RaceService;

public interface IRaceService
{
    Task<Result<RaceDto>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<IEnumerable<RaceDto>>> GetBySeasonAsync(int season, CancellationToken ct = default);
    Task<Result<RaceDto>> CreateAsync(CreateRaceRequest req, CancellationToken ct = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<Result<IEnumerable<RaceDto>>> GetAllAsync(CancellationToken ct = default);
}