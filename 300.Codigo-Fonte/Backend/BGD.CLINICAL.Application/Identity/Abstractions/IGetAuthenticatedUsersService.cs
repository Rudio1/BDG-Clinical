using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Identity.Dtos;
using System.Security.Claims;

namespace BGD.CLINICAL.Application.Identity.Abstractions;

public interface IGetAuthenticatedUsersService
{
    Task<Result<AuthenticatedUserDto>> ExecuteAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default);
}
