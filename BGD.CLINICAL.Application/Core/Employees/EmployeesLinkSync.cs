using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Core.Abstractions;
using BGD.CLINICAL.Application.Core.Dtos;
using BGD.CLINICAL.Domain.Entities;

namespace BGD.CLINICAL.Application.Core.Employees;

internal static class EmployeesLinkSync
{
    public static async Task<Result> ApplyAsync(
        Funcionario funcionario,
        Guid empresaId,
        bool linkToEmpresa,
        IReadOnlyList<Guid>? unidadeIds,
        Guid? cargoId,
        bool flagAplicador,
        IEmployeesRepository employeesRepository,
        CancellationToken cancellationToken)
    {
        if (linkToEmpresa)
        {
            funcionario.DeactivateVinculosInEmpresa(empresaId);
            funcionario.AddEmpresaVinculo(empresaId, cargoId, flagAplicador);
            return Result.Success();
        }

        if (unidadeIds is null || unidadeIds.Count == 0)
        {
            return Result.Failure("Informe ao menos uma unidade ou vincule o funcionário à empresa.");
        }

        var distinctUnidadeIds = unidadeIds.Distinct().ToList();

        if (!await employeesRepository.AllUnidadesBelongToEmpresaAsync(
                distinctUnidadeIds,
                empresaId,
                cancellationToken))
        {
            return Result.Failure("Uma ou mais unidades não pertencem à empresa.");
        }

        funcionario.DeactivateVinculosInEmpresa(empresaId);

        foreach (var unidadeId in distinctUnidadeIds)
        {
            funcionario.AddUnidadeVinculo(unidadeId, cargoId, flagAplicador);
        }

        return Result.Success();
    }
}
