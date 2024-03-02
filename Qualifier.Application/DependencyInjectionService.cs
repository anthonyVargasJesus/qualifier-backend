using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qualifier.Application.Configuration;
using Qualifier.Application.Database.Control.Commands.CreateControl;
using Qualifier.Application.Database.Control.Commands.DeleteControl;
using Qualifier.Application.Database.Control.Commands.UpdateControl;
using Qualifier.Application.Database.Control.Queries.GetControlById;
using Qualifier.Application.Database.Control.Queries.GetControlsByControlGroupId;
using Qualifier.Application.Database.ControlGroup.Commands.CreateControlGroup;
using Qualifier.Application.Database.ControlGroup.Commands.DeleteControlGroup;
using Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByStandardId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupById;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByStandardId;
using Qualifier.Application.Database.Indicator.Commands.CreateIndicator;
using Qualifier.Application.Database.Indicator.Commands.DeleteIndicator;
using Qualifier.Application.Database.Indicator.Commands.UpdateIndicator;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorById;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorsByCompanyId;
using Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Commands.DeleteMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Commands.UpdateMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelById;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelsByCompanyId;
using Qualifier.Application.Database.Requirement.Commands.CreateRequirement;
using Qualifier.Application.Database.Requirement.Commands.DeleteRequirement;
using Qualifier.Application.Database.Requirement.Commands.UpdateRequirement;
using Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementById;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId;
using Qualifier.Application.Database.Standard.Commands.CreateStandard;
using Qualifier.Application.Database.Standard.Commands.DeleteStandard;
using Qualifier.Application.Database.Standard.Commands.UpdateStandard;
using Qualifier.Application.Database.Standard.Queries.GetAllStandardsByCompanyId;
using Qualifier.Application.Database.Standard.Queries.GetStandardById;
using Qualifier.Application.Database.Standard.Queries.GetStandardsByCompanyId;
using Qualifier.Application.Database.User.Commands.Login;
using Qualifier.Application.Database.User.Queries.GetMenus;


namespace Qualifier.Application
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            services.AddSingleton(mapper.CreateMapper());

            //MaturityLevel
            services.AddTransient<IGetMaturityLevelByIdQuery, GetMaturityLevelByIdQuery>();
            services.AddTransient<ICreateMaturityLevelCommand, CreateMaturityLevelCommand>();
            services.AddTransient<IUpdateMaturityLevelCommand, UpdateMaturityLevelCommand>();
            services.AddTransient<IDeleteMaturityLevelCommand, DeleteMaturityLevelCommand>();
            services.AddTransient<IGetMaturityLevelsByCompanyIdQuery, GetMaturityLevelsByCompanyIdQuery>();

            //Indicator
            services.AddTransient<ICreateIndicatorCommand, CreateIndicatorCommand>();
            services.AddTransient<IUpdateIndicatorCommand, UpdateIndicatorCommand>();
            services.AddTransient<IDeleteIndicatorCommand, DeleteIndicatorCommand>();
            services.AddTransient<IGetIndicatorByIdQuery, GetIndicatorByIdQuery>();
            services.AddTransient<IGetIndicatorsByCompanyIdQuery, GetIndicatorsByCompanyIdQuery>();

            //User
            services.AddTransient<ILoginUserCommand, LoginUserCommand>();
            services.AddTransient<IGetMenusQuery, GetMenusQuery>();

            //Standard
            services.AddTransient<ICreateStandardCommand, CreateStandardCommand>();
            services.AddTransient<IUpdateStandardCommand, UpdateStandardCommand>();
            services.AddTransient<IDeleteStandardCommand, DeleteStandardCommand>();
            services.AddTransient<IGetStandardByIdQuery, GetStandardByIdQuery>();
            services.AddTransient<IGetStandardsByCompanyIdQuery, GetStandardsByCompanyIdQuery>();
            services.AddTransient<IGetAllStandardsByCompanyIdQuery, GetAllStandardsByCompanyIdQuery>();

            //ControlGroup
            services.AddTransient<ICreateControlGroupCommand, CreateControlGroupCommand>();
            services.AddTransient<IUpdateControlGroupCommand, UpdateControlGroupCommand>();
            services.AddTransient<IDeleteControlGroupCommand, DeleteControlGroupCommand>();
            services.AddTransient<IGetAllControlGroupsByCompanyIdQuery, GetAllControlGroupsByCompanyIdQuery>();
            services.AddTransient<IGetControlGroupsByCompanyIdQuery, GetControlGroupsByCompanyIdQuery>();
            services.AddTransient<IGetControlGroupByIdQuery, GetControlGroupByIdQuery>();
            services.AddTransient<IGetAllControlGroupsByStandardIdQuery, GetAllControlGroupsByStandardIdQuery>();

            //Control
            services.AddTransient<IUpdateControlCommand, UpdateControlCommand>();
            services.AddTransient<ICreateControlCommand, CreateControlCommand>();
            services.AddTransient<IGetControlsByControlGroupIdQuery, GetControlsByControlGroupIdQuery>();
            services.AddTransient<IGetControlByIdQuery, GetControlByIdQuery>();
            services.AddTransient<IDeleteControlCommand, DeleteControlCommand>();
            services.AddTransient<IGetControlGroupsByStandardIdQuery, GetControlGroupsByStandardIdQuery>();

            //Requirement
            services.AddTransient<ICreateRequirementCommand, CreateRequirementCommand>();
            services.AddTransient<IUpdateRequirementCommand, UpdateRequirementCommand>();
            services.AddTransient<IDeleteRequirementCommand, DeleteRequirementCommand>();
            services.AddTransient<IGetRequirementsByStandardIdQuery, GetRequirementsByStandardIdQuery>();
            services.AddTransient<IGetRequirementByIdQuery, GetRequirementByIdQuery>();
            services.AddTransient<IGetAllRequirementsByStandardIdQuery, GetAllRequirementsByStandardIdQuery>();

            return services;
        }
    }
}
