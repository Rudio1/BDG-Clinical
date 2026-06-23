using BGD.CLINICAL.Infra.ExternalApis.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace BGD.CLINICAL.Infra.ExternalApis;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalApis(this IServiceCollection services)
    {
        services.AddHttpClient<ExternalApiClient>();

        return services;
    }
}
