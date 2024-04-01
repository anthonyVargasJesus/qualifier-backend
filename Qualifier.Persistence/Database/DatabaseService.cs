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

        }

    }
}
