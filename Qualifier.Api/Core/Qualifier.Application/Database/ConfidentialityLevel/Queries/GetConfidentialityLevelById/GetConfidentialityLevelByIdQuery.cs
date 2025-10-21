using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ConfidentialityLevel.Queries.GetConfidentialityLevelById
{
    public class GetConfidentialityLevelByIdQuery : IGetConfidentialityLevelByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetConfidentialityLevelByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int confidentialityLevelId)
        {
            try
            {
                var entity = await (from item in _databaseService.ConfidentialityLevel
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.confidentialityLevelId == confidentialityLevelId)
                                    select new ConfidentialityLevelEntity()
                                    {
                                        confidentialityLevelId = item.confidentialityLevelId,
                                        name = item.name,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetConfidentialityLevelByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

