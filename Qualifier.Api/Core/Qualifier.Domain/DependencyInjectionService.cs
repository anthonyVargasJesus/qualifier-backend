using Microsoft.Extensions.DependencyInjection;
using pangolin.domain.services;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Domain
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {

            services.AddScoped<ILoginService, LoginService>();

            return services;
        }
    }
}
