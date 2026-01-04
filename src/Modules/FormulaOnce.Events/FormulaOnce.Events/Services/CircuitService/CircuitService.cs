using Ardalis.Result;
using FormulaOnce.Events.Domain.Circuit;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;
using FormulaOnce.Events.Infrastructure.Repositories.CircuitRepository;
using FormulaOnce.Events.Mappings;
using FormulaOnce.Events.Services.CircuitService.Dto;

namespace FormulaOnce.Events.Services.CircuitService;

public class CircuitService : ICircuitService
{
    private readonly ICircuitRepository _repository;

    public CircuitService(ICircuitRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CircuitDto>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var circuit = await _repository.GetByIdAsync(id, ct);

        if (circuit is null) return Result.NotFound();

        return Result.Success(circuit.ToDto());
    }

    public async Task<Result<IEnumerable<CircuitDto>>> GetAllAsync(CancellationToken ct = default)
    {
        var circuits = await _repository.GetAllAsync(ct);

        return Result.Success(circuits.Select(c => c.ToDto()));
    }

    public async Task<Result<CircuitDto>> CreateAsync(CreateCircuitRequest req, CancellationToken ct = default)
    {
        var coordinates = new Coordinates(req.Location.Latitude, req.Location.Longitude);

        var location = new Location(req.Location.Country, req.Location.City, coordinates);

        var circuit = Circuit.Factory.Create(req.Name, location, req.LengthKm);

        foreach (var landmarkReq in req.Landmarks)
            circuit.AddLandmark(landmarkReq.Label, landmarkReq.Type, landmarkReq.NearTurn);

        await _repository.AddAsync(circuit, ct);

        await _repository.SaveChangesAsync(ct);

        return Result.Success(circuit.ToDto());
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var circuit = await _repository.GetByIdAsync(id, ct);

        if (circuit is null) return Result.NotFound();

        _repository.Delete(circuit);

        await _repository.SaveChangesAsync(ct);

        return Result.Success();
    }
}