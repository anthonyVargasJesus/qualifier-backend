using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ResidualRisk.Queries.GetAllResidualRisksByCompanyId
{
    internal class GetAllResidualRisksByCompanyIdQuery : IGetAllResidualRisksByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllResidualRisksByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from residualRisk in _databaseService.ResidualRisk
                                      where ((residualRisk.isDeleted == null || residualRisk.isDeleted == false) && residualRisk.companyId == companyId)
                                      select new ResidualRiskEntity
                                      {
                                          residualRiskId = residualRisk.residualRiskId,
                                          name = residualRisk.name,
                                          description = residualRisk.description,
                                          abbreviation = residualRisk.abbreviation,
                                          factor = residualRisk.factor,
                                          minimum = residualRisk.minimum,
                                          maximum = residualRisk.maximum,
                                          color = residualRisk.color,
                                          companyId = residualRisk.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllResidualRisksByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllResidualRisksByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllResidualRisksByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

