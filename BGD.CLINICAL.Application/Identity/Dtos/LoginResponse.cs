using BGD.CLINICAL.Application.Core.Dtos;

namespace BGD.CLINICAL.Application.Identity.Dtos;

public sealed record LoginResponse(
    bool RequiresCompanySelection,
    string? Token,
    AuthenticatedUserDto? Usuario,
    IReadOnlyList<UserCompanyDto>? Companies);
