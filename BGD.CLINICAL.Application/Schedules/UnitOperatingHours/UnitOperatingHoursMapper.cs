using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Core.Abstractions;
using BGD.CLINICAL.Application.Schedules.Abstractions;
using BGD.CLINICAL.Application.Schedules.Dtos;
using BGD.CLINICAL.Domain.Entities;
using BGD.CLINICAL.Domain.Enums;

namespace BGD.CLINICAL.Application.Schedules.UnitOperatingHours;

internal static class UnitOperatingHoursMapper
{
    public static UnitOperatingHourDto Map(HorarioFuncionamentoUnidade horario)
    {
        return new UnitOperatingHourDto(
            horario.Id,
            horario.UnidadeId,
            horario.DiaSemana.ToString(),
            horario.HoraInicio,
            horario.HoraFim,
            horario.Ativo,
            horario.CriadoEm,
            horario.AtualizadoEm);
    }
}

internal static class UnitOperatingHoursValidator
{
    public static Result<DiaSemana> ParseDiaSemana(string diaSemana)
    {
        if (!Enum.TryParse<DiaSemana>(diaSemana, ignoreCase: true, out var parsed))
        {
            return Result<DiaSemana>.Failure("Dia da semana inválido.");
        }

        return Result<DiaSemana>.Success(parsed);
    }

    public static async Task<Result> EnsureUnitExistsAsync(
        Guid empresaId,
        Guid unidadeId,
        IUnitsRepository unitsRepository,
        CancellationToken cancellationToken)
    {
        var unidade = await unitsRepository.GetByIdAndEmpresaIdAsync(unidadeId, empresaId, cancellationToken);
        if (unidade is null || !unidade.Ativo)
        {
            return Result.Failure("Unidade não encontrada ou inativa.");
        }

        return Result.Success();
    }

    public static async Task<Result> EnsureNoOverlapAsync(
        Guid empresaId,
        Guid unidadeId,
        DiaSemana diaSemana,
        TimeOnly horaInicio,
        TimeOnly horaFim,
        IUnitOperatingHoursRepository operatingHoursRepository,
        Guid? excludeId = null,
        bool onlyActive = false,
        CancellationToken cancellationToken = default)
    {
        if (await operatingHoursRepository.HasOverlappingOperatingHourAsync(
                empresaId,
                unidadeId,
                diaSemana,
                horaInicio,
                horaFim,
                excludeId,
                onlyActive,
                cancellationToken))
        {
            return Result.Failure(
                "Já existe horário de funcionamento para este dia que coincide ou se sobrepõe ao intervalo informado.");
        }

        return Result.Success();
    }
}
