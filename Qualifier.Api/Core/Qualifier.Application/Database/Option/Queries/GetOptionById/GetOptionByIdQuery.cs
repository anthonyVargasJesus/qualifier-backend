using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Option.Queries.GetOptionById
{
    public class GetOptionByIdQuery : IGetOptionByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetOptionByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int optionId)
        {
            try
            {
                var entity = await (from item in _databaseService.Option
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.optionId == optionId)
                                    select new OptionEntity()
                                    {
                                        optionId = item.optionId,
                                        name = item.name,
                                        image = item.image,
                                        url = item.url,
                                        isMobile = item.isMobile,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetOptionByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

