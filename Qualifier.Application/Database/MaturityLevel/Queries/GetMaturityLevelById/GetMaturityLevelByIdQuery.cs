using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelById
{
    public class GetMaturityLevelByIdQuery : IGetMaturityLevelByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMaturityLevelByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int maturityLevelId)
        {
            try
            {
                var entity = await (from item in _databaseService.MaturityLevel
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.maturityLevelId == maturityLevelId)
                                    select new MaturityLevelEntity()
                                    {
                                        maturityLevelId = item.maturityLevelId,
                                        name = item.name,
                                        description = item.description,
                                        abbreviation = item.abbreviation,
                                        value = item.value,
                                        color = item.color,

                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetMaturityLevelByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
     
        }

    }
}
