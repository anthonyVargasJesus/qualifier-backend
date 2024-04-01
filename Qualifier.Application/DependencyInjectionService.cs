using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qualifier.Application.Configuration;
using Qualifier.Application.Database.Control.Commands.CreateControl;
using Qualifier.Application.Database.Control.Commands.DeleteControl;
using Qualifier.Application.Database.Control.Commands.UpdateControl;
using Qualifier.Application.Database.Control.Queries.GetControlById;
using Qualifier.Application.Database.Control.Queries.GetControlsByControlGroupId;
using Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Commands.DeleteControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationById;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Application.Database.ControlGroup.Commands.CreateControlGroup;
using Qualifier.Application.Database.ControlGroup.Commands.DeleteControlGroup;
using Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByStandardId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupById;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByStandardId;
using Qualifier.Application.Database.Documentation.Commands.CreateDocumentation;
using Qualifier.Application.Database.Documentation.Commands.DeleteDocumentation;
using Qualifier.Application.Database.Documentation.Commands.UpdateDocumentation;
using Qualifier.Application.Database.Documentation.Queries.GetAllDocumentationsByStandardId;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationById;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByStandardId;
using Qualifier.Application.Database.Evaluation.Commands.CreateEvaluation;
using Qualifier.Application.Database.Evaluation.Commands.DeleteEvaluation;
using Qualifier.Application.Database.Evaluation.Commands.UpdateEvaluation;
using Qualifier.Application.Database.Evaluation.Queries.GetControlsDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationById;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId;
using Qualifier.Application.Database.Evaluation.Queries.GetExcelDashboard;
using Qualifier.Application.Database.Indicator.Commands.CreateIndicator;
using Qualifier.Application.Database.Indicator.Commands.DeleteIndicator;
using Qualifier.Application.Database.Indicator.Commands.UpdateIndicator;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorById;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorsByCompanyId;
using Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Commands.DeleteMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Commands.UpdateMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelById;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelsByCompanyId;
using Qualifier.Application.Database.Requirement.Commands.CreateRequirement;
using Qualifier.Application.Database.Requirement.Commands.DeleteRequirement;
using Qualifier.Application.Database.Requirement.Commands.UpdateRequirement;
using Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementById;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId;
using Qualifier.Application.Database.RequirementEvaluation.Commands.CreateRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Commands.DeleteRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Commands.UpdateRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationById;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationsByRequirementId;
using Qualifier.Application.Database.Responsible.Commands.CreateResponsible;
using Qualifier.Application.Database.Responsible.Commands.DeleteResponsible;
using Qualifier.Application.Database.Responsible.Commands.UpdateResponsible;
using Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId;
using Qualifier.Application.Database.Responsible.Queries.GetResponsibleById;
using Qualifier.Application.Database.Responsible.Queries.GetResponsiblesByStandardId;
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
            services.AddTransient<IGetAllMaturityLevelsByCompanyIdQuery, GetAllMaturityLevelsByCompanyIdQuery>();

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

            //Responsible
            services.AddTransient<IUpdateResponsibleCommand, UpdateResponsibleCommand>();
            services.AddTransient<ICreateResponsibleCommand, CreateResponsibleCommand>();
            services.AddTransient<IGetResponsiblesByStandardIdQuery, GetResponsiblesByStandardIdQuery>();
            services.AddTransient<IGetResponsibleByIdQuery, GetResponsibleByIdQuery>();
            services.AddTransient<IDeleteResponsibleCommand, DeleteResponsibleCommand>();
            services.AddTransient<IGetAllResponsiblesByStandardIdQuery, GetAllResponsiblesByStandardIdQuery>();

            //Documentation
            services.AddTransient<IGetDocumentationsByStandardIdQuery, GetDocumentationsByStandardIdQuery>();
            services.AddTransient<IGetDocumentationByIdQuery, GetDocumentationByIdQuery>();
            services.AddTransient<ICreateDocumentationCommand, CreateDocumentationCommand>();
            services.AddTransient<IDeleteDocumentationCommand, DeleteDocumentationCommand>();
            services.AddTransient<IUpdateDocumentationCommand, UpdateDocumentationCommand>();
            services.AddTransient<IGetAllDocumentationsByStandardIdQuery, GetAllDocumentationsByStandardIdQuery>();

            //Evaluation
            services.AddTransient<ICreateEvaluationCommand, CreateEvaluationCommand>();
            services.AddTransient<IUpdateEvaluationCommand, UpdateEvaluationCommand>();
            services.AddTransient<IDeleteEvaluationCommand, DeleteEvaluationCommand>();
            services.AddTransient<IGetEvaluationsByCompanyIdQuery, GetEvaluationsByCompanyIdQuery>();
            services.AddTransient<IGetEvaluationByIdQuery, GetEvaluationByIdQuery>();
            services.AddTransient<IGetDashboardQuery, GetDashboardQuery>();
            services.AddTransient<IGetControlsDashboardQuery, GetControlsDashboardQuery>();

            //RequirementEvaluation
            services.AddTransient<ICreateRequirementEvaluationCommand, CreateRequirementEvaluationCommand>();
            services.AddTransient<IUpdateRequirementEvaluationCommand, UpdateRequirementEvaluationCommand>();
            services.AddTransient<IDeleteRequirementEvaluationCommand, DeleteRequirementEvaluationCommand>();
            services.AddTransient<IGetRequirementEvaluationsByRequirementIdQuery, GetRequirementEvaluationsByRequirementIdQuery>();
            services.AddTransient<IGetRequirementEvaluationByIdQuery, GetRequirementEvaluationByIdQuery>();
            services.AddTransient<IGetRequirementEvaluationByProcessQuery, GetRequirementEvaluationByProcessQuery>();

            //ControlEvaluation
            services.AddTransient<IGetControlEvaluationByIdQuery, GetControlEvaluationByIdQuery>();
            services.AddTransient<IUpdateControlEvaluationCommand, UpdateControlEvaluationCommand>();
            services.AddTransient<IDeleteControlEvaluationCommand, DeleteControlEvaluationCommand>();
            services.AddTransient<ICreateControlEvaluationCommand, CreateControlEvaluationCommand>();
            services.AddTransient<IGetControlEvaluationByProcessQuery, GetControlEvaluationByProcessQuery>();

            //dashboard
            services.AddTransient<IGetExcelDashboardQuery, GetExcelDashboardQuery>();

            return services;
        }
    }
}
