using AutoMapper;
using Qualifier.Application.Database.ActivesInventory.Commands.CreateActivesInventory;
using Qualifier.Application.Database.ActivesInventory.Commands.UpdateActivesInventory;
using Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoriesByCompanyId;
using Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoryById;
using Qualifier.Application.Database.ActivesInventory.Queries.GetAllActivesInventoriesByCompanyId;
using Qualifier.Application.Database.ActiveType.Commands.CreateActiveType;
using Qualifier.Application.Database.ActiveType.Commands.UpdateActiveType;
using Qualifier.Application.Database.ActiveType.Queries.GetActiveTypeById;
using Qualifier.Application.Database.ActiveType.Queries.GetActiveTypesByCompanyId;
using Qualifier.Application.Database.ActiveType.Queries.GetAllActiveTypesByCompanyId;
using Qualifier.Application.Database.Approver.Commands.CreateApprover;
using Qualifier.Application.Database.Approver.Queries.GetAllApproversByVersionId;
using Qualifier.Application.Database.Approver.Queries.GetApproverById;
using Qualifier.Application.Database.Company.Commands.UpdateCompany;
using Qualifier.Application.Database.Company.Queries.GetCompanyById;
using Qualifier.Application.Database.ConfidentialityLevel.Commands.CreateConfidentialityLevel;
using Qualifier.Application.Database.ConfidentialityLevel.Commands.UpdateConfidentialityLevel;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetAllConfidentialityLevelsByCompanyId;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelById;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelsByCompanyId;
using Qualifier.Application.Database.Control.Commands.CreateControl;
using Qualifier.Application.Database.Control.Commands.UpdateControl;
using Qualifier.Application.Database.Control.Queries.GetAllControlsByStandardId;
using Qualifier.Application.Database.Control.Queries.GetControlById;
using Qualifier.Application.Database.Control.Queries.GetControlsByControlGroupId;
using Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationById;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Application.Database.ControlGroup.Commands.CreateControlGroup;
using Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByStandardId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupById;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByStandardId;
using Qualifier.Application.Database.ControlType.Commands.CreateControlType;
using Qualifier.Application.Database.ControlType.Commands.UpdateControlType;
using Qualifier.Application.Database.ControlType.Queries.GetAllControlTypesByCompanyId;
using Qualifier.Application.Database.ControlType.Queries.GetControlTypeById;
using Qualifier.Application.Database.ControlType.Queries.GetControlTypesByCompanyId;
using Qualifier.Application.Database.Creator.Commands.CreateCreator;
using Qualifier.Application.Database.Creator.Commands.UpdateCreator;
using Qualifier.Application.Database.Creator.Queries.GetAllCreatorsByVersionId;
using Qualifier.Application.Database.Creator.Queries.GetCreatorById;
using Qualifier.Application.Database.Custodian.Commands.CreateCustodian;
using Qualifier.Application.Database.Custodian.Commands.UpdateCustodian;
using Qualifier.Application.Database.Custodian.Queries.GetAllCustodiansByCompanyId;
using Qualifier.Application.Database.Custodian.Queries.GetCustodianById;
using Qualifier.Application.Database.Custodian.Queries.GetCustodiansByCompanyId;
using Qualifier.Application.Database.DefaultSection.Commands.CreateDefaultSection;
using Qualifier.Application.Database.DefaultSection.Commands.UpdateDefaultSection;
using Qualifier.Application.Database.DefaultSection.Queries.GetAllDefaultSectionsByDocumentTypeId;
using Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionById;
using Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionsByDocumentTypeId;
using Qualifier.Application.Database.Documentation.Commands.CreateDocumentation;
using Qualifier.Application.Database.Documentation.Commands.UpdateDocumentation;
using Qualifier.Application.Database.Documentation.Queries.GetAllDocumentationsByStandardId;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationById;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByCompanyId;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByStandardId;
using Qualifier.Application.Database.DocumentType.Commands.CreateDocumentType;
using Qualifier.Application.Database.DocumentType.Commands.UpdateDocumentType;
using Qualifier.Application.Database.DocumentType.Queries.GetAllDocumentTypesByCompanyId;
using Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypeById;
using Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypesByCompanyId;
using Qualifier.Application.Database.Evaluation.Commands.CreateEvaluation;
using Qualifier.Application.Database.Evaluation.Commands.UpdateEvaluation;
using Qualifier.Application.Database.Evaluation.Queries.GetAllEvaluationsByCompanyId;
using Qualifier.Application.Database.Evaluation.Queries.GetControlsDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.Evaluation.Queries.GetDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationById;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId;
using Qualifier.Application.Database.Evaluation.Queries.GetPendingDocumentation;
using Qualifier.Application.Database.ImpactValuation.Commands.CreateImpactValuation;
using Qualifier.Application.Database.ImpactValuation.Commands.UpdateImpactValuation;
using Qualifier.Application.Database.ImpactValuation.Queries.GetAllImpactValuationsByCompanyId;
using Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationById;
using Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationsByCompanyId;
using Qualifier.Application.Database.Indicator.Commands.CreateIndicator;
using Qualifier.Application.Database.Indicator.Commands.UpdateIndicator;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorById;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorsByCompanyId;
using Qualifier.Application.Database.Location.Commands.CreateLocation;
using Qualifier.Application.Database.Location.Commands.UpdateLocation;
using Qualifier.Application.Database.Location.Queries.GetAllLocationsByCompanyId;
using Qualifier.Application.Database.Location.Queries.GetLocationById;
using Qualifier.Application.Database.Location.Queries.GetLocationsByCompanyId;
using Qualifier.Application.Database.Macroprocess.Commands.CreateMacroprocess;
using Qualifier.Application.Database.Macroprocess.Commands.UpdateMacroprocess;
using Qualifier.Application.Database.Macroprocess.Queries.GetAllMacroprocesssByCompanyId;
using Qualifier.Application.Database.Macroprocess.Queries.GetMacroprocessById;
using Qualifier.Application.Database.Macroprocess.Queries.GetMacroprocesssByCompanyId;
using Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Commands.UpdateMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelById;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelsByCompanyId;
using Qualifier.Application.Database.Menace.Commands.CreateMenace;
using Qualifier.Application.Database.Menace.Commands.UpdateMenace;
using Qualifier.Application.Database.Menace.Queries.GetAllMenacesByCompanyId;
using Qualifier.Application.Database.Menace.Queries.GetMenaceById;
using Qualifier.Application.Database.Menace.Queries.GetMenacesByCompanyId;
using Qualifier.Application.Database.MenaceType.Commands.CreateMenaceType;
using Qualifier.Application.Database.MenaceType.Commands.UpdateMenaceType;
using Qualifier.Application.Database.MenaceType.Queries.GetAllMenaceTypesByCompanyId;
using Qualifier.Application.Database.MenaceType.Queries.GetMenaceTypeById;
using Qualifier.Application.Database.MenaceType.Queries.GetMenaceTypesByCompanyId;
using Qualifier.Application.Database.Menu.Commands.CreateMenu;
using Qualifier.Application.Database.Menu.Commands.UpdateMenu;
using Qualifier.Application.Database.Menu.Queries.GetAllMenusByCompanyId;
using Qualifier.Application.Database.Menu.Queries.GetAllMenusByRoleId;
using Qualifier.Application.Database.Menu.Queries.GetMenuById;
using Qualifier.Application.Database.Menu.Queries.GetMenusByCompanyId;
using Qualifier.Application.Database.MenuInRole.Commands.CreateMenuInRole;
using Qualifier.Application.Database.MenuInRole.Commands.UpdateMenuInRole;
using Qualifier.Application.Database.MenuInRole.Queries.GetAllMenuInRolesByRoleId;
using Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRoleById;
using Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRolesByRoleId;
using Qualifier.Application.Database.Option.Commands.CreateOption;
using Qualifier.Application.Database.Option.Commands.UpdateOption;
using Qualifier.Application.Database.Option.Queries.GetAllOptionsByCompanyId;
using Qualifier.Application.Database.Option.Queries.GetOptionById;
using Qualifier.Application.Database.Option.Queries.GetOptionsByCompanyId;
using Qualifier.Application.Database.OptionInMenu.Commands.CreateOptionInMenu;
using Qualifier.Application.Database.OptionInMenu.Commands.UpdateOptionInMenu;
using Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenuById;
using Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenusByMenuId;
using Qualifier.Application.Database.OptionInMenuInRole.Commands.CreateOptionInMenuInRole;
using Qualifier.Application.Database.Owner.Commands.CreateOwner;
using Qualifier.Application.Database.Owner.Commands.UpdateOwner;
using Qualifier.Application.Database.Owner.Queries.GetAllOwnersByCompanyId;
using Qualifier.Application.Database.Owner.Queries.GetOwnerById;
using Qualifier.Application.Database.Owner.Queries.GetOwnersByCompanyId;
using Qualifier.Application.Database.Personal.Commands.CreatePersonal;
using Qualifier.Application.Database.Personal.Commands.UpdatePersonal;
using Qualifier.Application.Database.Personal.Queries.GetAllPersonalsByCompanyId;
using Qualifier.Application.Database.Personal.Queries.GetPersonalById;
using Qualifier.Application.Database.Personal.Queries.GetPersonalsByCompanyId;
using Qualifier.Application.Database.Policy.Commands.CreatePolicy;
using Qualifier.Application.Database.Policy.Commands.UpdatePolicy;
using Qualifier.Application.Database.Policy.Queries.GetAllPoliciesByStandardId;
using Qualifier.Application.Database.Policy.Queries.GetPoliciesByStandardId;
using Qualifier.Application.Database.Policy.Queries.GetPolicyById;
using Qualifier.Application.Database.Requirement.Commands.CreateRequirement;
using Qualifier.Application.Database.Requirement.Commands.UpdateRequirement;
using Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementById;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId;
using Qualifier.Application.Database.RequirementEvaluation.Commands.CreateRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Commands.UpdateRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationById;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationsByRequirementId;
using Qualifier.Application.Database.Responsible.Commands.CreateResponsible;
using Qualifier.Application.Database.Responsible.Commands.UpdateResponsible;
using Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId;
using Qualifier.Application.Database.Responsible.Queries.GetResponsibleById;
using Qualifier.Application.Database.Responsible.Queries.GetResponsiblesByStandardId;
using Qualifier.Application.Database.Reviewer.Commands.CreateReviewer;
using Qualifier.Application.Database.Reviewer.Commands.UpdateReviewer;
using Qualifier.Application.Database.Reviewer.Queries.GetAllReviewersByVersionId;
using Qualifier.Application.Database.Reviewer.Queries.GetReviewerById;
using Qualifier.Application.Database.Risk.Commands.CreateRisk;
using Qualifier.Application.Database.Risk.Commands.UpdateRisk;
using Qualifier.Application.Database.Risk.Queries.GetAllRisksByEvaluationId;
using Qualifier.Application.Database.Risk.Queries.GetRiskById;
using Qualifier.Application.Database.Risk.Queries.GetRisksByEvaluationId;
using Qualifier.Application.Database.RiskAssessment.Commands.CreateRiskAssessment;
using Qualifier.Application.Database.RiskAssessment.Commands.UpdateRiskAssessment;
using Qualifier.Application.Database.RiskAssessment.Queries.GetRiskAssessmentById;
using Qualifier.Application.Database.RiskLevel.Commands.CreateRiskLevel;
using Qualifier.Application.Database.RiskLevel.Commands.UpdateRiskLevel;
using Qualifier.Application.Database.RiskLevel.Queries.GetAllRiskLevelsByCompanyId;
using Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelById;
using Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelsByCompanyId;
using Qualifier.Application.Database.RiskTreatmentMethod.Commands.CreateRiskTreatmentMethod;
using Qualifier.Application.Database.RiskTreatmentMethod.Commands.UpdateRiskTreatmentMethod;
using Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetAllRiskTreatmentMethodsByCompanyId;
using Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetRiskTreatmentMethodById;
using Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetRiskTreatmentMethodsByCompanyId;
using Qualifier.Application.Database.Role.Commands.CreateRole;
using Qualifier.Application.Database.Role.Commands.UpdateRole;
using Qualifier.Application.Database.Role.Queries.GetAllRolesByCompanyId;
using Qualifier.Application.Database.Role.Queries.GetRoleById;
using Qualifier.Application.Database.Role.Queries.GetRolesByCompanyId;
using Qualifier.Application.Database.RoleInUser.Commands.CreateRoleInUser;
using Qualifier.Application.Database.RoleInUser.Commands.UpdateRoleInUser;
using Qualifier.Application.Database.RoleInUser.Queries.GetAllRoleInUsersByUserId;
using Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUserById;
using Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUsersByUserId;
using Qualifier.Application.Database.Scope.Commands.CreateScope;
using Qualifier.Application.Database.Scope.Commands.UpdateScope;
using Qualifier.Application.Database.Scope.Queries.GetAllScopesByStandardId;
using Qualifier.Application.Database.Scope.Queries.GetScopeById;
using Qualifier.Application.Database.Scope.Queries.GetScopesByStandardId;
using Qualifier.Application.Database.Section.Commands.CreateSection;
using Qualifier.Application.Database.Section.Commands.UpdateSection;
using Qualifier.Application.Database.Section.Queries.GetAllSectionsByDocumentationId;
using Qualifier.Application.Database.Section.Queries.GetAllSectionsByVersionId;
using Qualifier.Application.Database.Section.Queries.GetSectionById;
using Qualifier.Application.Database.Section.Queries.GetSectionsByDocumentationId;
using Qualifier.Application.Database.Section.Queries.GetSectionsByVersionId;
using Qualifier.Application.Database.Standard.Commands.CreateStandard;
using Qualifier.Application.Database.Standard.Commands.UpdateStandard;
using Qualifier.Application.Database.Standard.Queries.GetAllStandardsByCompanyId;
using Qualifier.Application.Database.Standard.Queries.GetStandardById;
using Qualifier.Application.Database.Standard.Queries.GetStandardsByCompanyId;
using Qualifier.Application.Database.Subprocess.Commands.CreateSubprocess;
using Qualifier.Application.Database.Subprocess.Commands.UpdateSubprocess;
using Qualifier.Application.Database.Subprocess.Queries.GetAllSubprocesssByCompanyId;
using Qualifier.Application.Database.Subprocess.Queries.GetSubprocessById;
using Qualifier.Application.Database.Subprocess.Queries.GetSubprocesssByCompanyId;
using Qualifier.Application.Database.SupportForControl.Commands.CreateSupportForControl;
using Qualifier.Application.Database.SupportForControl.Commands.UpdateSupportForControl;
using Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlById;
using Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlsByDocumentationId;
using Qualifier.Application.Database.SupportForRequirement.Commands.CreateSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Commands.UpdateSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementById;
using Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementsByDocumentationId;
using Qualifier.Application.Database.SupportType.Commands.CreateSupportType;
using Qualifier.Application.Database.SupportType.Commands.UpdateSupportType;
using Qualifier.Application.Database.SupportType.Queries.GetAllSupportTypesByCompanyId;
using Qualifier.Application.Database.SupportType.Queries.GetSupportTypeById;
using Qualifier.Application.Database.SupportType.Queries.GetSupportTypesByCompanyId;
using Qualifier.Application.Database.UsageClassification.Commands.CreateUsageClassification;
using Qualifier.Application.Database.UsageClassification.Commands.UpdateUsageClassification;
using Qualifier.Application.Database.UsageClassification.Queries.GetAllUsageClassificationsByCompanyId;
using Qualifier.Application.Database.UsageClassification.Queries.GetUsageClassificationById;
using Qualifier.Application.Database.UsageClassification.Queries.GetUsageClassificationsByCompanyId;
using Qualifier.Application.Database.User.Commands.CreateUser;
using Qualifier.Application.Database.User.Commands.Login;
using Qualifier.Application.Database.User.Commands.UpdateUser;
using Qualifier.Application.Database.User.Queries.GetAllUsersByCompanyId;
using Qualifier.Application.Database.User.Queries.GetMenus;
using Qualifier.Application.Database.User.Queries.GetUserById;
using Qualifier.Application.Database.User.Queries.GetUsersByCompanyId;
using Qualifier.Application.Database.UserState.Commands.CreateUserState;
using Qualifier.Application.Database.UserState.Commands.UpdateUserState;
using Qualifier.Application.Database.UserState.Queries.GetAllUserStatesByCompanyId;
using Qualifier.Application.Database.UserState.Queries.GetUserStateById;
using Qualifier.Application.Database.UserState.Queries.GetUserStatesByCompanyId;
using Qualifier.Application.Database.ValuationInActive.Commands.CreateValuationInActive;
using Qualifier.Application.Database.ValuationInActive.Commands.UpdateValuationInActive;
using Qualifier.Application.Database.ValuationInActive.Queries.GetAllValuationInActivesByCompanyId;
using Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActiveById;
using Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActivesByActivesInventoryId;
using Qualifier.Application.Database.Version.Commands.CreateVersion;
using Qualifier.Application.Database.Version.Commands.UpdateVersion;
using Qualifier.Application.Database.Version.Queries.GetAllVersionsByDocumentationId;
using Qualifier.Application.Database.Version.Queries.GetVersionById;
using Qualifier.Application.Database.Version.Queries.GetVersionsByDocumentationId;
using Qualifier.Application.Database.Vulnerability.Commands.CreateVulnerability;
using Qualifier.Application.Database.Vulnerability.Commands.UpdateVulnerability;
using Qualifier.Application.Database.Vulnerability.Queries.GetAllVulnerabilitiesByCompanyId;
using Qualifier.Application.Database.Vulnerability.Queries.GetVulnerabilitiesByCompanyId;
using Qualifier.Application.Database.Vulnerability.Queries.GetVulnerabilityById;
using Qualifier.Application.Database.VulnerabilityType.Commands.CreateVulnerabilityType;
using Qualifier.Application.Database.VulnerabilityType.Commands.UpdateVulnerabilityType;
using Qualifier.Application.Database.VulnerabilityType.Queries.GetAllVulnerabilityTypesByCompanyId;
using Qualifier.Application.Database.VulnerabilityType.Queries.GetVulnerabilityTypeById;
using Qualifier.Application.Database.VulnerabilityType.Queries.GetVulnerabilityTypesByCompanyId;
using Qualifier.Domain.Entities;
using static Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation.CreateControlEvaluationDto;
using static Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation.UpdateControlEvaluationDto;


