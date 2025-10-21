using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActiveType.Queries.GetActiveTypeById
{
    public class GetActiveTypeByIdQuery : IGetActiveTypeByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActiveTypeByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int activeTypeId)
        {
            try
            {
                var entity = await (from item in _databaseService.ActiveType
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.activeTypeId == activeTypeId)
                                    select new ActiveTypeEntity()
                                    {
                                        activeTypeId = item.activeTypeId,
                                        name = item.name,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetActiveTypeByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

