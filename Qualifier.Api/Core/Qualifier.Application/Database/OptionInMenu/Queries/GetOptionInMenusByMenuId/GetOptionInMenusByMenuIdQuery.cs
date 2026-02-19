using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenusByMenuId
{
    public class GetOptionInMenusByMenuIdQuery : IGetOptionInMenusByMenuIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetOptionInMenusByMenuIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int menuId)
        {
            try
            {
                var entities = await (from optionInMenu in _databaseService.OptionInMenu
                                      join option in _databaseService.Option on optionInMenu.option equals option
                                      where ((optionInMenu.isDeleted == null || optionInMenu.isDeleted == false) && optionInMenu.menuId == menuId)
                                      && (optionInMenu.option.name.ToUpper().Contains(search.ToUpper()))
                                      select new OptionInMenuEntity
                                      {
                                          optionInMenuId = optionInMenu.optionInMenuId,
                                          menuId = optionInMenu.menuId,
                                          optionId = optionInMenu.optionId,
                                          order = optionInMenu.order,
                                          companyId = optionInMenu.companyId,
                                          option = new OptionEntity
                                          {
                                              name = option.name,
                                          },
                                      })
                                      .OrderBy(option => option.order)
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetOptionInMenusByMenuIdDto> baseResponseDto = new BaseResponseDto<GetOptionInMenusByMenuIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetOptionInMenusByMenuIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, menuId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int menuId)
        {
            var total = await (from optionInMenu in _databaseService.OptionInMenu
                               join option in _databaseService.Option on optionInMenu.option equals option
                               where ((optionInMenu.isDeleted == null || optionInMenu.isDeleted == false) && optionInMenu.menuId == menuId)
                               && (optionInMenu.option.name.ToUpper().Contains(search.ToUpper()))
                               select new OptionInMenuEntity
                               {
                                   optionInMenuId = optionInMenu.optionInMenuId,
                               }).CountAsync();
            return total;
        }

    }
}

