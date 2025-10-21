using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRolesByRoleId
{
    public class GetMenuInRolesByRoleIdQuery : IGetMenuInRolesByRoleIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenuInRolesByRoleIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int roleId)
        {
            try
            {
                var entities = await (from menuInRole in _databaseService.MenuInRole
                                      join menu in _databaseService.Menu on menuInRole.menu equals menu
                                      where ((menuInRole.isDeleted == null || menuInRole.isDeleted == false) && menuInRole.roleId == roleId)
                                      && (menuInRole.menu.name.ToUpper().Contains(search.ToUpper()))
                                      select new MenuInRoleEntity
                                      {
                                          menuInRoleId = menuInRole.menuInRoleId,
                                          menuId = menuInRole.menuId,
                                          order = menuInRole.order,
                                          menu = new MenuEntity
                                          {
                                              name = menu.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetMenuInRolesByRoleIdDto> baseResponseDto = new BaseResponseDto<GetMenuInRolesByRoleIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetMenuInRolesByRoleIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, roleId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int roleId)
        {
            var total = await (from menuInRole in _databaseService.MenuInRole
                               join menu in _databaseService.Menu on menuInRole.menu equals menu
                               where ((menuInRole.isDeleted == null || menuInRole.isDeleted == false) && menuInRole.roleId == roleId)
                               && (menuInRole.menu.name.ToUpper().Contains(search.ToUpper()))
                               select new MenuInRoleEntity
                               {
                                   menuInRoleId = menuInRole.menuInRoleId,
                               }).CountAsync();
            return total;
        }

    }
}

