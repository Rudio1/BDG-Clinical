using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Identity.Dtos;

namespace BGD.CLINICAL.Application.Identity.Abstractions;

public interface ILoginUsersService
{
    Task<Result<AuthResponse>> ExecuteAsync(
        LoginRequest request,
        string? ip,
        CancellationToken cancellationToken = default);
}
