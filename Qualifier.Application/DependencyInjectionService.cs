using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qualifier.Application.Configuration;
using Qualifier.Application.Database.ConfidentialityLevel.Commands.CreateConfidentialityLevel;
using Qualifier.Application.Database.ConfidentialityLevel.Commands.DeleteConfidentialityLevel;
using Qualifier.Application.Database.ConfidentialityLevel.Commands.UpdateConfidentialityLevel;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetAllConfidentialityLevelsByCompanyId;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelById;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelsByCompanyId;
using Qualifier.Application.Database.Control.Commands.CreateControl;
using Qualifier.Application.Database.Control.Commands.DeleteControl;
using Qualifier.Application.Database.Control.Commands.UpdateControl;
using Qualifier.Application.Database.Control.Queries.GetAllControlsByStandardId;
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
using Qualifier.Application.Database.Creator.Commands.CreateCreator;
using Qualifier.Application.Database.Creator.Commands.DeleteCreator;
using Qualifier.Application.Database.Creator.Commands.UpdateCreator;
using Qualifier.Application.Database.Creator.Queries.GetCreatorById;
using Qualifier.Application.Database.Creator.Queries.GetAllCreatorsByVersionId;
using Qualifier.Application.Database.DefaultSection.Commands.CreateDefaultSection;
using Qualifier.Application.Database.DefaultSection.Commands.DeleteDefaultSection;
using Qualifier.Application.Database.DefaultSection.Commands.UpdateDefaultSection;
using Qualifier.Application.Database.DefaultSection.Queries.GetAllDefaultSectionsByDocumentTypeId;
using Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionById;
using Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionsByDocumentTypeId;
using Qualifier.Application.Database.Documentation.Commands.CreateDocumentation;
using Qualifier.Application.Database.Documentation.Commands.DeleteDocumentation;
using Qualifier.Application.Database.Documentation.Commands.UpdateDocumentation;
using Qualifier.Application.Database.Documentation.Queries.GetAllDocumentationsByStandardId;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationById;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByCompanyId;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByStandardId;
using Qualifier.Application.Database.DocumentType.Commands.CreateDocumentType;
using Qualifier.Application.Database.DocumentType.Commands.DeleteDocumentType;
using Qualifier.Application.Database.DocumentType.Commands.UpdateDocumentType;
using Qualifier.Application.Database.DocumentType.Queries.GetAllDocumentTypesByCompanyId;
using Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypeById;
using Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypesByCompanyId;
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
using Qualifier.Application.Database.Personal.Commands.CreatePersonal;
using Qualifier.Application.Database.Personal.Commands.DeletePersonal;
using Qualifier.Application.Database.Personal.Commands.UpdatePersonal;
using Qualifier.Application.Database.Personal.Queries.GetAllPersonalsByCompanyId;
using Qualifier.Application.Database.Personal.Queries.GetPersonalById;
using Qualifier.Application.Database.Personal.Queries.GetPersonalsByCompanyId;
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
using Qualifier.Application.Database.Reviewer.Commands.CreateReviewer;
using Qualifier.Application.Database.Reviewer.Commands.DeleteReviewer;
using Qualifier.Application.Database.Reviewer.Commands.UpdateReviewer;
using Qualifier.Application.Database.Reviewer.Queries.GetAllReviewersByVersionId;
using Qualifier.Application.Database.Reviewer.Queries.GetReviewerById;
using Qualifier.Application.Database.Section.Commands.CreateSection;
using Qualifier.Application.Database.Section.Commands.DeleteSection;
using Qualifier.Application.Database.Section.Commands.UpdateSection;
using Qualifier.Application.Database.Section.Queries.GetAllSectionsByVersionId;
using Qualifier.Application.Database.Section.Queries.GetSectionById;
using Qualifier.Application.Database.Section.Queries.GetSectionsByVersionId;
using Qualifier.Application.Database.Standard.Commands.CreateStandard;
using Qualifier.Application.Database.Standard.Commands.DeleteStandard;
using Qualifier.Application.Database.Standard.Commands.UpdateStandard;
using Qualifier.Application.Database.Standard.Queries.GetAllStandardsByCompanyId;
using Qualifier.Application.Database.Standard.Queries.GetStandardById;
using Qualifier.Application.Database.Standard.Queries.GetStandardsByCompanyId;
using Qualifier.Application.Database.SupportForControl.Commands.CreateSupportForControl;
using Qualifier.Application.Database.SupportForControl.Commands.DeleteSupportForControl;
using Qualifier.Application.Database.SupportForControl.Commands.UpdateSupportForControl;
using Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlById;
using Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlsByDocumentationId;
using Qualifier.Application.Database.SupportForRequirement.Commands.CreateSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Commands.DeleteSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Commands.UpdateSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementById;
using Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementsByDocumentationId;
using Qualifier.Application.Database.User.Commands.Login;
using Qualifier.Application.Database.User.Queries.GetMenus;
using Qualifier.Application.Database.Version.Commands.CreateVersion;
using Qualifier.Application.Database.Version.Commands.DeleteVersion;
using Qualifier.Application.Database.Version.Commands.UpdateVersion;
using Qualifier.Application.Database.Version.Queries.GetVersionById;
using Qualifier.Application.Database.Version.Queries.GetVersionsByDocumentationId;
using Qualifier.Application.Database.Approver.Commands.CreateApprover;
using Qualifier.Application.Database.Approver.Commands.UpdateApprover;
using Qualifier.Application.Database.Approver.Commands.DeleteApprover;
using Qualifier.Application.Database.Approver.Queries.GetApproverById;
using Qualifier.Application.Database.Approver.Queries.GetAllApproversByVersionId;
using Qualifier.Application.Database.Version.Commands.CreateWordDocumento;
using Qualifier.Application.Database.Macroprocess.Commands.CreateMacroprocess;
using Qualifier.Application.Database.Macroprocess.Commands.UpdateMacroprocess;
using Qualifier.Application.Database.Macroprocess.Commands.DeleteMacroprocess;
using Qualifier.Application.Database.Macroprocess.Queries.GetMacroprocessById;
using Qualifier.Application.Database.Macroprocess.Queries.GetAllMacroprocesssByCompanyId;
using Qualifier.Application.Database.Macroprocess.Queries.GetMacroprocesssByCompanyId;
using Qualifier.Application.Database.Subprocess.Commands.CreateSubprocess;
using Qualifier.Application.Database.Subprocess.Commands.UpdateSubprocess;
using Qualifier.Application.Database.Subprocess.Commands.DeleteSubprocess;
using Qualifier.Application.Database.Subprocess.Queries.GetSubprocesssByCompanyId;
using Qualifier.Application.Database.Subprocess.Queries.GetAllSubprocesssByCompanyId;
using Qualifier.Application.Database.Subprocess.Queries.GetSubprocessById;
using Qualifier.Application.Database.ActiveType.Commands.CreateActiveType;
using Qualifier.Application.Database.ActiveType.Commands.UpdateActiveType;
using Qualifier.Application.Database.ActiveType.Commands.DeleteActiveType;
using Qualifier.Application.Database.ActiveType.Queries.GetActiveTypeById;
using Qualifier.Application.Database.ActiveType.Queries.GetAllActiveTypesByCompanyId;
using Qualifier.Application.Database.ActiveType.Queries.GetActiveTypesByCompanyId;
using Qualifier.Application.Database.Owner.Commands.CreateOwner;
using Qualifier.Application.Database.Owner.Commands.UpdateOwner;
using Qualifier.Application.Database.Owner.Queries.GetOwnerById;
using Qualifier.Application.Database.Owner.Queries.GetAllOwnersByCompanyId;
using Qualifier.Application.Database.Owner.Queries.GetOwnersByCompanyId;
using Qualifier.Application.Database.Owner.Commands.DeleteOwner;
using Qualifier.Application.Database.Custodian.Commands.CreateCustodian;
using Qualifier.Application.Database.Custodian.Commands.UpdateCustodian;
using Qualifier.Application.Database.Custodian.Commands.DeleteCustodian;
using Qualifier.Application.Database.Custodian.Queries.GetCustodianById;
using Qualifier.Application.Database.Custodian.Queries.GetAllCustodiansByCompanyId;
using Qualifier.Application.Database.Custodian.Queries.GetCustodiansByCompanyId;
using Qualifier.Application.Database.UsageClassification.Commands.DeleteUsageClassification;
using Qualifier.Application.Database.UsageClassification.Commands.CreateUsageClassification;
using Qualifier.Application.Database.UsageClassification.Commands.UpdateUsageClassification;
using Qualifier.Application.Database.UsageClassification.Queries.GetUsageClassificationById;
using Qualifier.Application.Database.UsageClassification.Queries.GetAllUsageClassificationsByCompanyId;
using Qualifier.Application.Database.UsageClassification.Queries.GetUsageClassificationsByCompanyId;
using Qualifier.Application.Database.SupportType.Commands.CreateSupportType;
using Qualifier.Application.Database.SupportType.Commands.DeleteSupportType;
using Qualifier.Application.Database.SupportType.Commands.UpdateSupportType;
using Qualifier.Application.Database.SupportType.Queries.GetSupportTypeById;
using Qualifier.Application.Database.SupportType.Queries.GetAllSupportTypesByCompanyId;
using Qualifier.Application.Database.SupportType.Queries.GetSupportTypesByCompanyId;
using Qualifier.Application.Database.Location.Commands.CreateLocation;
using Qualifier.Application.Database.Location.Commands.DeleteLocation;
using Qualifier.Application.Database.Location.Commands.UpdateLocation;
using Qualifier.Application.Database.Location.Queries.GetAllLocationsByCompanyId;
using Qualifier.Application.Database.Location.Queries.GetLocationById;
using Qualifier.Application.Database.Location.Queries.GetLocationsByCompanyId;
using Qualifier.Application.Database.ImpactValuation.Commands.CreateImpactValuation;
using Qualifier.Application.Database.ImpactValuation.Commands.DeleteImpactValuation;
using Qualifier.Application.Database.ImpactValuation.Commands.UpdateImpactValuation;
using Qualifier.Application.Database.ImpactValuation.Queries.GetAllImpactValuationsByCompanyId;
using Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationById;
using Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationsByCompanyId;
using Qualifier.Application.Database.ActivesInventory.Commands.CreateActivesInventory;
using Qualifier.Application.Database.ActivesInventory.Commands.DeleteActivesInventory;
using Qualifier.Application.Database.ActivesInventory.Commands.UpdateActivesInventory;
using Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoryById;
using Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoriesByCompanyId;
using Qualifier.Application.Database.ValuationInActive.Commands.CreateValuationInActive;
using Qualifier.Application.Database.ValuationInActive.Commands.UpdateValuationInActive;
using Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActivesByActivesInventoryId;
using Qualifier.Application.Database.ValuationInActive.Commands.DeleteValuationInActive;
using Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActiveById;
using Qualifier.Application.Database.ValuationInActive.Queries.GetAllValuationInActivesByCompanyId;
using Qualifier.Application.Database.Option.Commands.CreateOption;
using Qualifier.Application.Database.Option.Commands.UpdateOption;
using Qualifier.Application.Database.Option.Queries.GetOptionById;
using Qualifier.Application.Database.Option.Queries.GetAllOptionsByCompanyId;
using Qualifier.Application.Database.Option.Queries.GetOptionsByCompanyId;
using Qualifier.Application.Database.Option.Commands.DeleteOption;
using Qualifier.Application.Database.Menu.Commands.CreateMenu;
using Qualifier.Application.Database.Menu.Commands.DeleteMenu;
using Qualifier.Application.Database.Menu.Commands.UpdateMenu;
using Qualifier.Application.Database.Menu.Queries.GetAllMenusByCompanyId;
using Qualifier.Application.Database.Menu.Queries.GetMenuById;
using Qualifier.Application.Database.Menu.Queries.GetMenusByCompanyId;
using Qualifier.Application.Database.Role.Commands.CreateRole;
using Qualifier.Application.Database.Role.Commands.DeleteRole;
using Qualifier.Application.Database.Role.Commands.UpdateRole;
using Qualifier.Application.Database.Role.Queries.GetAllRolesByCompanyId;
using Qualifier.Application.Database.Role.Queries.GetRoleById;
using Qualifier.Application.Database.Role.Queries.GetRolesByCompanyId;
using Qualifier.Application.Database.MenuInRole.Commands.CreateMenuInRole;
using Qualifier.Application.Database.MenuInRole.Commands.DeleteMenuInRole;
using Qualifier.Application.Database.MenuInRole.Commands.UpdateMenuInRole;
using Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRoleById;
using Qualifier.Application.Database.MenuInRole.Queries.GetAllMenuInRolesByRoleId;
using Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRolesByRoleId;
using Qualifier.Application.Database.OptionInMenu.Commands.CreateOptionInMenu;
using Qualifier.Application.Database.OptionInMenu.Commands.DeleteOptionInMenu;
using Qualifier.Application.Database.OptionInMenu.Commands.UpdateOptionInMenu;
using Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenuById;
using Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenusByMenuId;
using Qualifier.Application.Database.Menu.Queries.GetAllMenusByRoleId;
using Qualifier.Application.Database.OptionInMenuInRole.Commands.CreateOptionInMenuInRole;
using Qualifier.Application.Database.OptionInMenuInRole.Commands.DeleteOptionInMenuInRole;
using Qualifier.Application.Database.UserState.Commands.CreateUserState;
using Qualifier.Application.Database.UserState.Commands.UpdateUserState;
using Qualifier.Application.Database.UserState.Queries.GetUserStateById;
using Qualifier.Application.Database.UserState.Queries.GetAllUserStatesByCompanyId;
using Qualifier.Application.Database.UserState.Queries.GetUserStatesByCompanyId;
using Qualifier.Application.Database.UserState.Commands.DeleteUserState;
using Qualifier.Application.Database.User.Commands.CreateUser;
using Qualifier.Application.Database.User.Commands.UpdateUser;
using Qualifier.Application.Database.User.Commands.DeleteUser;
using Qualifier.Application.Database.User.Queries.GetUserById;
using Qualifier.Application.Database.User.Queries.GetAllUsersByCompanyId;
using Qualifier.Application.Database.User.Queries.GetUsersByCompanyId;
using Qualifier.Application.Database.RoleInUser.Commands.CreateRoleInUser;
using Qualifier.Application.Database.RoleInUser.Commands.UpdateRoleInUser;
using Qualifier.Application.Database.RoleInUser.Commands.DeleteRoleInUser;
using Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUserById;
using Qualifier.Application.Database.RoleInUser.Queries.GetAllRoleInUsersByUserId;
using Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUsersByUserId;
using Qualifier.Application.Database.MenaceType.Commands.CreateMenaceType;
using Qualifier.Application.Database.MenaceType.Commands.UpdateMenaceType;
using Qualifier.Application.Database.MenaceType.Commands.DeleteMenaceType;
using Qualifier.Application.Database.MenaceType.Queries.GetMenaceTypeById;
using Qualifier.Application.Database.MenaceType.Queries.GetAllMenaceTypesByCompanyId;
using Qualifier.Application.Database.MenaceType.Queries.GetMenaceTypesByCompanyId;
using Qualifier.Application.Database.Menace.Commands.CreateMenace;
using Qualifier.Application.Database.Menace.Commands.UpdateMenace;
using Qualifier.Application.Database.Menace.Commands.DeleteMenace;
using Qualifier.Application.Database.Menace.Queries.GetMenaceById;
using Qualifier.Application.Database.Menace.Queries.GetAllMenacesByCompanyId;
using Qualifier.Application.Database.Menace.Queries.GetMenacesByCompanyId;
using Qualifier.Application.Database.VulnerabilityType.Commands.CreateVulnerabilityType;
using Qualifier.Application.Database.VulnerabilityType.Commands.UpdateVulnerabilityType;
using Qualifier.Application.Database.VulnerabilityType.Commands.DeleteVulnerabilityType;
using Qualifier.Application.Database.VulnerabilityType.Queries.GetVulnerabilityTypeById;
using Qualifier.Application.Database.VulnerabilityType.Queries.GetAllVulnerabilityTypesByCompanyId;
using Qualifier.Application.Database.VulnerabilityType.Queries.GetVulnerabilityTypesByCompanyId;
using Qualifier.Application.Database.Vulnerability.Commands.CreateVulnerability;
using Qualifier.Application.Database.Vulnerability.Commands.UpdateVulnerability;
using Qualifier.Application.Database.Vulnerability.Commands.DeleteVulnerability;
using Qualifier.Application.Database.Vulnerability.Queries.GetVulnerabilityById;
using Qualifier.Application.Database.Vulnerability.Queries.GetAllVulnerabilitiesByCompanyId;
using Qualifier.Application.Database.Vulnerability.Queries.GetVulnerabilitiesByCompanyId;
using Qualifier.Application.Database.ControlType.Commands.CreateControlType;
using Qualifier.Application.Database.ControlType.Commands.DeleteControlType;
using Qualifier.Application.Database.ControlType.Commands.UpdateControlType;
using Qualifier.Application.Database.ControlType.Queries.GetAllControlTypesByCompanyId;
using Qualifier.Application.Database.ControlType.Queries.GetControlTypeById;
using Qualifier.Application.Database.ControlType.Queries.GetControlTypesByCompanyId;
using Qualifier.Application.Database.RiskLevel.Commands.CreateRiskLevel;
using Qualifier.Application.Database.RiskLevel.Commands.DeleteRiskLevel;
using Qualifier.Application.Database.RiskLevel.Commands.UpdateRiskLevel;
using Qualifier.Application.Database.RiskLevel.Queries.GetAllRiskLevelsByCompanyId;
using Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelById;
using Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelsByCompanyId;
using Qualifier.Application.Database.Risk.Commands.CreateRisk;
using Qualifier.Application.Database.Risk.Commands.UpdateRisk;
using Qualifier.Application.Database.Risk.Commands.DeleteRisk;
using Qualifier.Application.Database.Risk.Queries.GetRiskById;
using Qualifier.Application.Database.Risk.Queries.GetRisksByCompanyId;
using Qualifier.Application.Database.Company.Queries.GetCompanyById;
using Qualifier.Application.Database.Company.Commands.UpdateCompany;
using Qualifier.Application.Database.Evaluation.Queries.GetAllEvaluationsByCompanyId;
using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;



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
            services.AddTransient<IGetAllEvaluationsByCompanyIdQuery, GetAllEvaluationsByCompanyIdQuery>();
            services.AddTransient<IGetCurrentEvaluationQuery, GetCurrentEvaluationQuery>();

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

            //DocumentType
            services.AddTransient<ICreateDocumentTypeCommand, CreateDocumentTypeCommand>();
            services.AddTransient<IUpdateDocumentTypeCommand, UpdateDocumentTypeCommand>();
            services.AddTransient<IDeleteDocumentTypeCommand, DeleteDocumentTypeCommand>();
            services.AddTransient<IGetDocumentTypeByIdQuery, GetDocumentTypeByIdQuery>();
            services.AddTransient<IGetDocumentTypesByCompanyIdQuery, GetDocumentTypesByCompanyIdQuery>();
            services.AddTransient<IGetAllDocumentTypesByCompanyIdQuery, GetAllDocumentTypesByCompanyIdQuery>();
            
            //DefaultSection
            services.AddTransient<ICreateDefaultSectionCommand, CreateDefaultSectionCommand>();
            services.AddTransient<IUpdateDefaultSectionCommand, UpdateDefaultSectionCommand>();
            services.AddTransient<IDeleteDefaultSectionCommand, DeleteDefaultSectionCommand>();
            services.AddTransient<IGetDefaultSectionByIdQuery, GetDefaultSectionByIdQuery>();
            services.AddTransient<IGetAllDefaultSectionsByDocumentTypeIdQuery, GetAllDefaultSectionsByDocumentTypeIdQuery>();
            services.AddTransient<IGetDefaultSectionsByDocumentTypeIdQuery, GetDefaultSectionsByDocumentTypeIdQuery>();
            services.AddTransient<IGetDocumentationsByCompanyIdQuery, GetDocumentationsByCompanyIdQuery>();

            //ConfidentialityLevel
            services.AddTransient<ICreateConfidentialityLevelCommand, CreateConfidentialityLevelCommand>();
            services.AddTransient<IUpdateConfidentialityLevelCommand, UpdateConfidentialityLevelCommand>();
            services.AddTransient<IDeleteConfidentialityLevelCommand, DeleteConfidentialityLevelCommand>();
            services.AddTransient<IGetConfidentialityLevelByIdQuery, GetConfidentialityLevelByIdQuery>();
            services.AddTransient<IGetAllConfidentialityLevelsByCompanyIdQuery, GetAllConfidentialityLevelsByCompanyIdQuery>();
            services.AddTransient<IGetConfidentialityLevelsByCompanyIdQuery, GetConfidentialityLevelsByCompanyIdQuery>();

            //Version
            services.AddTransient<ICreateVersionCommand, CreateVersionCommand>();
            services.AddTransient<IUpdateVersionCommand, UpdateVersionCommand>();
            services.AddTransient<IDeleteVersionCommand, DeleteVersionCommand>();
            services.AddTransient<IGetVersionByIdQuery, GetVersionByIdQuery>();
            services.AddTransient<IGetVersionsByDocumentationIdQuery, GetVersionsByDocumentationIdQuery>();

            //SupportForRequirement
            services.AddTransient<ICreateSupportForRequirementCommand, CreateSupportForRequirementCommand>();
            services.AddTransient<IUpdateSupportForRequirementCommand, UpdateSupportForRequirementCommand>();
            services.AddTransient<IGetSupportForRequirementByIdQuery, GetSupportForRequirementByIdQuery>();
            services.AddTransient<IGetSupportForRequirementsByDocumentationIdQuery, GetSupportForRequirementsByDocumentationIdQuery>();
            services.AddTransient<IDeleteSupportForRequirementCommand, DeleteSupportForRequirementCommand>();

            //SupportForControl
            services.AddTransient<ICreateSupportForControlCommand, CreateSupportForControlCommand>();
            services.AddTransient<IUpdateSupportForControlCommand, UpdateSupportForControlCommand>();
            services.AddTransient<IDeleteSupportForControlCommand, DeleteSupportForControlCommand>();
            services.AddTransient<IGetSupportForControlByIdQuery, GetSupportForControlByIdQuery>();
            services.AddTransient<IGetSupportForControlsByDocumentationIdQuery, GetSupportForControlsByDocumentationIdQuery>();

            services.AddTransient<IGetAllControlsByStandardIdQuery, GetAllControlsByStandardIdQuery>();
                   
            //Section
            services.AddTransient<ICreateSectionCommand, CreateSectionCommand>();
            services.AddTransient<IUpdateSectionCommand, UpdateSectionCommand>();
            services.AddTransient<IDeleteSectionCommand, DeleteSectionCommand>();
            services.AddTransient<IGetSectionByIdQuery, GetSectionByIdQuery>();
            services.AddTransient<IGetAllSectionsByVersionIdQuery, GetAllSectionsByVersionIdQuery>();
            services.AddTransient<IGetSectionsByVersionIdQuery, GetSectionsByVersionIdQuery>();

            //Personal
            services.AddTransient<ICreatePersonalCommand, CreatePersonalCommand>();
            services.AddTransient<IUpdatePersonalCommand, UpdatePersonalCommand>();
            services.AddTransient<IDeletePersonalCommand, DeletePersonalCommand>();
            services.AddTransient<IGetPersonalByIdQuery, GetPersonalByIdQuery>();
            services.AddTransient<IGetAllPersonalsByCompanyIdQuery, GetAllPersonalsByCompanyIdQuery>();
            services.AddTransient<IGetPersonalsByCompanyIdQuery, GetPersonalsByCompanyIdQuery>();

            //Creator
            services.AddTransient<ICreateCreatorCommand, CreateCreatorCommand>();
            services.AddTransient<IUpdateCreatorCommand, UpdateCreatorCommand>();
            services.AddTransient<IDeleteCreatorCommand, DeleteCreatorCommand>();
            services.AddTransient<IGetAllCreatorsByVersionIdQuery, GetAllCreatorsByVersionIdQuery>();
            services.AddTransient<IGetCreatorByIdQuery, GetCreatorByIdQuery>();

            //Reviewer
            services.AddTransient<ICreateReviewerCommand, CreateReviewerCommand>();
            services.AddTransient<IUpdateReviewerCommand, UpdateReviewerCommand>();
            services.AddTransient<IDeleteReviewerCommand, DeleteReviewerCommand>();
            services.AddTransient<IGetReviewerByIdQuery, GetReviewerByIdQuery>();
            services.AddTransient<IGetAllReviewersByVersionIdQuery, GetAllReviewersByVersionIdQuery>();

            //Approver
            services.AddTransient<ICreateApproverCommand, CreateApproverCommand>();
            services.AddTransient<IUpdateApproverCommand, UpdateApproverCommand>();
            services.AddTransient<IDeleteApproverCommand, DeleteApproverCommand>();
            services.AddTransient<IGetApproverByIdQuery, GetApproverByIdQuery>();
            services.AddTransient<IGetAllApproversByVersionIdQuery, GetAllApproversByVersionIdQuery>();
            services.AddTransient<ICreateWordDocumentCommand, CreateWordDocumentCommand>();

            //Macroprocess
            services.AddTransient<ICreateMacroprocessCommand, CreateMacroprocessCommand>();
            services.AddTransient<IUpdateMacroprocessCommand, UpdateMacroprocessCommand>();
            services.AddTransient<IDeleteMacroprocessCommand, DeleteMacroprocessCommand>();
            services.AddTransient<IGetMacroprocessByIdQuery, GetMacroprocessByIdQuery>();
            services.AddTransient<IGetAllMacroprocesssByCompanyIdQuery, GetAllMacroprocesssByCompanyIdQuery>();
            services.AddTransient<IGetMacroprocesssByCompanyIdQuery, GetMacroprocesssByCompanyIdQuery>();

            //Subprocess
            services.AddTransient<ICreateSubprocessCommand, CreateSubprocessCommand>();
            services.AddTransient<IUpdateSubprocessCommand, UpdateSubprocessCommand>();
            services.AddTransient<IDeleteSubprocessCommand, DeleteSubprocessCommand>();
            services.AddTransient<IGetSubprocesssByCompanyIdQuery, GetSubprocesssByCompanyIdQuery>();
            services.AddTransient<IGetAllSubprocesssByCompanyIdQuery, GetAllSubprocesssByCompanyIdQuery>();
            services.AddTransient<IGetSubprocessByIdQuery, GetSubprocessByIdQuery>();

            //ActiveType
            services.AddTransient<ICreateActiveTypeCommand, CreateActiveTypeCommand>();
            services.AddTransient<IUpdateActiveTypeCommand, UpdateActiveTypeCommand>();
            services.AddTransient<IDeleteActiveTypeCommand, DeleteActiveTypeCommand>();
            services.AddTransient<IGetActiveTypeByIdQuery, GetActiveTypeByIdQuery>();
            services.AddTransient<IGetAllActiveTypesByCompanyIdQuery, GetAllActiveTypesByCompanyIdQuery>();
            services.AddTransient<IGetActiveTypesByCompanyIdQuery, GetActiveTypesByCompanyIdQuery>();

            //Owner
            services.AddTransient<ICreateOwnerCommand, CreateOwnerCommand>();
            services.AddTransient<IUpdateOwnerCommand, UpdateOwnerCommand>();
            services.AddTransient<IGetOwnerByIdQuery, GetOwnerByIdQuery>();
            services.AddTransient<IDeleteOwnerCommand, DeleteOwnerCommand>();
            services.AddTransient<IGetAllOwnersByCompanyIdQuery, GetAllOwnersByCompanyIdQuery>();
            services.AddTransient<IGetOwnersByCompanyIdQuery, GetOwnersByCompanyIdQuery>();

            //Custodian
            services.AddTransient<ICreateCustodianCommand, CreateCustodianCommand>();
            services.AddTransient<IUpdateCustodianCommand, UpdateCustodianCommand>();
            services.AddTransient<IDeleteCustodianCommand, DeleteCustodianCommand>();
            services.AddTransient<IGetCustodianByIdQuery, GetCustodianByIdQuery>();
            services.AddTransient<IGetAllCustodiansByCompanyIdQuery, GetAllCustodiansByCompanyIdQuery>();
            services.AddTransient<IGetCustodiansByCompanyIdQuery, GetCustodiansByCompanyIdQuery>();

            //UsageClassification
            services.AddTransient<ICreateUsageClassificationCommand, CreateUsageClassificationCommand>();
            services.AddTransient<IDeleteUsageClassificationCommand, DeleteUsageClassificationCommand>();
            services.AddTransient<IUpdateUsageClassificationCommand, UpdateUsageClassificationCommand>();
            services.AddTransient<IGetUsageClassificationByIdQuery, GetUsageClassificationByIdQuery>();
            services.AddTransient<IGetAllUsageClassificationsByCompanyIdQuery, GetAllUsageClassificationsByCompanyIdQuery>();
            services.AddTransient<IGetUsageClassificationsByCompanyIdQuery, GetUsageClassificationsByCompanyIdQuery>();

            //SupportType
            services.AddTransient<ICreateSupportTypeCommand, CreateSupportTypeCommand>();
            services.AddTransient<IUpdateSupportTypeCommand, UpdateSupportTypeCommand>();
            services.AddTransient<IDeleteSupportTypeCommand, DeleteSupportTypeCommand>();
            services.AddTransient<IGetSupportTypeByIdQuery, GetSupportTypeByIdQuery>();
            services.AddTransient<IGetAllSupportTypesByCompanyIdQuery, GetAllSupportTypesByCompanyIdQuery>();
            services.AddTransient<IGetSupportTypesByCompanyIdQuery, GetSupportTypesByCompanyIdQuery>();

            //Location
            services.AddTransient<ICreateLocationCommand, CreateLocationCommand>();
            services.AddTransient<IUpdateLocationCommand, UpdateLocationCommand>();
            services.AddTransient<IDeleteLocationCommand, DeleteLocationCommand>();
            services.AddTransient<IGetLocationByIdQuery, GetLocationByIdQuery>();
            services.AddTransient<IGetAllLocationsByCompanyIdQuery, GetAllLocationsByCompanyIdQuery>();
            services.AddTransient<IGetLocationsByCompanyIdQuery, GetLocationsByCompanyIdQuery>();

            //ImpactValuation
            services.AddTransient<ICreateImpactValuationCommand, CreateImpactValuationCommand>();
            services.AddTransient<IUpdateImpactValuationCommand, UpdateImpactValuationCommand>();
            services.AddTransient<IDeleteImpactValuationCommand, DeleteImpactValuationCommand>();
            services.AddTransient<IGetImpactValuationByIdQuery, GetImpactValuationByIdQuery>();
            services.AddTransient<IGetAllImpactValuationsByCompanyIdQuery, GetAllImpactValuationsByCompanyIdQuery>();
            services.AddTransient<IGetImpactValuationsByCompanyIdQuery, GetImpactValuationsByCompanyIdQuery>();

            //ActivesInventory
            services.AddTransient<ICreateActivesInventoryCommand, CreateActivesInventoryCommand>();
            services.AddTransient<IUpdateActivesInventoryCommand, UpdateActivesInventoryCommand>();
            services.AddTransient<IDeleteActivesInventoryCommand, DeleteActivesInventoryCommand>();
            services.AddTransient<IGetActivesInventoryByIdQuery, GetActivesInventoryByIdQuery>();
            services.AddTransient<IGetActivesInventoriesByCompanyIdQuery, GetActivesInventoriesByCompanyIdQuery>();

            //ValuationInActive
            services.AddTransient<ICreateValuationInActiveCommand, CreateValuationInActiveCommand>();
            services.AddTransient<IUpdateValuationInActiveCommand, UpdateValuationInActiveCommand>();
            services.AddTransient<IDeleteValuationInActiveCommand, DeleteValuationInActiveCommand>();
            services.AddTransient<IGetValuationInActiveByIdQuery, GetValuationInActiveByIdQuery>();
            services.AddTransient<IGetAllValuationInActivesByCompanyIdQuery, GetAllValuationInActivesByCompanyIdQuery>();
            services.AddTransient<IGetValuationInActivesByActivesInventoryIdQuery, GetValuationInActivesByActivesInventoryIdQuery>();

            //Option
            services.AddTransient<ICreateOptionCommand, CreateOptionCommand>();
            services.AddTransient<IUpdateOptionCommand, UpdateOptionCommand>();
            services.AddTransient<IDeleteOptionCommand, DeleteOptionCommand>();
            services.AddTransient<IGetOptionByIdQuery, GetOptionByIdQuery>();
            services.AddTransient<IGetAllOptionsByCompanyIdQuery, GetAllOptionsByCompanyIdQuery>();
            services.AddTransient<IGetOptionsByCompanyIdQuery, GetOptionsByCompanyIdQuery>();

            //Menu
            services.AddTransient<ICreateMenuCommand, CreateMenuCommand>();
            services.AddTransient<IUpdateMenuCommand, UpdateMenuCommand>();
            services.AddTransient<IDeleteMenuCommand, DeleteMenuCommand>();
            services.AddTransient<IGetMenuByIdQuery, GetMenuByIdQuery>();
            services.AddTransient<IGetAllMenusByCompanyIdQuery, GetAllMenusByCompanyIdQuery>();
            services.AddTransient<IGetMenusByCompanyIdQuery, GetMenusByCompanyIdQuery>();

            //Role
            services.AddTransient<ICreateRoleCommand, CreateRoleCommand>();
            services.AddTransient<IUpdateRoleCommand, UpdateRoleCommand>();
            services.AddTransient<IDeleteRoleCommand, DeleteRoleCommand>();
            services.AddTransient<IGetRoleByIdQuery, GetRoleByIdQuery>();
            services.AddTransient<IGetAllRolesByCompanyIdQuery, GetAllRolesByCompanyIdQuery>();
            services.AddTransient<IGetRolesByCompanyIdQuery, GetRolesByCompanyIdQuery>();

            //MenuInRole
            services.AddTransient<ICreateMenuInRoleCommand, CreateMenuInRoleCommand>();
            services.AddTransient<IUpdateMenuInRoleCommand, UpdateMenuInRoleCommand>();
            services.AddTransient<IDeleteMenuInRoleCommand, DeleteMenuInRoleCommand>();
            services.AddTransient<IGetMenuInRoleByIdQuery, GetMenuInRoleByIdQuery>();
            services.AddTransient<IGetAllMenuInRolesByRoleIdQuery, GetAllMenuInRolesByRoleIdQuery>();

            //OptionInMenu
            services.AddTransient<ICreateOptionInMenuCommand, CreateOptionInMenuCommand>();
            services.AddTransient<IUpdateOptionInMenuCommand, UpdateOptionInMenuCommand>();
            services.AddTransient<IDeleteOptionInMenuCommand, DeleteOptionInMenuCommand>();
            services.AddTransient<IGetOptionInMenuByIdQuery, GetOptionInMenuByIdQuery>();
            services.AddTransient<IGetOptionInMenusByMenuIdQuery, GetOptionInMenusByMenuIdQuery>();
            services.AddTransient<IGetMenuInRolesByRoleIdQuery, GetMenuInRolesByRoleIdQuery>();

            services.AddTransient<IGetAllMenusByRoleIdQuery, GetAllMenusByRoleIdQuery>();
            
            //OptionInMenuInRole
            services.AddTransient<ICreateOptionInMenuInRoleCommand, CreateOptionInMenuInRoleCommand>();
            services.AddTransient<IDeleteOptionInMenuInRoleCommand, DeleteOptionInMenuInRoleCommand>();

            //UserState
            services.AddTransient<ICreateUserStateCommand, CreateUserStateCommand>();
            services.AddTransient<IUpdateUserStateCommand, UpdateUserStateCommand>();
            services.AddTransient<IDeleteUserStateCommand, DeleteUserStateCommand>();
            services.AddTransient<IGetUserStateByIdQuery, GetUserStateByIdQuery>();
            services.AddTransient<IGetAllUserStatesByCompanyIdQuery, GetAllUserStatesByCompanyIdQuery>();
            services.AddTransient<IGetUserStatesByCompanyIdQuery, GetUserStatesByCompanyIdQuery>();

            //User
            services.AddTransient<ICreateUserCommand, CreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, UpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();
            services.AddTransient<IGetUserByIdQuery, GetUserByIdQuery>();
            services.AddTransient<IGetAllUsersByCompanyIdQuery, GetAllUsersByCompanyIdQuery>();
            services.AddTransient<IGetUsersByCompanyIdQuery, GetUsersByCompanyIdQuery>();

            //RoleInUser
            services.AddTransient<ICreateRoleInUserCommand, CreateRoleInUserCommand>();
            services.AddTransient<IUpdateRoleInUserCommand, UpdateRoleInUserCommand>();
            services.AddTransient<IDeleteRoleInUserCommand, DeleteRoleInUserCommand>();
            services.AddTransient<IGetRoleInUserByIdQuery, GetRoleInUserByIdQuery>();
            services.AddTransient<IGetAllRoleInUsersByUserIdQuery, GetAllRoleInUsersByUserIdQuery>();
            services.AddTransient<IGetRoleInUsersByUserIdQuery, GetRoleInUsersByUserIdQuery>();

            //MenaceType
            services.AddTransient<ICreateMenaceTypeCommand, CreateMenaceTypeCommand>();
            services.AddTransient<IUpdateMenaceTypeCommand, UpdateMenaceTypeCommand>();
            services.AddTransient<IDeleteMenaceTypeCommand, DeleteMenaceTypeCommand>();
            services.AddTransient<IGetMenaceTypeByIdQuery, GetMenaceTypeByIdQuery>();
            services.AddTransient<IGetAllMenaceTypesByCompanyIdQuery, GetAllMenaceTypesByCompanyIdQuery>();
            services.AddTransient<IGetMenaceTypesByCompanyIdQuery, GetMenaceTypesByCompanyIdQuery>();

            //Menace
            services.AddTransient<ICreateMenaceCommand, CreateMenaceCommand>();
            services.AddTransient<IUpdateMenaceCommand, UpdateMenaceCommand>();
            services.AddTransient<IDeleteMenaceCommand, DeleteMenaceCommand>();
            services.AddTransient<IGetMenaceByIdQuery, GetMenaceByIdQuery>();
            services.AddTransient<IGetAllMenacesByCompanyIdQuery, GetAllMenacesByCompanyIdQuery>();
            services.AddTransient<IGetMenacesByCompanyIdQuery, GetMenacesByCompanyIdQuery>();

            //VulnerabilityType
            services.AddTransient<ICreateVulnerabilityTypeCommand, CreateVulnerabilityTypeCommand>();
            services.AddTransient<IUpdateVulnerabilityTypeCommand, UpdateVulnerabilityTypeCommand>();
            services.AddTransient<IDeleteVulnerabilityTypeCommand, DeleteVulnerabilityTypeCommand>();
            services.AddTransient<IGetVulnerabilityTypeByIdQuery, GetVulnerabilityTypeByIdQuery>();
            services.AddTransient<IGetAllVulnerabilityTypesByCompanyIdQuery, GetAllVulnerabilityTypesByCompanyIdQuery>();
            services.AddTransient<IGetVulnerabilityTypesByCompanyIdQuery, GetVulnerabilityTypesByCompanyIdQuery>();

            //Vulnerability
            services.AddTransient<ICreateVulnerabilityCommand, CreateVulnerabilityCommand>();
            services.AddTransient<IUpdateVulnerabilityCommand, UpdateVulnerabilityCommand>();
            services.AddTransient<IDeleteVulnerabilityCommand, DeleteVulnerabilityCommand>();
            services.AddTransient<IGetVulnerabilityByIdQuery, GetVulnerabilityByIdQuery>();
            services.AddTransient<IGetAllVulnerabilitiesByCompanyIdQuery, GetAllVulnerabilitiesByCompanyIdQuery>();
            services.AddTransient<IGetVulnerabilitiesByCompanyIdQuery, GetVulnerabilitiesByCompanyIdQuery>();

            //ControlType
            services.AddTransient<ICreateControlTypeCommand, CreateControlTypeCommand>();
            services.AddTransient<IUpdateControlTypeCommand, UpdateControlTypeCommand>();
            services.AddTransient<IDeleteControlTypeCommand, DeleteControlTypeCommand>();
            services.AddTransient<IGetControlTypeByIdQuery, GetControlTypeByIdQuery>();
            services.AddTransient<IGetAllControlTypesByCompanyIdQuery, GetAllControlTypesByCompanyIdQuery>();
            services.AddTransient<IGetControlTypesByCompanyIdQuery, GetControlTypesByCompanyIdQuery>();

            //RiskLevel
            services.AddTransient<ICreateRiskLevelCommand, CreateRiskLevelCommand>();
            services.AddTransient<IUpdateRiskLevelCommand, UpdateRiskLevelCommand>();
            services.AddTransient<IDeleteRiskLevelCommand, DeleteRiskLevelCommand>();
            services.AddTransient<IGetRiskLevelByIdQuery, GetRiskLevelByIdQuery>();
            services.AddTransient<IGetAllRiskLevelsByCompanyIdQuery, GetAllRiskLevelsByCompanyIdQuery>();
            services.AddTransient<IGetRiskLevelsByCompanyIdQuery, GetRiskLevelsByCompanyIdQuery>();

            //Risk
            services.AddTransient<ICreateRiskCommand, CreateRiskCommand>();
            services.AddTransient<IUpdateRiskCommand, UpdateRiskCommand>();
            services.AddTransient<IDeleteRiskCommand, DeleteRiskCommand>();
            services.AddTransient<IGetRiskByIdQuery, GetRiskByIdQuery>();
            services.AddTransient<IGetRisksByCompanyIdQuery, GetRisksByCompanyIdQuery>();

            //Company
            services.AddTransient<IGetCompanyByIdQuery, GetCompanyByIdQuery>();
            services.AddTransient<IUpdateCompanyCommand, UpdateCompanyCommand>();

            return services;
        }

    }
}
