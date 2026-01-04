using Ardalis.Result;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;
using FormulaOnce.Events.Services.CircuitService.Dto;

namespace FormulaOnce.Events.Services.CircuitService;

public interface ICircuitService
{
    Task<Result<CircuitDto>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<IEnumerable<CircuitDto>>> GetAllAsync(CancellationToken ct = default);
    Task<Result<CircuitDto>> CreateAsync(CreateCircuitRequest req, CancellationToken ct = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken ct = default);
}