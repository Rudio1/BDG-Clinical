using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Identity.Abstractions;
using BGD.CLINICAL.Application.Identity.Dtos;
using BGD.CLINICAL.Domain.Entities;

namespace BGD.CLINICAL.Application.Identity.Authentications;

public sealed class LoginUsersService : ILoginUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHashGenerator _passwordHashGenerator;
    private readonly ITokenService _tokenService;
    private readonly IAuditLogsService _auditLogsService;

    public LoginUsersService(
        IUsersRepository usersRepository,
        IPasswordHashGenerator passwordHashGenerator,
        ITokenService tokenService,
        IAuditLogsService auditLogsService)
    {
        _usersRepository = usersRepository;
        _passwordHashGenerator = passwordHashGenerator;
        _tokenService = tokenService;
        _auditLogsService = auditLogsService;
    }

    public async Task<Result<AuthResponse>> ExecuteAsync(
        LoginRequest request,
        string? ip,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
        {
            return Result<AuthResponse>.Failure(IdentityValidation.CredenciaisInvalidas);
        }

        var email = IdentityValidation.NormalizeEmail(request.Email);
        var usuarios = await _usersRepository.ListByEmailLoginAsync(email, cancellationToken);

        var candidatos = usuarios
            .Where(usuario =>
                usuario.Ativo
                && usuario.AuthProvider == IdentityConstants.AuthProviderLocal
                && usuario.Empresa.Ativo)
            .ToList();

        if (candidatos.Count == 0)
        {
            return Result<AuthResponse>.Failure(IdentityValidation.CredenciaisInvalidas);
        }

        if (candidatos.Count > 1)
        {
            return Result<AuthResponse>.Failure(IdentityValidation.MultiplasContas);
        }

        var usuario = candidatos[0];

        if (!_passwordHashGenerator.Verify(request.Senha, usuario.SenhaHash))
        {
            return Result<AuthResponse>.Failure(IdentityValidation.CredenciaisInvalidas);
        }

        var token = _tokenService.GenerateToken(usuario);
        await _auditLogsService.RegisterLoginAsync(usuario.Id, usuario.EmpresaId, ip, cancellationToken);

        return Result<AuthResponse>.Success(new AuthResponse(
            token,
            AuthenticatedUsersMapper.Map(usuario)));
    }
}
