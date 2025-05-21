using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ResidualRisk.Queries.GetResidualRiskById
{
    public class GetResidualRiskByIdQuery : IGetResidualRiskByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetResidualRiskByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int residualRiskId)
        {
            try
            {
                var entity = await (from item in _databaseService.ResidualRisk
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.residualRiskId == residualRiskId)
                                    select new ResidualRiskEntity()
                                    {
                                        residualRiskId = item.residualRiskId,
                                        name = item.name,
                                        description = item.description,
                                        abbreviation = item.abbreviation,
                                        factor = item.factor,
                                        minimum = item.minimum,
                                        maximum = item.maximum,
                                        color = item.color,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetResidualRiskByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

