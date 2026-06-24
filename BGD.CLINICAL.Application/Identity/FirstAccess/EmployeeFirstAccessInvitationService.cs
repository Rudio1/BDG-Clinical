using BGD.CLINICAL.Application.Abstractions.Notifications;
using BGD.CLINICAL.Application.Common;
using BGD.CLINICAL.Application.Identity.Abstractions;
using BGD.CLINICAL.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BGD.CLINICAL.Application.Identity.FirstAccess;

public interface IEmployeeFirstAccessInvitationService
{
    Task<Result<string>> StageInvitationAsync(
        Guid usuarioId,
        CancellationToken cancellationToken = default);

    Task<Result> SendInvitationEmailAsync(
        Usuario usuario,
        string empresaNome,
        string rawToken,
        CancellationToken cancellationToken = default);
}

public sealed class EmployeeFirstAccessInvitationService : IEmployeeFirstAccessInvitationService
{
    private readonly IFirstAccessInvitationsRepository _invitationsRepository;
    private readonly IEmailSender _emailSender;
    private readonly FirstAccessSettings _settings;
    private readonly ILogger<EmployeeFirstAccessInvitationService> _logger;

    public EmployeeFirstAccessInvitationService(
        IFirstAccessInvitationsRepository invitationsRepository,
        IEmailSender emailSender,
        IOptions<FirstAccessSettings> settings,
        ILogger<EmployeeFirstAccessInvitationService> logger)
    {
        _invitationsRepository = invitationsRepository;
        _emailSender = emailSender;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<Result<string>> StageInvitationAsync(
        Guid usuarioId,
        CancellationToken cancellationToken = default)
    {
        await _invitationsRepository.InvalidatePendingByUsuarioIdAsync(usuarioId, cancellationToken);

        var (convite, rawToken) = ConvitePrimeiroAcesso.Create(
            usuarioId,
            _settings.TokenExpirationHours);

        await _invitationsRepository.AddAsync(convite, cancellationToken);
        return Result<string>.Success(rawToken);
    }

    public async Task<Result> SendInvitationEmailAsync(
        Usuario usuario,
        string empresaNome,
        string rawToken,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_settings.FrontendBaseUrl))
        {
            return Result.Failure("URL do frontend não configurada para primeiro acesso.");
        }

        var firstAccessUrl = BuildFirstAccessUrl(rawToken);
        var subject = $"Acesso à plataforma — {empresaNome}";
        var htmlBody = FirstAccessInvitationEmailTemplate.Build(
            usuario.Nome,
            empresaNome,
            firstAccessUrl,
            _settings.TokenExpirationHours);

        try
        {
            await _emailSender.SendAsync(usuario.EmailLogin, subject, htmlBody, cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                "Falha ao enviar e-mail de primeiro acesso para {EmailLogin}.",
                usuario.EmailLogin);

            return Result.Failure("Não foi possível enviar o e-mail de primeiro acesso. Verifique a configuração de e-mail.");
        }

        _logger.LogInformation(
            "Convite de primeiro acesso enviado para {EmailLogin}. URL: {FirstAccessUrl}",
            usuario.EmailLogin,
            firstAccessUrl);

        return Result.Success();
    }

    private string BuildFirstAccessUrl(string rawToken)
    {
        var baseUrl = _settings.FrontendBaseUrl.TrimEnd('/');
        var path = string.IsNullOrWhiteSpace(_settings.Path) ? "/primeiro-acesso" : _settings.Path;

        if (!path.StartsWith('/'))
        {
            path = "/" + path;
        }

        return $"{baseUrl}{path}?token={Uri.EscapeDataString(rawToken)}";
    }
}
