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

            //Indicator
            CreateMap<IndicatorEntity, CreateIndicatorDto>().ReverseMap();
            CreateMap<IndicatorEntity, UpdateIndicatorDto>().ReverseMap();
            CreateMap<IndicatorEntity, GetIndicatorByIdDto>().ReverseMap();
            CreateMap<IndicatorEntity, GetIndicatorsByCompanyIdDto>().ReverseMap();

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
        }
    }
}







