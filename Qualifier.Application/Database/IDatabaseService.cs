using Microsoft.EntityFrameworkCore;
using Qualifier.Domain.Entities;

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
        Task<bool> SaveAsync();
    }
}
