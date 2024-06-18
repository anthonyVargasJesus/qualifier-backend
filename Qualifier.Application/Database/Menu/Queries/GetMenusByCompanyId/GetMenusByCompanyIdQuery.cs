using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Menu.Queries.GetMenusByCompanyId
{
    public class GetMenusByCompanyIdQuery : IGetMenusByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenusByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from menu in _databaseService.Menu
                                      where ((menu.isDeleted == null || menu.isDeleted == false) && menu.companyId == companyId)
                                      && (menu.name.ToUpper().Contains(search.ToUpper()))
                                      select new MenuEntity
                                      {
                                          menuId = menu.menuId,
                                          name = menu.name,
                                          image = menu.image,
                                          companyId = menu.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetMenusByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetMenusByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetMenusByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from menu in _databaseService.Menu
                               where ((menu.isDeleted == null || menu.isDeleted == false) && menu.companyId == companyId)
                               && (menu.name.ToUpper().Contains(search.ToUpper()))
                               select new MenuEntity
                               {
                                   menuId = menu.menuId,
                               }).CountAsync();
            return total;
        }

    }
}

