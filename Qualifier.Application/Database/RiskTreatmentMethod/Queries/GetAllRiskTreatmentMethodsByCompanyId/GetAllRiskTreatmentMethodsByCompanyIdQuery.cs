using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskTreatmentMethod.Queries.GetAllRiskTreatmentMethodsByCompanyId
{
    internal class GetAllRiskTreatmentMethodsByCompanyIdQuery : IGetAllRiskTreatmentMethodsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllRiskTreatmentMethodsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from riskTreatmentMethod in _databaseService.RiskTreatmentMethod
                                      where ((riskTreatmentMethod.isDeleted == null || riskTreatmentMethod.isDeleted == false) && riskTreatmentMethod.companyId == companyId)
                                      select new RiskTreatmentMethodEntity
                                      {
                                          riskTreatmentMethodId = riskTreatmentMethod.riskTreatmentMethodId,
                                          name = riskTreatmentMethod.name,
                                          companyId = riskTreatmentMethod.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllRiskTreatmentMethodsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllRiskTreatmentMethodsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllRiskTreatmentMethodsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

