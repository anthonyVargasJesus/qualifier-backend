using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database;
using Qualifier.Domain.Entities;
using Qualifier.Persistence.Configuration;


namespace Qualifier.Persistence.Database
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        public DatabaseService(DbContextOptions options) : base(options)
        {

        }
        public DbSet<MaturityLevelEntity> MaturityLevel { get; set; }
        public DbSet<IndicatorEntity> Indicator { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<RoleInUserEntity> RoleInUser { get; set; }
        public DbSet<MenuEntity> Menu { get; set; }
        public DbSet<MenuInRoleEntity> MenuInRole { get; set; }
        public DbSet<OptionEntity> Option { get; set; }
        public DbSet<OptionInMenuInRoleEntity> OptionInMenuInRole { get; set; }
        public DbSet<UserStateEntity> UserState { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<StandardEntity> Standard { get; set; }
        public DbSet<ControlGroupEntity> ControlGroup { get; set; }
        public DbSet<ControlEntity> Control { get; set; }
        public DbSet<RequirementEntity> Requirement { get; set; }
        public DbSet<ResponsibleEntity> Responsible { get; set; }
        public DbSet<DocumentationEntity> Documentation { get; set; }
        public DbSet<EvaluationEntity> Evaluation { get; set; }
        public DbSet<RequirementEvaluationEntity> RequirementEvaluation { get; set; }
        public DbSet<ReferenceDocumentationEntity> ReferenceDocumentation { get; set; }
        public DbSet<ControlEvaluationEntity> ControlEvaluation { get; set; }
        public DbSet<DocumentTypeEntity> DocumentType { get; set; }
        public DbSet<DefaultSectionEntity> DefaultSection { get; set; }
        public DbSet<ConfidentialityLevelEntity> ConfidentialityLevel { get; set; }
        public DbSet<VersionEntity> Version { get; set; }
        public DbSet<SupportForRequirementEntity> SupportForRequirement { get; set; }
        public DbSet<SupportForControlEntity> SupportForControl { get; set; }
        public DbSet<SectionEntity> Section { get; set; }
        public DbSet<PersonalEntity> Personal { get; set; }
        public DbSet<CreatorEntity> Creator { get; set; }
        public DbSet<ReviewerEntity> Reviewer { get; set; }
        public DbSet<ApproverEntity> Approver { get; set; }
        public DbSet<MacroprocessEntity> Macroprocess { get; set; }
        public DbSet<SubprocessEntity> Subprocess { get; set; }
        public DbSet<ActiveTypeEntity> ActiveType { get; set; }
        public DbSet<OwnerEntity> Owner { get; set; }
        public DbSet<CustodianEntity> Custodian { get; set; }
        public DbSet<UsageClassificationEntity> UsageClassification { get; set; }
        public DbSet<SupportTypeEntity> SupportType { get; set; }
        public DbSet<LocationEntity> Location { get; set; }
        public DbSet<ImpactValuationEntity> ImpactValuation { get; set; }
        public DbSet<ActivesInventoryEntity> ActivesInventory { get; set; }
        public DbSet<ValuationInActiveEntity> ValuationInActive { get; set; }
        public DbSet<OptionInMenuEntity> OptionInMenu { get; set; }
        public DbSet<MenaceTypeEntity> MenaceType { get; set; }
        public DbSet<MenaceEntity> Menace { get; set; }
        public DbSet<VulnerabilityTypeEntity> VulnerabilityType { get; set; }
        public DbSet<VulnerabilityEntity> Vulnerability { get; set; }
        public DbSet<ControlTypeEntity> ControlType { get; set; }
        public DbSet<RiskLevelEntity> RiskLevel { get; set; }
        public DbSet<RiskEntity> Risk { get; set; }
        public DbSet<CompanyEntity> Company { get; set; }
        public DbSet<EvaluationStateEntity> EvaluationState { get; set; }
        public DbSet<ScopeEntity> Scope { get; set; }
        public DbSet<PolicyEntity> Policy { get; set; }
        public DbSet<RiskTreatmentMethodEntity> RiskTreatmentMethod { get; set; }
        public DbSet<RiskAssessmentEntity> RiskAssessment { get; set; }
        public DbSet<RiskTreatmentEntity> RiskTreatment { get; set; }
        public DbSet<ResidualRiskEntity> ResidualRisk { get; set; }
        public DbSet<ControlImplementationEntity> ControlImplementation { get; set; }
        

        public async Task<bool> SaveAsync()
        {

            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }

        private void EntityConfiguration(ModelBuilder modelBuilder)
        {
            new MaturityLevelConfiguration(modelBuilder.Entity<MaturityLevelEntity>());
            new IndicatorConfiguration(modelBuilder.Entity<IndicatorEntity>());
            new RoleConfiguration(modelBuilder.Entity<RoleEntity>());
            new RoleInUserConfiguration(modelBuilder.Entity<RoleInUserEntity>());
            new MenuConfiguration(modelBuilder.Entity<MenuEntity>());
            new MenuInRoleConfiguration(modelBuilder.Entity<MenuInRoleEntity>());
            new OptionConfiguration(modelBuilder.Entity<OptionEntity>());
            new OptionInMenuInRoleConfiguration(modelBuilder.Entity<OptionInMenuInRoleEntity>());
            new UserStateConfiguration(modelBuilder.Entity<UserStateEntity>());
            new UserConfiguration(modelBuilder.Entity<UserEntity>());
            new StandardConfiguration(modelBuilder.Entity<StandardEntity>());
            new ControlGroupConfiguration(modelBuilder.Entity<ControlGroupEntity>());
            new ControlConfiguration(modelBuilder.Entity<ControlEntity>());
            new RequirementConfiguration(modelBuilder.Entity<RequirementEntity>());
            new ResponsibleConfiguration(modelBuilder.Entity<ResponsibleEntity>());
            new DocumentationConfiguration(modelBuilder.Entity<DocumentationEntity>());
            new EvaluationConfiguration(modelBuilder.Entity<EvaluationEntity>());
            new RequirementEvaluationConfiguration(modelBuilder.Entity<RequirementEvaluationEntity>());
            new ReferenceDocumentationConfiguration(modelBuilder.Entity<ReferenceDocumentationEntity>());
            new ControlEvaluationConfiguration(modelBuilder.Entity<ControlEvaluationEntity>());
            new DocumentTypeConfiguration(modelBuilder.Entity<DocumentTypeEntity>());
            new DefaultSectionConfiguration(modelBuilder.Entity<DefaultSectionEntity>());
            new ConfidentialityLevelConfiguration(modelBuilder.Entity<ConfidentialityLevelEntity>());
            new VersionConfiguration(modelBuilder.Entity<VersionEntity>());
            new SupportForRequirementConfiguration(modelBuilder.Entity<SupportForRequirementEntity>());
            new SupportForControlConfiguration(modelBuilder.Entity<SupportForControlEntity>());
            new SectionConfiguration(modelBuilder.Entity<SectionEntity>());
            new PersonalConfiguration(modelBuilder.Entity<PersonalEntity>());
            new CreatorConfiguration(modelBuilder.Entity<CreatorEntity>());
            new ReviewerConfiguration(modelBuilder.Entity<ReviewerEntity>());
            new ApproverConfiguration(modelBuilder.Entity<ApproverEntity>());
            new MacroprocessConfiguration(modelBuilder.Entity<MacroprocessEntity>());
            new SubprocessConfiguration(modelBuilder.Entity<SubprocessEntity>());
            new ActiveTypeConfiguration(modelBuilder.Entity<ActiveTypeEntity>());
            new OwnerConfiguration(modelBuilder.Entity<OwnerEntity>());
            new CustodianConfiguration(modelBuilder.Entity<CustodianEntity>());
            new UsageClassificationConfiguration(modelBuilder.Entity<UsageClassificationEntity>());
            new SupportTypeConfiguration(modelBuilder.Entity<SupportTypeEntity>());
            new LocationConfiguration(modelBuilder.Entity<LocationEntity>());
            new ImpactValuationConfiguration(modelBuilder.Entity<ImpactValuationEntity>());
            new ActivesInventoryConfiguration(modelBuilder.Entity<ActivesInventoryEntity>());
            new ValuationInActiveConfiguration(modelBuilder.Entity<ValuationInActiveEntity>());
            new OptionInMenuConfiguration(modelBuilder.Entity<OptionInMenuEntity>());
            new MenaceTypeConfiguration(modelBuilder.Entity<MenaceTypeEntity>());
            new MenaceConfiguration(modelBuilder.Entity<MenaceEntity>());
            new VulnerabilityTypeConfiguration(modelBuilder.Entity<VulnerabilityTypeEntity>());
            new VulnerabilityConfiguration(modelBuilder.Entity<VulnerabilityEntity>());
            new ControlTypeConfiguration(modelBuilder.Entity<ControlTypeEntity>());
            new RiskLevelConfiguration(modelBuilder.Entity<RiskLevelEntity>());
            new RiskConfiguration(modelBuilder.Entity<RiskEntity>());
            new CompanyConfiguration(modelBuilder.Entity<CompanyEntity>());
            new EvaluationStateConfiguration(modelBuilder.Entity<EvaluationStateEntity>());
            new ScopeConfiguration(modelBuilder.Entity<ScopeEntity>());
            new PolicyConfiguration(modelBuilder.Entity<PolicyEntity>());
            new RiskTreatmentMethodConfiguration(modelBuilder.Entity<RiskTreatmentMethodEntity>());
            new RiskAssessmentConfiguration(modelBuilder.Entity<RiskAssessmentEntity>());
            new RiskTreatmentConfiguration(modelBuilder.Entity<RiskTreatmentEntity>());
            new ResidualRiskConfiguration(modelBuilder.Entity<ResidualRiskEntity>());
            new ControlImplementationConfiguration(modelBuilder.Entity<ControlImplementationEntity>());

        }

    }
}
