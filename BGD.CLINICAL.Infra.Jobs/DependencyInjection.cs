using BGD.CLINICAL.Application.Notifications;
using BGD.CLINICAL.Infra.Jobs.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGD.CLINICAL.Infra.Jobs;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraJobs(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailOutboxSettings>(configuration.GetSection("EmailOutbox"));
        services.AddHostedService<EmailOutboxWorker>();

        return services;
    }
}
