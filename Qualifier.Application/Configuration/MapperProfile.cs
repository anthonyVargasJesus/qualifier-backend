using AutoMapper;
using Qualifier.Domain.Entities;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelById;
using Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel;
using Qualifier.Application.Database.MaturityLevel.Commands.UpdateMaturityLevel;
using Qualifier.Application.Database.Indicator.Commands.CreateIndicator;
using Qualifier.Application.Database.Indicator.Commands.UpdateIndicator;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorById;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorsByCompanyId;
using Qualifier.Application.Database.User.Commands.Login;
using Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelsByCompanyId;
using Qualifier.Application.Database.User.Queries.GetMenus;
using Qualifier.Application.Database.Standard.Commands.CreateStandard;
using Qualifier.Application.Database.Standard.Commands.UpdateStandard;
using Qualifier.Application.Database.Standard.Queries.GetStandardById;
using Qualifier.Application.Database.Standard.Queries.GetStandardsByCompanyId;
using Qualifier.Application.Database.Standard.Queries.GetAllStandardsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Commands.CreateControlGroup;
using Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByCompanyId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupById;
using Qualifier.Application.Database.Control.Commands.UpdateControl;
using Qualifier.Application.Database.Control.Commands.CreateControl;
using Qualifier.Application.Database.Control.Queries.GetControlsByControlGroupId;
using Qualifier.Application.Database.Control.Queries.GetControlById;
using Qualifier.Application.Database.Requirement.Commands.CreateRequirement;
using Qualifier.Application.Database.Requirement.Commands.UpdateRequirement;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementById;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByStandardId;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByStandardId;
using Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId;
using Qualifier.Application.Database.Responsible.Commands.UpdateResponsible;
using Qualifier.Application.Database.Responsible.Commands.CreateResponsible;
using Qualifier.Application.Database.Responsible.Queries.GetResponsiblesByStandardId;
using Qualifier.Application.Database.Responsible.Queries.GetResponsibleById;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationById;
using Qualifier.Application.Database.Documentation.Commands.CreateDocumentation;
using Qualifier.Application.Database.Documentation.Commands.UpdateDocumentation;
using Qualifier.Application.Database.Evaluation.Commands.UpdateEvaluation;
using Qualifier.Application.Database.Evaluation.Commands.CreateEvaluation;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationById;
using Qualifier.Application.Database.RequirementEvaluation.Commands.CreateRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Commands.UpdateRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationsByRequirementId;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationById;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;
using Qualifier.Application.Database.Documentation.Queries.GetAllDocumentationsByStandardId;
using Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationById;
using Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using static Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation.CreateControlEvaluationDto;
using static Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation.UpdateControlEvaluationDto;
using Qualifier.Application.Database.Evaluation.Queries.GetDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetControlsDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetExcelDashboard;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByStandardId;
using Qualifier.Application.Database.DocumentType.Commands.CreateDocumentType;
using Qualifier.Application.Database.DocumentType.Commands.UpdateDocumentType;
using Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypeById;
using Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypesByCompanyId;
using Qualifier.Application.Database.DocumentType.Queries.GetAllDocumentTypesByCompanyId;
using Qualifier.Application.Database.DefaultSection.Commands.CreateDefaultSection;
using Qualifier.Application.Database.DefaultSection.Commands.UpdateDefaultSection;
using Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionById;
using Qualifier.Application.Database.DefaultSection.Queries.GetAllDefaultSectionsByDocumentTypeId;
using Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionsByDocumentTypeId;
using Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByCompanyId;
using Qualifier.Application.Database.ConfidentialityLevel.Commands.CreateConfidentialityLevel;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelById;
using Qualifier.Application.Database.ConfidentialityLevel.Commands.UpdateConfidentialityLevel;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetAllConfidentialityLevelsByCompanyId;
using Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelsByCompanyId;
using Qualifier.Application.Database.Version.Commands.CreateVersion;
using Qualifier.Application.Database.Version.Queries.GetVersionById;
using Qualifier.Application.Database.Version.Queries.GetVersionsByDocumentationId;
using Qualifier.Application.Database.Version.Commands.UpdateVersion;
using Qualifier.Application.Database.SupportForRequirement.Commands.CreateSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Commands.UpdateSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementById;
using Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementsByDocumentationId;
using Qualifier.Application.Database.SupportForControl.Commands.CreateSupportForControl;
using Qualifier.Application.Database.SupportForControl.Commands.UpdateSupportForControl;
using Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlsByDocumentationId;
using Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlById;
using Qualifier.Application.Database.Control.Queries.GetAllControlsByStandardId;
using Qualifier.Application.Database.Section.Commands.CreateSection;
using Qualifier.Application.Database.Section.Commands.UpdateSection;
using Qualifier.Application.Database.Section.Queries.GetSectionById;
using Qualifier.Application.Database.Section.Queries.GetAllSectionsByVersionId;
using Qualifier.Application.Database.Section.Queries.GetSectionsByVersionId;
using Qualifier.Application.Database.Personal.Commands.CreatePersonal;
using Qualifier.Application.Database.Personal.Commands.UpdatePersonal;
using Qualifier.Application.Database.Personal.Queries.GetPersonalById;
using Qualifier.Application.Database.Personal.Queries.GetAllPersonalsByCompanyId;
using Qualifier.Application.Database.Personal.Queries.GetPersonalsByCompanyId;
using Qualifier.Application.Database.Creator.Commands.CreateCreator;
using Qualifier.Application.Database.Creator.Commands.UpdateCreator;
using Qualifier.Application.Database.Creator.Queries.GetAllCreatorsByVersionId;
using Qualifier.Application.Database.Creator.Queries.GetCreatorById;
using Qualifier.Application.Database.Reviewer.Commands.CreateReviewer;
using Qualifier.Application.Database.Reviewer.Commands.UpdateReviewer;
using Qualifier.Application.Database.Reviewer.Queries.GetReviewerById;
using Qualifier.Application.Database.Reviewer.Queries.GetAllReviewersByVersionId;
using Qualifier.Application.Database.Approver.Commands.CreateApprover;
using Qualifier.Application.Database.Approver.Queries.GetApproverById;
using Qualifier.Application.Database.Approver.Queries.GetAllApproversByVersionId;


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

            //Requirement
            CreateMap<RequirementEntity, CreateRequirementDto>().ReverseMap();
            CreateMap<RequirementEntity, UpdateRequirementDto>().ReverseMap();
            CreateMap<RequirementEntity, GetRequirementsByStandardIdDto>().ReverseMap();
            CreateMap<RequirementEntity, GetRequirementByIdDto>().ReverseMap();
            CreateMap<RequirementEntity, GetAllRequirementsByStandardIdDto>().ReverseMap();
            CreateMap<RequirementEntity, GetDashboardRequirementDto>().ReverseMap();

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
            CreateMap<StandardEntity, GetEvaluationsByCompanyIdStandardDto>().ReverseMap();
            CreateMap<EvaluationEntity, GetEvaluationByIdDto>().ReverseMap();

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

        }
    }
}







