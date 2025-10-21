using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelById
{
    public class GetRiskLevelByIdQuery : IGetRiskLevelByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskLevelByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int riskLevelId)
        {
            try
            {
                var entity = await (from item in _databaseService.RiskLevel
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.riskLevelId == riskLevelId)
                                    select new RiskLevelEntity()
                                    {
                                        riskLevelId = item.riskLevelId,
                                        name = item.name,
                                        description = item.description,
                                        abbreviation = item.abbreviation,
                                        factor = item.factor,
                                        minimum = item.minimum,
                                        maximum = item.maximum,
                                        color = item.color,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRiskLevelByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

