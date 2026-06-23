using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Identity.Dtos;

namespace BGD.CLINICAL.Application.Identity.Abstractions;

public interface IRegisterCompaniesService
{
    Task<Result<AuthResponse>> ExecuteAsync(
        RegisterCompanyRequest request,
        CancellationToken cancellationToken = default);
}
