using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qualifier.Application.Database;
using Qualifier.Domain.Interfaces;
using Qualifier.Persistence.Database;
using Qualifier.Persistence.Repositories;


namespace Qualifier.Persistence
{
    public static class DependencyInjectionService
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseService>(options =>
            options.UseNpgsql(configuration.GetConnectionString("SQLConnectionString")));     

            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<IStandardRepository, StandardRepository>();
            services.AddScoped<IIndicatorRepository, IndicatorRepository>();
            services.AddScoped<IMaturityLevelRepository, MaturityLevelRepository>();
            services.AddScoped<IControlGroupRepository, ControlGroupRepository>();
            services.AddScoped<IControlRepository, ControlRepository>();
            services.AddScoped<IRequirementRepository, RequirementRepository>();
            services.AddScoped<IResponsibleRepository, ResponsibleRepository>();
            services.AddScoped<IDocumentationRepository, DocumentationRepository>();
            services.AddScoped<IEvaluationRepository, EvaluationRepository>();
            services.AddScoped<IRequirementEvaluationRepository, RequirementEvaluationRepository>();
            services.AddScoped<IControlEvaluationRepository, ControlEvaluationRepository>();

            return services;
        }
    }
}
