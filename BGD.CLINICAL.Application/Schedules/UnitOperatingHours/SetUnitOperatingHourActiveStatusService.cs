using BGD.CLINICAL.Application.Abstractions.Persistence;
using BGD.CLINICAL.Application.Abstractions.Security;
using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Schedules.Abstractions;
using BGD.CLINICAL.Application.Schedules.Dtos;
using BGD.CLINICAL.Domain.Exceptions;

namespace BGD.CLINICAL.Application.Schedules.UnitOperatingHours;

public interface ISetUnitOperatingHourActiveStatusService
{
    Task<Result<UnitOperatingHourDto>> ExecuteAsync(
        Guid id,
        SetUnitOperatingHourActiveRequest request,
        CancellationToken cancellationToken = default);
}

public sealed class SetUnitOperatingHourActiveStatusService : ISetUnitOperatingHourActiveStatusService
{
    private readonly ICurrentTenantContext _tenantContext;
    private readonly IUnitOperatingHoursRepository _operatingHoursRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetUnitOperatingHourActiveStatusService(
        ICurrentTenantContext tenantContext,
        IUnitOperatingHoursRepository operatingHoursRepository,
        IUnitOfWork unitOfWork)
    {
        _tenantContext = tenantContext;
        _operatingHoursRepository = operatingHoursRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UnitOperatingHourDto>> ExecuteAsync(
        Guid id,
        SetUnitOperatingHourActiveRequest request,
        CancellationToken cancellationToken = default)
    {
        var empresaId = _tenantContext.EmpresaId;

        var horario = await _operatingHoursRepository.GetByIdAndEmpresaIdAsync(id, empresaId, cancellationToken);
        if (horario is null)
        {
            return Result<UnitOperatingHourDto>.Failure("Horário de funcionamento não encontrado.");
        }

        if (request.Ativo)
        {
            var overlapValidation = await UnitOperatingHoursValidator.EnsureNoOverlapAsync(
                empresaId,
                horario.UnidadeId,
                horario.DiaSemana,
                horario.HoraInicio,
                horario.HoraFim,
                _operatingHoursRepository,
                excludeId: id,
                onlyActive: true,
                cancellationToken);

            if (overlapValidation.IsFailure)
            {
                return Result<UnitOperatingHourDto>.Failure(overlapValidation.Error!);
            }
        }

        try
        {
            horario.SetActive(request.Ativo);

            _operatingHoursRepository.Update(horario);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<UnitOperatingHourDto>.Success(UnitOperatingHoursMapper.Map(horario));
        }
        catch (DomainException exception)
        {
            return Result<UnitOperatingHourDto>.Failure(exception.Message);
        }
    }
}
