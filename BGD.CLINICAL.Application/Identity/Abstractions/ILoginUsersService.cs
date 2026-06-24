using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Identity.Dtos;

namespace BGD.CLINICAL.Application.Identity.Abstractions;

public interface ILoginUsersService
{
    Task<Result<LoginResponse>> ExecuteAsync(
        LoginRequest request,
        string? ip,
        CancellationToken cancellationToken = default);
}
