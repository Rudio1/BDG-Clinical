using BGD.CLINICAL.Application.Schedules.Abstractions;
using BGD.CLINICAL.Domain.Entities;
using BGD.CLINICAL.Domain.Enums;
using BGD.CLINICAL.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BGD.CLINICAL.Infra.Data.Repositories.Schedules;

public sealed class AppointmentsRepository : IAppointmentsRepository
{
    private static readonly StatusAgendamento[] BlockingStatuses =
    [
        StatusAgendamento.Agendado,
        StatusAgendamento.Confirmado,
        StatusAgendamento.Concluido
    ];

    private readonly AppDbContext _context;

    public AppointmentsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Agendamento>> ListByEmpresaIdAsync(
        Guid empresaId,
        Guid? unidadeId,
        Guid? funcionarioId,
        Guid? pacienteId,
        StatusAgendamento? status,
        DateTime? dataInicioFrom,
        DateTime? dataInicioTo,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Agendamentos
            .AsNoTracking()
            .Include(a => a.Unidade)
            .Include(a => a.Paciente)
            .Include(a => a.Funcionario)
            .Include(a => a.Procedimento)
            .Include(a => a.CriadoPor)
            .Include(a => a.AplicacaoPaciente)
            .Where(a => a.EmpresaId == empresaId);

        if (unidadeId.HasValue)
        {
            query = query.Where(a => a.UnidadeId == unidadeId.Value);
        }

        if (funcionarioId.HasValue)
        {
            query = query.Where(a => a.FuncionarioId == funcionarioId.Value);
        }

        if (pacienteId.HasValue)
        {
            query = query.Where(a => a.PacienteId == pacienteId.Value);
        }

        if (status.HasValue)
        {
            query = query.Where(a => a.Status == status.Value);
        }

        if (dataInicioFrom.HasValue)
        {
            query = query.Where(a => a.DataInicio >= dataInicioFrom.Value);
        }

        if (dataInicioTo.HasValue)
        {
            query = query.Where(a => a.DataInicio <= dataInicioTo.Value);
        }

        return await query
            .OrderBy(a => a.DataInicio)
            .ToListAsync(cancellationToken);
    }

    public Task<Agendamento?> GetByIdAndEmpresaIdAsync(
        Guid id,
        Guid empresaId,
        CancellationToken cancellationToken = default)
    {
        return _context.Agendamentos
            .FirstOrDefaultAsync(a => a.Id == id && a.EmpresaId == empresaId, cancellationToken);
    }

    public Task<Agendamento?> GetByIdAndEmpresaIdWithDetailsAsync(
        Guid id,
        Guid empresaId,
        CancellationToken cancellationToken = default)
    {
        return _context.Agendamentos
            .Include(a => a.Empresa)
            .Include(a => a.Unidade)
            .Include(a => a.Paciente)
            .Include(a => a.Funcionario)
            .Include(a => a.Procedimento)
            .Include(a => a.CriadoPor)
            .Include(a => a.CanceladoPor)
            .Include(a => a.AplicacaoPaciente)
            .FirstOrDefaultAsync(a => a.Id == id && a.EmpresaId == empresaId, cancellationToken);
    }

    public Task<bool> HasOverlappingAppointmentAsync(
        Guid empresaId,
        Guid funcionarioId,
        Guid unidadeId,
        DateTime dataInicio,
        DateTime dataFim,
        Guid? excludeAppointmentId,
        CancellationToken cancellationToken = default)
    {
        return _context.Agendamentos.AnyAsync(
            a => a.EmpresaId == empresaId
                && a.FuncionarioId == funcionarioId
                && a.UnidadeId == unidadeId
                && BlockingStatuses.Contains(a.Status)
                && (!excludeAppointmentId.HasValue || a.Id != excludeAppointmentId.Value)
                && a.DataInicio < dataFim
                && a.DataFim > dataInicio,
            cancellationToken);
    }

    public Task<bool> HasScheduleBlockAsync(
        Guid empresaId,
        Guid funcionarioId,
        Guid unidadeId,
        DateTime dataInicio,
        DateTime dataFim,
        CancellationToken cancellationToken = default)
    {
        return _context.BloqueiosAgenda.AnyAsync(
            b => b.EmpresaId == empresaId
                && b.FuncionarioId == funcionarioId
                && b.UnidadeId == unidadeId
                && b.DataInicio < dataFim
                && b.DataFim > dataInicio,
            cancellationToken);
    }

    public async Task<bool> HasMatchingAvailabilityAsync(
        Guid empresaId,
        Guid funcionarioId,
        Guid unidadeId,
        DateTime dataInicio,
        DateTime dataFim,
        CancellationToken cancellationToken = default)
    {
        var diaSemana = (DiaSemana)(int)dataInicio.DayOfWeek;
        var horaInicio = TimeOnly.FromDateTime(dataInicio);
        var horaFim = TimeOnly.FromDateTime(dataFim);

        return await _context.DisponibilidadesFuncionario.AnyAsync(
            d => d.EmpresaId == empresaId
                && d.FuncionarioId == funcionarioId
                && d.UnidadeId == unidadeId
                && d.Ativo
                && d.DiaSemana == diaSemana
                && d.HoraInicio <= horaInicio
                && d.HoraFim >= horaFim,
            cancellationToken);
    }

    public Task<bool> HasActiveAvailabilityConfiguredAsync(
        Guid empresaId,
        Guid funcionarioId,
        Guid unidadeId,
        CancellationToken cancellationToken = default)
    {
        return _context.DisponibilidadesFuncionario.AnyAsync(
            d => d.EmpresaId == empresaId
                && d.FuncionarioId == funcionarioId
                && d.UnidadeId == unidadeId
                && d.Ativo,
            cancellationToken);
    }

    public async Task AddAsync(Agendamento agendamento, CancellationToken cancellationToken = default)
    {
        await _context.Agendamentos.AddAsync(agendamento, cancellationToken);
    }

    public void Update(Agendamento agendamento)
    {
        if (_context.Entry(agendamento).State == EntityState.Detached)
        {
            _context.Agendamentos.Update(agendamento);
        }
    }
}
