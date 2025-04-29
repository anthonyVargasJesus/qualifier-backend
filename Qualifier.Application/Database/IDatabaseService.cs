using Microsoft.EntityFrameworkCore;
using Qualifier.Domain.Entities;
using System.Collections.Generic;

namespace Qualifier.Application.Database
{
    public interface IDatabaseService
    {
        DbSet<MaturityLevelEntity> MaturityLevel { get; set; }
        DbSet<IndicatorEntity> Indicator { get; set; }
        DbSet<RoleEntity> Role { get; set; }
        DbSet<RoleInUserEntity> RoleInUser { get; set; }
        DbSet<MenuEntity> Menu { get; set; }
        DbSet<MenuInRoleEntity> MenuInRole { get; set; }
        DbSet<OptionEntity> Option { get; set; }
        DbSet<OptionInMenuInRoleEntity> OptionInMenuInRole { get; set; }
        DbSet<UserStateEntity> UserState { get; set; }
        DbSet<UserEntity> User { get; set; }
        DbSet<StandardEntity> Standard { get; set; }
        DbSet<ControlGroupEntity> ControlGroup { get; set; }
        DbSet<ControlEntity> Control { get; set; }
        DbSet<RequirementEntity> Requirement { get; set; }
        DbSet<ResponsibleEntity> Responsible { get; set; }
        DbSet<DocumentationEntity> Documentation { get; set; }
        DbSet<EvaluationEntity> Evaluation { get; set; }
        DbSet<RequirementEvaluationEntity> RequirementEvaluation { get; set; }
        DbSet<ReferenceDocumentationEntity> ReferenceDocumentation { get; set; }
        DbSet<ControlEvaluationEntity> ControlEvaluation { get; set; }
        DbSet<DocumentTypeEntity> DocumentType { get; set; }
        DbSet<DefaultSectionEntity> DefaultSection { get; set; }
        DbSet<ConfidentialityLevelEntity> ConfidentialityLevel { get; set; }
        DbSet<VersionEntity> Version { get; set; }
        DbSet<SupportForRequirementEntity> SupportForRequirement { get; set; }
        DbSet<SupportForControlEntity> SupportForControl { get; set; }
        DbSet<SectionEntity> Section { get; set; }
        DbSet<PersonalEntity> Personal { get; set; }
        DbSet<CreatorEntity> Creator { get; set; }
        DbSet<ReviewerEntity> Reviewer { get; set; }
        DbSet<ApproverEntity> Approver { get; set; }
        DbSet<MacroprocessEntity> Macroprocess { get; set; }
        DbSet<SubprocessEntity> Subprocess { get; set; }
        DbSet<ActiveTypeEntity> ActiveType { get; set; }
        DbSet<OwnerEntity> Owner { get; set; }
        DbSet<CustodianEntity> Custodian { get; set; }
        DbSet<UsageClassificationEntity> UsageClassification { get; set; }
        DbSet<SupportTypeEntity> SupportType { get; set; }
        DbSet<LocationEntity> Location { get; set; }
        DbSet<ImpactValuationEntity> ImpactValuation { get; set; }
        DbSet<ActivesInventoryEntity> ActivesInventory { get; set; }
        DbSet<ValuationInActiveEntity> ValuationInActive { get; set; }
        DbSet<OptionInMenuEntity> OptionInMenu { get; set; }
        DbSet<MenaceTypeEntity> MenaceType { get; set; }
        DbSet<MenaceEntity> Menace { get; set; }
        DbSet<VulnerabilityTypeEntity> VulnerabilityType { get; set; }
        DbSet<VulnerabilityEntity> Vulnerability { get; set; }
        DbSet<ControlTypeEntity> ControlType { get; set; }
        DbSet<RiskLevelEntity> RiskLevel { get; set; }
        DbSet<RiskEntity> Risk { get; set; }
        DbSet<CompanyEntity> Company { get; set; }
        DbSet<EvaluationStateEntity> EvaluationState { get; set; }
        DbSet<ScopeEntity> Scope { get; set; }
        DbSet<PolicyEntity> Policy { get; set; }
        DbSet<RiskTreatmentMethodEntity> RiskTreatmentMethod { get; set; }
        DbSet<RiskAssessmentEntity> RiskAssessment { get; set; }
        Task<bool> SaveAsync();
    }
}
