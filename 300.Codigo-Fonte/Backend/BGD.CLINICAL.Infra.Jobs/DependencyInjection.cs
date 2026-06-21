using BGD.CLINICAL.Infra.Jobs.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace BGD.CLINICAL.Infra.Jobs;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraJobs(this IServiceCollection services)
    {
        services.AddHostedService<JobWorker>();

        return services;
    }
}
