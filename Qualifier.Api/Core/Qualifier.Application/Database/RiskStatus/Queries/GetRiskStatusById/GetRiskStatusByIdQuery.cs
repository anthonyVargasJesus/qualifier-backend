using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskStatus.Queries.GetRiskStatusById
{
    public class GetRiskStatusByIdQuery : IGetRiskStatusByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskStatusByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int riskStatusId)
        {
            try
            {
                var entity = await (from item in _databaseService.RiskStatus
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.riskStatusId == riskStatusId)
                                    select new RiskStatusEntity()
                                    {
                                        riskStatusId = item.riskStatusId,
                                        name = item.name,
                                        description = item.description,
                                        abbreviation = item.abbreviation,
                                        value = item.value,
                                        color = item.color,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRiskStatusByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

