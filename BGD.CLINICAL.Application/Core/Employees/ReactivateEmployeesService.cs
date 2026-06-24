using BGD.CLINICAL.Application.Abstractions.Identity;
using BGD.CLINICAL.Application.Abstractions.Persistence;
using BGD.CLINICAL.Application.Abstractions.Security;
using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Core.Abstractions;
using BGD.CLINICAL.Application.Core.Dtos;
using BGD.CLINICAL.Application.Identity.Abstractions;
using BGD.CLINICAL.Domain.Entities;
using BGD.CLINICAL.Domain.Enums;
using BGD.CLINICAL.Domain.Exceptions;

namespace BGD.CLINICAL.Application.Core.Employees;

public interface IReactivateEmployeesService
{
    Task<Result<EmployeeDto>> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}

public sealed class ReactivateEmployeesService : IReactivateEmployeesService
{
    private readonly ICurrentTenantContext _tenantContext;
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IProvisionEmployeeUsersService _provisionEmployeeUsersService;
    private readonly IAuditLogsService _auditLogsService;
    private readonly IUnitOfWork _unitOfWork;

    public ReactivateEmployeesService(
        ICurrentTenantContext tenantContext,
        IEmployeesRepository employeesRepository,
        IProvisionEmployeeUsersService provisionEmployeeUsersService,
        IAuditLogsService auditLogsService,
        IUnitOfWork unitOfWork)
    {
        _tenantContext = tenantContext;
        _employeesRepository = employeesRepository;
        _provisionEmployeeUsersService = provisionEmployeeUsersService;
        _auditLogsService = auditLogsService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EmployeeDto>> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var empresaId = _tenantContext.EmpresaId;
        var funcionario = await _employeesRepository.GetByIdAndEmpresaIdAsync(id, empresaId, cancellationToken);

        if (funcionario is null)
        {
            return Result<EmployeeDto>.Failure("Funcionário não encontrado.");
        }

        if (funcionario.Ativo && funcionario.HasActiveVinculoInEmpresa(empresaId))
        {
            return Result<EmployeeDto>.Failure("Funcionário já está ativo.");
        }

        var userAccessInfo = await _employeesRepository.GetUserAccessInfoByFuncionarioAndEmpresaAsync(
            funcionario.Id,
            empresaId,
            cancellationToken);

        var emailLogin = userAccessInfo?.EmailLogin;
        var dadosAnteriores = EmployeesAuditSerializer.Serialize(funcionario, empresaId, emailLogin);

        try
        {
            funcionario.ReactivateVinculosInEmpresa(empresaId);
            funcionario.Reactivate();

            await _provisionEmployeeUsersService.ReactivateByFuncionarioAsync(
                empresaId,
                funcionario.Id,
                cancellationToken);

            _employeesRepository.Update(funcionario);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            userAccessInfo = await _employeesRepository.GetUserAccessInfoByFuncionarioAndEmpresaAsync(
                funcionario.Id,
                empresaId,
                cancellationToken);

            await _auditLogsService.RegisterEntityChangeAsync(
                empresaId,
                _tenantContext.UsuarioId,
                nameof(Funcionario),
                funcionario.Id,
                AcaoAuditoria.Editar,
                dadosAnteriores: dadosAnteriores,
                dadosNovos: EmployeesAuditSerializer.Serialize(funcionario, empresaId, userAccessInfo?.EmailLogin),
                cancellationToken: cancellationToken);

            return Result<EmployeeDto>.Success(EmployeesMapper.Map(funcionario, empresaId, userAccessInfo));
        }
        catch (DomainException exception)
        {
            return Result<EmployeeDto>.Failure(exception.Message);
        }
    }
}
