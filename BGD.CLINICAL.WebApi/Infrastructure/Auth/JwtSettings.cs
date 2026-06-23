namespace BGD.CLINICAL.WebApi.Infrastructure.Auth;

public sealed class JwtSettings
{
    public string Secret { get; set; } = string.Empty;

    public string Issuer { get; set; } = "BGD.Clinical";

    public string Audience { get; set; } = "BGD.Clinical";

    public int ExpirationMinutes { get; set; } = 480;
}