namespace Qualifier.Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //MaturityLevel
            CreateMap<MaturityLevelEntity, GetMaturityLevelByIdDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, CreateMaturityLevelDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, UpdateMaturityLevelDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, GetMaturityLevelsByCompanyIdDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, GetAllMaturityLevelsByCompanyIdDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, GetDashboardMaturityLevelDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, GetDashboardMaturityLevelInRequirementDto>().ReverseMap();

            //Indicator
            CreateMap<IndicatorEntity, CreateIndicatorDto>().ReverseMap();
            CreateMap<IndicatorEntity, UpdateIndicatorDto>().ReverseMap();
            CreateMap<IndicatorEntity, GetIndicatorByIdDto>().ReverseMap();
            CreateMap<IndicatorEntity, GetIndicatorsByCompanyIdDto>().ReverseMap();
            CreateMap<IndicatorEntity, GetDashboardMaturityLevelInRequirementIndicatorDto>().ReverseMap();

            //Login
            CreateMap<LoginEntity, LoginUserLoginTryDto>().ReverseMap();

            CreateMap<LoginEntity, LoginUserLoginDto>().ReverseMap();
            CreateMap<UserStateEntity, LoginUserUserStateDto>().ReverseMap();
            CreateMap<RoleEntity, LoginUserRoleDto>().ReverseMap();

            //User
            CreateMap<MenuEntity, GetMenusMenuQueryDto>().ReverseMap();
            CreateMap<OptionEntity, GetMenusOptionQueryDto>().ReverseMap();

            //Standard
            CreateMap<StandardEntity, CreateStandardDto>().ReverseMap();
            CreateMap<StandardEntity, UpdateStandardDto>().ReverseMap();
            CreateMap<StandardEntity, GetStandardByIdDto>().ReverseMap();
            CreateMap<StandardEntity, GetStandardsByCompanyIdDto>().ReverseMap();
            CreateMap<StandardEntity, GetAllStandardsByCompanyIdDto>().ReverseMap();
            CreateMap<StandardEntity, GetEvaluationByIdDtoStandardDto>().ReverseMap();

            //ControlGroup
            CreateMap<ControlGroupEntity, CreateControlGroupDto>().ReverseMap();
            CreateMap<ControlGroupEntity, UpdateControlGroupDto>().ReverseMap();
            CreateMap<ControlGroupEntity, GetAllControlGroupsByCompanyIdDto>().ReverseMap();
            CreateMap<ControlGroupEntity, GetControlGroupsByCompanyIdDto>().ReverseMap();
            CreateMap<ControlGroupEntity, GetControlGroupByIdDto>().ReverseMap();
            CreateMap<ControlGroupEntity, GetControlGroupsByStandardIdDto>().ReverseMap();
            CreateMap<ControlGroupEntity, GetAllControlGroupsByStandardIdDto>().ReverseMap();

            //Control
            CreateMap<ControlEntity, UpdateControlDto>().ReverseMap();
            CreateMap<ControlEntity, CreateControlDto>().ReverseMap();
            CreateMap<ControlEntity, GetControlsByControlGroupIdDto>().ReverseMap();
            CreateMap<ControlEntity, GetControlByIdDto>().ReverseMap();
            CreateMap<ControlEntity, GetPendingDocumentationControlDto>().ReverseMap();

            //Requirement
            CreateMap<RequirementEntity, CreateRequirementDto>().ReverseMap();
            CreateMap<RequirementEntity, UpdateRequirementDto>().ReverseMap();
            CreateMap<RequirementEntity, GetRequirementsByStandardIdDto>().ReverseMap();
            CreateMap<RequirementEntity, GetRequirementByIdDto>().ReverseMap();
            CreateMap<RequirementEntity, GetAllRequirementsByStandardIdDto>().ReverseMap();
            CreateMap<RequirementEntity, GetDashboardRequirementDto>().ReverseMap();
            CreateMap<RequirementEntity, GetPendingDocumentationRequirementDto>().ReverseMap();

            //Responsible
            CreateMap<ResponsibleEntity, UpdateResponsibleDto>().ReverseMap();
            CreateMap<ResponsibleEntity, CreateResponsibleDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetResponsiblesByStandardIdDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetResponsibleByIdDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetAllResponsiblesByStandardIdDto>().ReverseMap();

            //Documentation
            CreateMap<DocumentationEntity, GetDocumentationsByStandardIdDto>().ReverseMap();
            CreateMap<DocumentationEntity, GetDocumentationByIdDto>().ReverseMap();
            CreateMap<DocumentationEntity, CreateDocumentationDto>().ReverseMap();
            CreateMap<DocumentationEntity, UpdateDocumentationDto>().ReverseMap();
            CreateMap<DocumentationEntity, GetAllDocumentationsByStandardIdDto>().ReverseMap();

            //Evaluation
            CreateMap<EvaluationEntity, CreateEvaluationDto>().ReverseMap();
            CreateMap<EvaluationEntity, UpdateEvaluationDto>().ReverseMap();
            CreateMap<EvaluationEntity, GetEvaluationsByCompanyIdDto>().ReverseMap();
            CreateMap<EvaluationStateEntity, GetEvaluationsByCompanyIdEvaluationStateDto>().ReverseMap();
            CreateMap<EvaluationEntity, GetEvaluationByIdDto>().ReverseMap();
            CreateMap<EvaluationEntity, GetAllEvaluationsByCompanyIdDto>().ReverseMap();
            CreateMap<EvaluationEntity, GetCurrentEvaluationDto>().ReverseMap();

            //RequirementEvaluation
            CreateMap<RequirementEvaluationEntity, CreateRequirementEvaluationDto>().ReverseMap();
            CreateMap<RequirementEvaluationEntity, UpdateRequirementEvaluationDto>().ReverseMap();

            //CreateMap<RequirementEvaluationEntity, GetRequirementEvaluationsByRequirementIdEvaluationDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, GetRequirementEvaluationsByRequirementIdMaturityLevelDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetRequirementEvaluationsByRequirementIdResponsibleDto>().ReverseMap();
            CreateMap<RequirementEvaluationEntity, GetRequirementEvaluationsByRequirementIdDto>().ReverseMap();
            CreateMap<RequirementEvaluationEntity, GetRequirementEvaluationByIdDto>().ReverseMap();


            CreateMap<MaturityLevelEntity, GetRequirementEvaluationsByProcessMaturityLevelDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetRequirementEvaluationsByProcessResponsibleDto>().ReverseMap();
            CreateMap<RequirementEntity, GetRequirementEvaluationsByProcessRequirementDto>().ReverseMap();
            CreateMap<RequirementEvaluationEntity, GetRequirementEvaluationsByProcessDto>().ReverseMap();
            CreateMap<RequirementEntity, GetRequirementEvaluationsByProcessChildRequirementDto>().ReverseMap();

            //ReferenceDocumentation
            CreateMap<ReferenceDocumentationEntity, CreateRequirementEvaluationReferenceDocumentationDto>().ReverseMap();
            CreateMap<ReferenceDocumentationEntity, UpdateRequirementEvaluationReferenceDocumentationDto>().ReverseMap();
            CreateMap<ReferenceDocumentationEntity, CreateControlEvaluationReferenceDocumentationDto>().ReverseMap();
            CreateMap<ReferenceDocumentationEntity, UpdateControlEvaluationReferenceDocumentationDto>().ReverseMap();
            CreateMap<ReferenceDocumentationEntity, GetRequirementEvaluationsByProcessReferenceDocumentationDto>().ReverseMap();
            CreateMap<ReferenceDocumentationEntity, GetControlEvaluationsByProcessReferenceDocumentationDto>().ReverseMap();

            //ControlEvaluation
            CreateMap<ControlEvaluationEntity, GetControlEvaluationByIdDto>().ReverseMap();
            CreateMap<ControlEvaluationEntity, UpdateControlEvaluationDto>().ReverseMap();
            CreateMap<ControlEvaluationEntity, CreateControlEvaluationDto>().ReverseMap();
            CreateMap<ControlGroupEntity, GetControlEvaluationsByProcessControlGroupDto>().ReverseMap();
            CreateMap<ControlEntity, GetControlEvaluationsByProcessControlDto>().ReverseMap();

            CreateMap<MaturityLevelEntity, GetControlEvaluationsByProcessMaturityLevelDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetControlEvaluationsByProcessResponsibleDto>().ReverseMap();
            CreateMap<ControlEvaluationEntity, GetControlEvaluationsByProcessDto>().ReverseMap();

            //Dashboard
            CreateMap<PieDashboardRequirement, GetPieDashboardRequirementDto>().ReverseMap();
            CreateMap<BartVerticalDashboardRequirement, GetBartVerticalDashboardRequirementDto>().ReverseMap();
            CreateMap<DashboardRequirementSerie, GetDashboardRequirementSerieDto>().ReverseMap();

            CreateMap<ControlGroupEntity, GetControlDashboardControlGroupDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, GetControlDashboardMaturityLevelDto>().ReverseMap();
            CreateMap<MaturityLevelEntity, GetDashboardMaturityLevelInControlDto>().ReverseMap();
            CreateMap<IndicatorEntity, GetControlDashboardMaturityLevelInControlIndicatorDto>().ReverseMap();
            CreateMap<PieControlDashboardControlGroup, GetPieControlDashboardControlGroupDto>().ReverseMap();

            CreateMap<BartVerticalControlDashboard, GetBartVerticalControlDashboardDto>().ReverseMap();
            CreateMap<DashboardControlGroupSerie, GetDashboardControlGroupSerieDto>().ReverseMap();


            ////ExcelDashboard
            //CreateMap<MaturityLevelEntity, GetExcelDashboardMaturityLevelDto>().ReverseMap();
            //CreateMap<ResponsibleEntity, GetExcelDashboardResponsibleDto>().ReverseMap();
            //CreateMap<RequirementEntity, GetExcelDashboardRequirementDto>().ReverseMap();
            //CreateMap<RequirementEvaluationEntity, GetExcelDashboardDto>().ReverseMap();
            //CreateMap<RequirementEntity, GetExcelDashboardChildRequirementDto>().ReverseMap();

            //DocumentType
            CreateMap<DocumentTypeEntity, CreateDocumentTypeDto>().ReverseMap();
            CreateMap<DocumentTypeEntity, UpdateDocumentTypeDto>().ReverseMap();
            CreateMap<DocumentTypeEntity, GetDocumentTypeByIdDto>().ReverseMap();
            CreateMap<DocumentTypeEntity, GetDocumentTypesByCompanyIdDto>().ReverseMap();
            CreateMap<DocumentTypeEntity, GetAllDocumentTypesByCompanyIdDto>().ReverseMap();

            //DefaultSection
            CreateMap<DefaultSectionEntity, CreateDefaultSectionDto>().ReverseMap();
            CreateMap<DefaultSectionEntity, UpdateDefaultSectionDto>().ReverseMap();
            CreateMap<DefaultSectionEntity, GetDefaultSectionByIdDto>().ReverseMap();
            CreateMap<DefaultSectionEntity, GetAllDefaultSectionsByDocumentTypeIdDto>().ReverseMap();
            CreateMap<DefaultSectionEntity, GetDefaultSectionsByDocumentTypeIdDto>().ReverseMap();

            //Documentation
            CreateMap<DocumentationEntity, GetDocumentationsByCompanyIdDto>().ReverseMap();
            CreateMap<DocumentTypeEntity, GetDocumentationsByCompanyIdDocumentTypeDto>().ReverseMap();
            CreateMap<StandardEntity, GetDocumentationsByCompanyIdStandardDto>().ReverseMap();
            CreateMap<DocumentationEntity, GetPendingDocumentationDto>().ReverseMap();

            //ConfidentialityLevel
            CreateMap<ConfidentialityLevelEntity, CreateConfidentialityLevelDto>().ReverseMap();
            CreateMap<ConfidentialityLevelEntity, UpdateConfidentialityLevelDto>().ReverseMap();
            CreateMap<ConfidentialityLevelEntity, GetConfidentialityLevelByIdDto>().ReverseMap();
            CreateMap<ConfidentialityLevelEntity, GetAllConfidentialityLevelsByCompanyIdDto>().ReverseMap();
            CreateMap<ConfidentialityLevelEntity, GetConfidentialityLevelsByCompanyIdDto>().ReverseMap();

            //Version
            CreateMap<VersionEntity, CreateVersionDto>().ReverseMap();
            CreateMap<VersionEntity, UpdateVersionDto>().ReverseMap();
            CreateMap<VersionEntity, GetVersionByIdDto>().ReverseMap();
            CreateMap<VersionEntity, GetVersionsByDocumentationIdDto>().ReverseMap();
            CreateMap<ConfidentialityLevelEntity, GetVersionsByDocumentationIdConfidentialityLevelDto>().ReverseMap();
            CreateMap<VersionEntity, GetAllVersionsByDocumentationIdDto>().ReverseMap();

            //SupportForRequirement
            CreateMap<SupportForRequirementEntity, CreateSupportForRequirementDto>().ReverseMap();
            CreateMap<SupportForRequirementEntity, UpdateSupportForRequirementDto>().ReverseMap();
            CreateMap<SupportForRequirementEntity, GetSupportForRequirementByIdDto>().ReverseMap();
            CreateMap<SupportForRequirementEntity, GetSupportForRequirementsByDocumentationIdDto>().ReverseMap();
            CreateMap<RequirementEntity, GetSupportForRequirementsByDocumentationIdRequirementDto>().ReverseMap();

            //SupportForControl
            CreateMap<SupportForControlEntity, CreateSupportForControlDto>().ReverseMap();
            CreateMap<SupportForControlEntity, UpdateSupportForControlDto>().ReverseMap();
            CreateMap<SupportForControlEntity, GetSupportForControlByIdDto>().ReverseMap();
            CreateMap<SupportForControlEntity, GetSupportForControlsByDocumentationIdDto>().ReverseMap();
            CreateMap<ControlEntity, GetSupportForControlsByDocumentationIdControlDto>().ReverseMap();

            //Control
            CreateMap<ControlEntity, GetAllControlsByStandardIdDto>().ReverseMap();

            //Section
            CreateMap<SectionEntity, CreateSectionDto>().ReverseMap();
            CreateMap<SectionEntity, UpdateSectionDto>().ReverseMap();
            CreateMap<SectionEntity, GetSectionByIdDto>().ReverseMap();
            CreateMap<SectionEntity, GetAllSectionsByVersionIdDto>().ReverseMap();
            CreateMap<SectionEntity, GetSectionsByVersionIdDto>().ReverseMap();
            CreateMap<SectionEntity, GetSectionsByDocumentationIdDto>().ReverseMap();
            CreateMap<SectionEntity, GetAllSectionsByDocumentationIdDto>().ReverseMap();

            //Personal
            CreateMap<PersonalEntity, CreatePersonalDto>().ReverseMap();
            CreateMap<PersonalEntity, UpdatePersonalDto>().ReverseMap();
            CreateMap<PersonalEntity, GetPersonalByIdDto>().ReverseMap();
            CreateMap<PersonalEntity, GetAllPersonalsByCompanyIdDto>().ReverseMap();
            CreateMap<PersonalEntity, GetPersonalsByCompanyIdDto>().ReverseMap();

            //Creator
            CreateMap<CreatorEntity, CreateCreatorDto>().ReverseMap();
            CreateMap<CreatorEntity, UpdateCreatorDto>().ReverseMap();
            CreateMap<CreatorEntity, GetAllCreatorsByVersionIdDto>().ReverseMap();
            CreateMap<CreatorEntity, GetAllCreatorsByVersionIdDto>().ReverseMap();
            CreateMap<PersonalEntity, GetAllCreatorsByVersionIdPersonalDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetAllCreatorsByVersionIdResponsibleDto>().ReverseMap();
            CreateMap<CreatorEntity, GetCreatorByIdDto>().ReverseMap();

            //Reviewer
            CreateMap<ReviewerEntity, CreateReviewerDto>().ReverseMap();
            CreateMap<ReviewerEntity, UpdateReviewerDto>().ReverseMap();
            CreateMap<ReviewerEntity, GetReviewerByIdDto>().ReverseMap();
            CreateMap<ReviewerEntity, GetAllReviewersByVersionIdDto>().ReverseMap();
            CreateMap<PersonalEntity, GetAllReviewersByVersionIdPersonalDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetAllReviewersByVersionIdResponsibleDto>().ReverseMap();

            //Approver
            CreateMap<ApproverEntity, CreateApproverDto>().ReverseMap();
            CreateMap<ApproverEntity, GetApproverByIdDto>().ReverseMap();
            CreateMap<ApproverEntity, GetAllApproversByVersionIdDto>().ReverseMap();
            CreateMap<PersonalEntity, GetAllApproversByVersionIdPersonalDto>().ReverseMap();
            CreateMap<ResponsibleEntity, GetAllApproversByVersionIdResponsibleDto>().ReverseMap();

            //Macroprocess
            CreateMap<MacroprocessEntity, CreateMacroprocessDto>().ReverseMap();
            CreateMap<MacroprocessEntity, UpdateMacroprocessDto>().ReverseMap();
            CreateMap<MacroprocessEntity, GetMacroprocessByIdDto>().ReverseMap();
            CreateMap<MacroprocessEntity, GetAllMacroprocesssByCompanyIdDto>().ReverseMap();
            CreateMap<MacroprocessEntity, GetMacroprocesssByCompanyIdDto>().ReverseMap();

            //Subprocess
            CreateMap<SubprocessEntity, CreateSubprocessDto>().ReverseMap();
            CreateMap<SubprocessEntity, UpdateSubprocessDto>().ReverseMap();
            CreateMap<SubprocessEntity, GetSubprocesssByCompanyIdDto>().ReverseMap();
            CreateMap<MacroprocessEntity, GetSubprocesssByCompanyIdMacroprocessDto>().ReverseMap();
            CreateMap<SubprocessEntity, GetAllSubprocesssByCompanyIdDto>().ReverseMap();
            CreateMap<MacroprocessEntity, GetAllSubprocesssByCompanyIdMacroprocessDto>().ReverseMap();
            CreateMap<SubprocessEntity, GetSubprocessByIdDto>().ReverseMap();

            //ActiveType
            CreateMap<ActiveTypeEntity, CreateActiveTypeDto>().ReverseMap();
            CreateMap<ActiveTypeEntity, UpdateActiveTypeDto>().ReverseMap();
            CreateMap<ActiveTypeEntity, GetActiveTypeByIdDto>().ReverseMap();
            CreateMap<ActiveTypeEntity, GetAllActiveTypesByCompanyIdDto>().ReverseMap();
            CreateMap<ActiveTypeEntity, GetActiveTypesByCompanyIdDto>().ReverseMap();

            //Owner
            CreateMap<OwnerEntity, CreateOwnerDto>().ReverseMap();
            CreateMap<OwnerEntity, UpdateOwnerDto>().ReverseMap();
            CreateMap<OwnerEntity, GetOwnerByIdDto>().ReverseMap();
            CreateMap<OwnerEntity, GetAllOwnersByCompanyIdDto>().ReverseMap();
            CreateMap<OwnerEntity, GetOwnersByCompanyIdDto>().ReverseMap();

            //Custodian
            CreateMap<CustodianEntity, CreateCustodianDto>().ReverseMap();
            CreateMap<CustodianEntity, UpdateCustodianDto>().ReverseMap();
            CreateMap<CustodianEntity, GetCustodianByIdDto>().ReverseMap();
            CreateMap<CustodianEntity, GetAllCustodiansByCompanyIdDto>().ReverseMap();
            CreateMap<CustodianEntity, GetCustodiansByCompanyIdDto>().ReverseMap();

            //UsageClassification
            CreateMap<UsageClassificationEntity, CreateUsageClassificationDto>().ReverseMap();
            CreateMap<UsageClassificationEntity, UpdateUsageClassificationDto>().ReverseMap();
            CreateMap<UsageClassificationEntity, GetAllUsageClassificationsByCompanyIdDto>().ReverseMap();
            CreateMap<UsageClassificationEntity, GetUsageClassificationByIdDto>().ReverseMap();
            CreateMap<UsageClassificationEntity, GetUsageClassificationsByCompanyIdDto>().ReverseMap();

            //SupportType
            CreateMap<SupportTypeEntity, CreateSupportTypeDto>().ReverseMap();
            CreateMap<SupportTypeEntity, UpdateSupportTypeDto>().ReverseMap();
            CreateMap<SupportTypeEntity, GetSupportTypeByIdDto>().ReverseMap();
            CreateMap<SupportTypeEntity, GetAllSupportTypesByCompanyIdDto>().ReverseMap();
            CreateMap<SupportTypeEntity, GetSupportTypesByCompanyIdDto>().ReverseMap();

            //Location
            CreateMap<LocationEntity, CreateLocationDto>().ReverseMap();
            CreateMap<LocationEntity, UpdateLocationDto>().ReverseMap();
            CreateMap<LocationEntity, GetLocationByIdDto>().ReverseMap();
            CreateMap<LocationEntity, GetAllLocationsByCompanyIdDto>().ReverseMap();
            CreateMap<LocationEntity, GetLocationsByCompanyIdDto>().ReverseMap();

            //ImpactValuation
            CreateMap<ImpactValuationEntity, CreateImpactValuationDto>().ReverseMap();
            CreateMap<ImpactValuationEntity, UpdateImpactValuationDto>().ReverseMap();
            CreateMap<ImpactValuationEntity, GetImpactValuationByIdDto>().ReverseMap();
            CreateMap<ImpactValuationEntity, GetAllImpactValuationsByCompanyIdDto>().ReverseMap();
            CreateMap<ImpactValuationEntity, GetImpactValuationsByCompanyIdDto>().ReverseMap();

            //ActivesInventory
            CreateMap<ActivesInventoryEntity, CreateActivesInventoryDto>().ReverseMap();
            CreateMap<ActivesInventoryEntity, UpdateActivesInventoryDto>().ReverseMap();
            CreateMap<ActivesInventoryEntity, GetActivesInventoryByIdDto>().ReverseMap();

            CreateMap<ActivesInventoryEntity, GetActivesInventoriesByCompanyIdDto>().ReverseMap();
            CreateMap<ActiveTypeEntity, GetActivesInventoriesByCompanyIdActiveTypeDto>().ReverseMap();
            CreateMap<CustodianEntity, GetActivesInventoriesByCompanyIdCustodianDto>().ReverseMap();
            CreateMap<LocationEntity, GetActivesInventoriesByCompanyIdLocationDto>().ReverseMap();
            CreateMap<MacroprocessEntity, GetActivesInventoriesByCompanyIdMacroprocessDto>().ReverseMap();
            CreateMap<OwnerEntity, GetActivesInventoriesByCompanyIdOwnerDto>().ReverseMap();
            CreateMap<SubprocessEntity, GetActivesInventoriesByCompanyIdSubprocessDto>().ReverseMap();
            CreateMap<SupportTypeEntity, GetActivesInventoriesByCompanyIdSupportTypeDto>().ReverseMap();
            CreateMap<UsageClassificationEntity, GetActivesInventoriesByCompanyIdUsageClassificationDto>().ReverseMap();

            //ValuationInActive
            CreateMap<ValuationInActiveEntity, CreateValuationInActiveDto>().ReverseMap();
            CreateMap<ValuationInActiveEntity, UpdateValuationInActiveDto>().ReverseMap();
            CreateMap<ValuationInActiveEntity, GetValuationInActiveByIdDto>().ReverseMap();
            CreateMap<ValuationInActiveEntity, GetAllValuationInActivesByCompanyIdDto>().ReverseMap();
            CreateMap<ImpactValuationEntity, GetAllValuationInActivesByCompanyIdImpactValuationDto>().ReverseMap();
            CreateMap<ValuationInActiveEntity, GetValuationInActivesByActivesInventoryIdDto>().ReverseMap();
            CreateMap<ImpactValuationEntity, GetValuationInActivesByActivesInventoryIdImpactValuationDto>().ReverseMap();

            //Option
            CreateMap<OptionEntity, CreateOptionDto>().ReverseMap();
            CreateMap<OptionEntity, UpdateOptionDto>().ReverseMap();
            CreateMap<OptionEntity, GetOptionByIdDto>().ReverseMap();
            CreateMap<OptionEntity, GetAllOptionsByCompanyIdDto>().ReverseMap();
            CreateMap<OptionEntity, GetOptionsByCompanyIdDto>().ReverseMap();

            //Menu
            CreateMap<MenuEntity, CreateMenuDto>().ReverseMap();
            CreateMap<MenuEntity, UpdateMenuDto>().ReverseMap();
            CreateMap<MenuEntity, GetMenuByIdDto>().ReverseMap();
            CreateMap<MenuEntity, GetAllMenusByCompanyIdDto>().ReverseMap();
            CreateMap<MenuEntity, GetMenusByCompanyIdDto>().ReverseMap();

            CreateMap<MenuEntity, GetAllMenusByRoleIdDto>().ReverseMap();
            CreateMap<OptionEntity, GetAllMenusOptionByRoleIdDto>().ReverseMap();

            //Role
            CreateMap<RoleEntity, CreateRoleDto>().ReverseMap();
            CreateMap<RoleEntity, UpdateRoleDto>().ReverseMap();
            CreateMap<RoleEntity, GetRoleByIdDto>().ReverseMap();
            CreateMap<RoleEntity, GetAllRolesByCompanyIdDto>().ReverseMap();
            CreateMap<RoleEntity, GetRolesByCompanyIdDto>().ReverseMap();
            CreateMap<RoleEntity, GetUserByIdRoleDto>().ReverseMap();

            //MenuInRole
            CreateMap<MenuInRoleEntity, CreateMenuInRoleDto>().ReverseMap();
            CreateMap<MenuInRoleEntity, UpdateMenuInRoleDto>().ReverseMap();
            CreateMap<MenuInRoleEntity, GetMenuInRoleByIdDto>().ReverseMap();
            CreateMap<MenuInRoleEntity, GetAllMenuInRolesByRoleIdDto>().ReverseMap();
            CreateMap<MenuEntity, GetAllMenuInRolesByRoleIdMenuDto>().ReverseMap();
            CreateMap<MenuInRoleEntity, GetMenuInRolesByRoleIdDto>().ReverseMap();
            CreateMap<MenuEntity, GetMenuInRolesByRoleIdMenuDto>().ReverseMap();

            //OptionInMenu
            CreateMap<OptionInMenuEntity, CreateOptionInMenuDto>().ReverseMap();
            CreateMap<OptionInMenuEntity, UpdateOptionInMenuDto>().ReverseMap();
            CreateMap<OptionInMenuEntity, GetOptionInMenuByIdDto>().ReverseMap();
            CreateMap<OptionInMenuEntity, GetOptionInMenusByMenuIdDto>().ReverseMap();
            CreateMap<OptionEntity, GetOptionInMenusByMenuIdOptionDto>().ReverseMap();

            //OptionInMenuInRole
            CreateMap<OptionInMenuInRoleEntity, CreateOptionInMenuInRoleDto>().ReverseMap();

            //UserState
            CreateMap<UserStateEntity, CreateUserStateDto>().ReverseMap();
            CreateMap<UserStateEntity, UpdateUserStateDto>().ReverseMap();
            CreateMap<UserStateEntity, GetUserStateByIdDto>().ReverseMap();
            CreateMap<UserStateEntity, GetAllUserStatesByCompanyIdDto>().ReverseMap();
            CreateMap<UserStateEntity, GetUserStatesByCompanyIdDto>().ReverseMap();
            CreateMap<UserStateEntity, GetUserByIdUserStateDto>().ReverseMap();

            //User
            CreateMap<UserEntity, CreateUserDto>().ReverseMap();
            CreateMap<UserEntity, UpdateUserDto>().ReverseMap();
            CreateMap<UserEntity, GetUserByIdDto>().ReverseMap();
            CreateMap<UserEntity, GetAllUsersByCompanyIdDto>().ReverseMap();
            CreateMap<UserEntity, GetUsersByCompanyIdDto>().ReverseMap();

            //RoleInUser
            CreateMap<RoleInUserEntity, CreateRoleInUserDto>().ReverseMap();
            CreateMap<RoleInUserEntity, UpdateRoleInUserDto>().ReverseMap();
            CreateMap<RoleInUserEntity, GetRoleInUserByIdDto>().ReverseMap();
            CreateMap<RoleInUserEntity, GetAllRoleInUsersByUserIdDto>().ReverseMap();
            CreateMap<RoleEntity, GetAllRoleInUsersByUserIdRoleDto>().ReverseMap();
            CreateMap<RoleInUserEntity, GetRoleInUsersByUserIdDto>().ReverseMap();
            CreateMap<RoleEntity, GetRoleInUsersByUserIdRoleDto>().ReverseMap();

            //MenaceType
            CreateMap<MenaceTypeEntity, CreateMenaceTypeDto>().ReverseMap();
            CreateMap<MenaceTypeEntity, UpdateMenaceTypeDto>().ReverseMap();
            CreateMap<MenaceTypeEntity, GetMenaceTypeByIdDto>().ReverseMap();
            CreateMap<MenaceTypeEntity, GetAllMenaceTypesByCompanyIdDto>().ReverseMap();
            CreateMap<MenaceTypeEntity, GetMenaceTypesByCompanyIdDto>().ReverseMap();

            //Menace
            CreateMap<MenaceEntity, CreateMenaceDto>().ReverseMap();
            CreateMap<MenaceEntity, UpdateMenaceDto>().ReverseMap();
            CreateMap<MenaceEntity, GetMenaceByIdDto>().ReverseMap();
            CreateMap<MenaceEntity, GetAllMenacesByCompanyIdDto>().ReverseMap();
            CreateMap<MenaceTypeEntity, GetAllMenacesByCompanyIdMenaceTypeDto>().ReverseMap();
            CreateMap<MenaceEntity, GetMenacesByCompanyIdDto>().ReverseMap();
            CreateMap<MenaceTypeEntity, GetMenacesByCompanyIdMenaceTypeDto>().ReverseMap();

            //VulnerabilityType
            CreateMap<VulnerabilityTypeEntity, CreateVulnerabilityTypeDto>().ReverseMap();
            CreateMap<VulnerabilityTypeEntity, UpdateVulnerabilityTypeDto>().ReverseMap();
            CreateMap<VulnerabilityTypeEntity, GetVulnerabilityTypeByIdDto>().ReverseMap();
            CreateMap<VulnerabilityTypeEntity, GetAllVulnerabilityTypesByCompanyIdDto>().ReverseMap();
            CreateMap<VulnerabilityTypeEntity, GetVulnerabilityTypesByCompanyIdDto>().ReverseMap();

            //Vulnerability
            CreateMap<VulnerabilityEntity, CreateVulnerabilityDto>().ReverseMap();
            CreateMap<VulnerabilityEntity, UpdateVulnerabilityDto>().ReverseMap();
            CreateMap<VulnerabilityEntity, GetVulnerabilityByIdDto>().ReverseMap();
            CreateMap<VulnerabilityEntity, GetAllVulnerabilitiesByCompanyIdDto>().ReverseMap();
            CreateMap<VulnerabilityTypeEntity, GetAllVulnerabilitiesByCompanyIdVulnerabilityTypeDto>().ReverseMap();
            CreateMap<VulnerabilityEntity, GetVulnerabilitiesByCompanyIdDto>().ReverseMap();
            CreateMap<VulnerabilityTypeEntity, GetVulnerabilitiesByCompanyIdVulnerabilityTypeDto>().ReverseMap();

            //ControlType
            CreateMap<ControlTypeEntity, CreateControlTypeDto>().ReverseMap();
            CreateMap<ControlTypeEntity, UpdateControlTypeDto>().ReverseMap();
            CreateMap<ControlTypeEntity, GetControlTypeByIdDto>().ReverseMap();
            CreateMap<ControlTypeEntity, GetAllControlTypesByCompanyIdDto>().ReverseMap();
            CreateMap<ControlTypeEntity, GetControlTypesByCompanyIdDto>().ReverseMap();

            //RiskLevel
            CreateMap<RiskLevelEntity, CreateRiskLevelDto>().ReverseMap();
            CreateMap<RiskLevelEntity, UpdateRiskLevelDto>().ReverseMap();
            CreateMap<RiskLevelEntity, GetRiskLevelByIdDto>().ReverseMap();
            CreateMap<RiskLevelEntity, GetAllRiskLevelsByCompanyIdDto>().ReverseMap();
            CreateMap<RiskLevelEntity, GetRiskLevelsByCompanyIdDto>().ReverseMap();

            ////Risk
            //CreateMap<RiskEntity, CreateRiskDto>().ReverseMap();
            //CreateMap<RiskEntity, UpdateRiskDto>().ReverseMap();
            //CreateMap<RiskEntity, GetRiskByIdDto>().ReverseMap();
            //CreateMap<RiskEntity, GetRisksByCompanyIdDto>().ReverseMap();
            //CreateMap<ControlEntity, GetRisksByCompanyIdControlDto>().ReverseMap();
            //CreateMap<ControlTypeEntity, GetRisksByCompanyIdControlTypeDto>().ReverseMap();
            //CreateMap<MenaceEntity, GetRisksByCompanyIdMenaceDto>().ReverseMap();
            //CreateMap<ResponsibleEntity, GetRisksByCompanyIdResponsibleDto>().ReverseMap();
            //CreateMap<RiskLevelEntity, GetRisksByCompanyIdRiskLevelDto>().ReverseMap();
            //CreateMap<VulnerabilityEntity, GetRisksByCompanyIdVulnerabilityDto>().ReverseMap();
            
            //Company
            CreateMap<CompanyEntity, GetCompanyByIdDto>().ReverseMap();
            CreateMap<CompanyEntity, UpdateCompanyDto>().ReverseMap();

            //EvaluationState
            CreateMap<EvaluationStateEntity, GetCurrentEvaluationDtoEvaluationStateDto>().ReverseMap();
            CreateMap<ScopeEntity, GetCurrentScopeDto>().ReverseMap();
            CreateMap<PolicyEntity, GetCurrentPolicyDto>().ReverseMap();
            CreateMap<StandardEntity, GetCurrentEvaluationDtoStandardDto>().ReverseMap();

            //Scope
            CreateMap<ScopeEntity, CreateScopeDto>().ReverseMap();
            CreateMap<ScopeEntity, UpdateScopeDto>().ReverseMap();
            CreateMap<ScopeEntity, GetScopeByIdDto>().ReverseMap();
            CreateMap<ScopeEntity, GetAllScopesByStandardIdDto>().ReverseMap();
            CreateMap<ScopeEntity, GetScopesByStandardIdDto>().ReverseMap();

            //Policy
            CreateMap<PolicyEntity, CreatePolicyDto>().ReverseMap();
            CreateMap<PolicyEntity, UpdatePolicyDto>().ReverseMap();
            CreateMap<PolicyEntity, GetPolicyByIdDto>().ReverseMap();
            CreateMap<PolicyEntity, GetAllPoliciesByStandardIdDto>().ReverseMap();
            CreateMap<PolicyEntity, GetPoliciesByStandardIdDto>().ReverseMap();

            //RiskTreatmentMethod
            CreateMap<RiskTreatmentMethodEntity, CreateRiskTreatmentMethodDto>().ReverseMap();
            CreateMap<RiskTreatmentMethodEntity, UpdateRiskTreatmentMethodDto>().ReverseMap();
            CreateMap<RiskTreatmentMethodEntity, GetRiskTreatmentMethodByIdDto>().ReverseMap();
            CreateMap<RiskTreatmentMethodEntity, GetAllRiskTreatmentMethodsByCompanyIdDto>().ReverseMap();
            CreateMap<RiskTreatmentMethodEntity, GetRiskTreatmentMethodsByCompanyIdDto>().ReverseMap();

            //Risk
            CreateMap<RiskEntity, GetRisksByEvaluationIdDto>().ReverseMap();
            CreateMap<MenaceEntity, GetRisksByEvaluationIdMenaceDto>().ReverseMap();
            CreateMap<VulnerabilityEntity, GetRisksByEvaluationIdVulnerabilityDto>().ReverseMap();
            CreateMap<RiskEntity, CreateRiskDto>().ReverseMap();
            CreateMap<RiskEntity, UpdateRiskDto>().ReverseMap();
            CreateMap<RiskEntity, GetRiskByIdDto>().ReverseMap();
            CreateMap<RiskEntity, GetAllRisksByEvaluationIdDto>().ReverseMap();

            //ActivesInventory
            CreateMap<ActivesInventoryEntity, GetAllActivesInventoriesByCompanyIdDto>().ReverseMap();

            //RiskAssessment
            CreateMap<RiskAssessmentEntity, CreateRiskAssessmentDto>().ReverseMap();
            CreateMap<RiskAssessmentEntity, UpdateRiskAssessmentDto>().ReverseMap();
            CreateMap<RiskAssessmentEntity, GetRiskAssessmentByIdDto>().ReverseMap();


        }
    }
}








