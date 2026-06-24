using BGD.CLINICAL.Application.Abstractions.Security;
using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Core.Abstractions;
using BGD.CLINICAL.Application.Core.Dtos;

namespace BGD.CLINICAL.Application.Core.Companies;

public interface IGetCurrentCompanyService
{
    Task<Result<CompanyDto>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetCurrentCompanyService : IGetCurrentCompanyService
{
    private readonly ICurrentTenantContext _tenantContext;
    private readonly ICompaniesRepository _companiesRepository;

    public GetCurrentCompanyService(
        ICurrentTenantContext tenantContext,
        ICompaniesRepository companiesRepository)
    {
        _tenantContext = tenantContext;
        _companiesRepository = companiesRepository;
    }

    public async Task<Result<CompanyDto>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var empresa = await _companiesRepository.GetByIdAsync(
            _tenantContext.EmpresaId,
            cancellationToken);

        if (empresa is null)
        {
            return Result<CompanyDto>.Failure("Empresa não encontrada.");
        }

        return Result<CompanyDto>.Success(CompaniesMapper.Map(empresa));
    }
}
