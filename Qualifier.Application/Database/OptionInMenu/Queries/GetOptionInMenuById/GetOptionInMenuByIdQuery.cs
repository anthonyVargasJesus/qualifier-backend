using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenuById
{
    public class GetOptionInMenuByIdQuery : IGetOptionInMenuByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetOptionInMenuByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int optionInMenuId)
        {
            try
            {
                var entity = await (from item in _databaseService.OptionInMenu
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.optionInMenuId == optionInMenuId)
                                    select new OptionInMenuEntity()
                                    {
                                        optionInMenuId = item.optionInMenuId,
                                        menuId = item.menuId,
                                        optionId = item.optionId,
                                        order = item.order,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetOptionInMenuByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

