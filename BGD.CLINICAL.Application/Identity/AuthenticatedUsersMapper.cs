using BGD.CLINICAL.Application.Identity.Dtos;
using BGD.CLINICAL.Domain.Entities;
using BGD.CLINICAL.Domain.Enums;
using System.Net.Mail;

namespace BGD.CLINICAL.Application.Identity;

internal static class AuthenticatedUsersMapper
{
    public static AuthenticatedUserDto Map(Usuario usuario)
    {
        return new AuthenticatedUserDto(
            usuario.Id,
            usuario.Nome,
            usuario.EmailLogin,
            GetPermissions(usuario));
    }

    private static IReadOnlyList<string> GetPermissions(Usuario usuario)
    {
        if (usuario.TipoUsuario == TipoUsuario.Admin)
        {
            return [IdentityConstants.PermissaoDashboardVisualizar];
        }

        return [];
    }
}

internal static class IdentityValidation
{
    public const string CredenciaisInvalidas = IdentityConstants.CredenciaisInvalidas;

    public const string MultiplasContas = IdentityConstants.MultiplasContas;

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            _ = new MailAddress(email.Trim());
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }
}
