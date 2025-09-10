using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DefaultRisk.Queries.GetDefaultRiskById
{
    public class GetDefaultRiskByIdQuery : IGetDefaultRiskByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDefaultRiskByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int defaultRiskId)
        {
            try
            {
                var entity = await (from item in _databaseService.DefaultRisk
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.defaultRiskId == defaultRiskId)
                                    select new DefaultRiskEntity()
                                    {
                                        defaultRiskId = item.defaultRiskId,
                                        standardId = item.standardId,
                                        name = item.name,
                                        menaceId = item.menaceId,
                                        vulnerabilityId = item.vulnerabilityId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetDefaultRiskByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

