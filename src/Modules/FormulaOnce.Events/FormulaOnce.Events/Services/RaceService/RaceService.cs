using Ardalis.Result;
using FormulaOnce.Events.Domain.Race;
using FormulaOnce.Events.Domain.Services;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit;
using FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;
using FormulaOnce.Events.Infrastructure.Repositories.CircuitRepository;
using FormulaOnce.Events.Infrastructure.Repositories.RaceRepository;
using FormulaOnce.Events.Mappings;
using FormulaOnce.Events.Services.RaceService.Dto;

namespace FormulaOnce.Events.Services.RaceService;

public class RaceService : IRaceService
{
    private readonly ICircuitRepository _circuitRepository;
    private readonly IRaceRepository _raceRepository;
    private readonly RaceWeekendScheduler _scheduler;

    public RaceService(
        IRaceRepository raceRepository,
        ICircuitRepository circuitRepository,
        RaceWeekendScheduler scheduler)
    {
        _raceRepository = raceRepository;
        _circuitRepository = circuitRepository;
        _scheduler = scheduler;
    }

    public async Task<Result<RaceDto>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var race = await _raceRepository.GetByIdAsync(id, ct);
        if (race is null) return Result.NotFound();

        return Result.Success(race.ToDto());
    }

    public async Task<Result<IEnumerable<RaceDto>>> GetBySeasonAsync(int season, CancellationToken ct = default)
    {
        var races = await _raceRepository.GetBySeasonAsync(season, ct);
        return Result.Success(races.Select(r => r.ToDto()));
    }

    public async Task<Result<RaceDto>> CreateAsync(CreateRaceRequest req, CancellationToken ct = default)
    {
        var circuit = await _circuitRepository.GetByIdAsync(req.CircuitId, ct);

        if (circuit is null) return Result.NotFound("The specified circuit does not exist.");

        var race = Race.Factory.Create(
            req.Title,
            req.Season,
            req.Round,
            circuit,
            req.NumberOfLaps);

        var scheduleResult = _scheduler.ScheduleWeekend(race, req.MainRaceStartTime, req.IsSprintWeekend);

        if (!scheduleResult.IsSuccess)
            return Result<RaceDto>.Error(string.Join("; ", scheduleResult.Errors));

        await _raceRepository.AddAsync(race, ct);

        await _raceRepository.SaveChangesAsync(ct);

        return Result.Success(race.ToDto());
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var race = await _raceRepository.GetByIdAsync(id, ct);

        if (race is null) return Result.NotFound();

        _raceRepository.Delete(race);

        await _raceRepository.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result<IEnumerable<RaceDto>>> GetAllAsync(CancellationToken ct = default)
    {
        var races = await _raceRepository.GetAllAsync(ct);

        return Result.Success(races.Select(r => r.ToDto()));
    }
}