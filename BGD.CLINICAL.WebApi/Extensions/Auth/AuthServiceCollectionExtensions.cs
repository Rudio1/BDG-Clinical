using BGD.CLINICAL.Application.Identity;
using BGD.CLINICAL.Application.Identity.Abstractions;
using BGD.CLINICAL.Domain.Enums;
using BGD.CLINICAL.WebApi.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BGD.CLINICAL.WebApi.Extensions.Auth;

public static class AuthServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        services.AddScoped<ITokenService, JwtTokensService>();
        services.AddScoped<IPasswordHashGenerator, BcryptPasswordHashGenerator>();

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>()
            ?? throw new InvalidOperationException("Configuração Jwt não encontrada.");

        if (string.IsNullOrWhiteSpace(jwtSettings.Secret))
        {
            throw new InvalidOperationException("Jwt:Secret não configurado.");
        }

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(IdentityConstants.PolicyAdmin, policy =>
                policy.RequireClaim(
                    IdentityConstants.ClaimTipoUsuario,
                    nameof(TipoUsuario.Admin)));
        });

        return services;
    }
}
