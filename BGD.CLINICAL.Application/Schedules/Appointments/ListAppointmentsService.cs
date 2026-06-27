using BGD.CLINICAL.Application.Abstractions.Security;
using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Schedules.Abstractions;
using BGD.CLINICAL.Application.Schedules.Dtos;
using BGD.CLINICAL.Domain.Enums;

namespace BGD.CLINICAL.Application.Schedules.Appointments;

public interface IListAppointmentsService
{
    Task<Result<IReadOnlyList<AppointmentDto>>> ExecuteAsync(
        Guid? unidadeId,
        Guid? funcionarioId,
        Guid? pacienteId,
        string? status,
        DateTime? dataInicioFrom,
        DateTime? dataInicioTo,
        CancellationToken cancellationToken = default);
}

public sealed class ListAppointmentsService : IListAppointmentsService
{
    private readonly ICurrentTenantContext _tenantContext;
    private readonly IAppointmentsRepository _appointmentsRepository;

    public ListAppointmentsService(
        ICurrentTenantContext tenantContext,
        IAppointmentsRepository appointmentsRepository)
    {
        _tenantContext = tenantContext;
        _appointmentsRepository = appointmentsRepository;
    }

    public async Task<Result<IReadOnlyList<AppointmentDto>>> ExecuteAsync(
        Guid? unidadeId,
        Guid? funcionarioId,
        Guid? pacienteId,
        string? status,
        DateTime? dataInicioFrom,
        DateTime? dataInicioTo,
        CancellationToken cancellationToken = default)
    {
        StatusAgendamento? statusFilter = null;

        if (!string.IsNullOrWhiteSpace(status))
        {
            if (!Enum.TryParse<StatusAgendamento>(status, ignoreCase: true, out var parsed))
            {
                return Result<IReadOnlyList<AppointmentDto>>.Failure("Status de agendamento inválido.");
            }

            statusFilter = parsed;
        }

        var agendamentos = await _appointmentsRepository.ListByEmpresaIdAsync(
            _tenantContext.EmpresaId,
            unidadeId,
            funcionarioId,
            pacienteId,
            statusFilter,
            dataInicioFrom,
            dataInicioTo,
            cancellationToken);

        var dtos = agendamentos.Select(AppointmentsMapper.Map).ToList();
        return Result<IReadOnlyList<AppointmentDto>>.Success(dtos);
    }
}
