using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Menu.Queries.GetMenuById
{
    public class GetMenuByIdQuery : IGetMenuByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenuByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int menuId)
        {
            try
            {
                var entity = await (from item in _databaseService.Menu
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.menuId == menuId)
                                    select new MenuEntity()
                                    {
                                        menuId = item.menuId,
                                        name = item.name,
                                        image = item.image,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetMenuByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

