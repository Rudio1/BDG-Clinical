using BGD.CLINICAL.Domain.Entities;

namespace BGD.CLINICAL.Application.Identity.Abstractions;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}
