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
            usuario.TipoUsuario == TipoUsuario.Admin);
    }
}

internal static class IdentityValidation
{
    public const string CredenciaisInvalidas = IdentityConstants.CredenciaisInvalidas;

    public const string MultiplasContas = IdentityConstants.MultiplasContas;

    public const string PrimeiroAcessoPendente = IdentityConstants.PrimeiroAcessoPendente;

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
