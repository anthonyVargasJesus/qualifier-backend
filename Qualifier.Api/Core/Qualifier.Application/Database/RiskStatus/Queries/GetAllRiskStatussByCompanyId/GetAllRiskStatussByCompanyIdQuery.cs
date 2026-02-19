using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskStatus.Queries.GetAllRiskStatussByCompanyId
{
    internal class GetAllRiskStatussByCompanyIdQuery : IGetAllRiskStatussByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllRiskStatussByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from riskStatus in _databaseService.RiskStatus
                                      where ((riskStatus.isDeleted == null || riskStatus.isDeleted == false) && riskStatus.companyId == companyId)
                                      select new RiskStatusEntity
                                      {
                                          riskStatusId = riskStatus.riskStatusId,
                                          name = riskStatus.name,
                                          description = riskStatus.description,
                                          abbreviation = riskStatus.abbreviation,
                                          value = riskStatus.value,
                                          color = riskStatus.color,
                                          companyId = riskStatus.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllRiskStatussByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllRiskStatussByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllRiskStatussByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception EX)
            {
                throw EX;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

