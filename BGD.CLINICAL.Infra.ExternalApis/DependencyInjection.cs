using BGD.CLINICAL.Application.Abstractions.Notifications;
using BGD.CLINICAL.Application.Abstractions.Storage;
using BGD.CLINICAL.Application.Identity;
using BGD.CLINICAL.Infra.ExternalApis.Clients;
using BGD.CLINICAL.Infra.ExternalApis.Email;
using BGD.CLINICAL.Infra.ExternalApis.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGD.CLINICAL.Infra.ExternalApis;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalApis(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpClient<ExternalApiClient>();

        services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));
        services.Configure<FirstAccessSettings>(configuration.GetSection("FirstAccess"));
        services.ConfigureCloudflareR2(configuration);

        services.AddScoped<IEmailSender, SmtpEmailSender>();
        services.AddScoped<IObjectStorageService, CloudflareR2ObjectStorageService>();

        return services;
    }
}
