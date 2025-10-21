using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MenuInRole.Queries.GetAllMenuInRolesByRoleId
{
    internal class GetAllMenuInRolesByRoleIdQuery : IGetAllMenuInRolesByRoleIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllMenuInRolesByRoleIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int roleId)
        {
            try
            {
                var entities = await (from menuInRole in _databaseService.MenuInRole
                                      join menu in _databaseService.Menu on menuInRole.menu equals menu
                                      where ((menuInRole.isDeleted == null || menuInRole.isDeleted == false) && menuInRole.roleId == roleId)
                                      select new MenuInRoleEntity
                                      {
                                          menuInRoleId = menuInRole.menuInRoleId,
                                          menuId = menuInRole.menuId,
                                          order = menuInRole.order,
                                      }).ToListAsync();

                BaseResponseDto<GetAllMenuInRolesByRoleIdDto> baseResponseDto = new BaseResponseDto<GetAllMenuInRolesByRoleIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllMenuInRolesByRoleIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

