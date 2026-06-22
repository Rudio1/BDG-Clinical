using BGD.CLINICAL.Application.Identity.Abstractions;
using BGD.CLINICAL.Application.Identity.Authentications;
using BGD.CLINICAL.Application.Identity.Registrations;
using BGD.CLINICAL.Application.Identity.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BGD.CLINICAL.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILoginUsersService, LoginUsersService>();
        services.AddScoped<IRegisterCompaniesService, RegisterCompaniesService>();
        services.AddScoped<IGetAuthenticatedUsersService, GetAuthenticatedUsersService>();

        return services;
    }
}
