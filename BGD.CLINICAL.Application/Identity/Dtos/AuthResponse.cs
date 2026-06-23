namespace BGD.CLINICAL.Application.Identity.Dtos;

public sealed record AuthResponse(string Token, AuthenticatedUserDto Usuario);
