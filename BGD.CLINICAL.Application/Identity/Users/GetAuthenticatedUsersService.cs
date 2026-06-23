using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Identity.Abstractions;
using BGD.CLINICAL.Application.Identity.Dtos;
using System.Security.Claims;

namespace BGD.CLINICAL.Application.Identity.Users;

public sealed class GetAuthenticatedUsersService : IGetAuthenticatedUsersService
{
    private readonly IUsersRepository _usersRepository;

    public GetAuthenticatedUsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<Result<AuthenticatedUserDto>> ExecuteAsync(
        ClaimsPrincipal principal,
        CancellationToken cancellationToken = default)
    {
        var usuarioIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? principal.FindFirst("sub")?.Value;

        if (!Guid.TryParse(usuarioIdClaim, out var usuarioId))
        {
            return Result<AuthenticatedUserDto>.Failure("Usuário não autenticado.");
        }

        var usuario = await _usersRepository.GetByIdAsync(usuarioId, cancellationToken);

        if (usuario is null || !usuario.Ativo || !usuario.Empresa.Ativo)
        {
            return Result<AuthenticatedUserDto>.Failure("Usuário não autenticado.");
        }

        return Result<AuthenticatedUserDto>.Success(AuthenticatedUsersMapper.Map(usuario));
    }
}
