using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ImpactValuation.Queries.GetAllImpactValuationsByCompanyId
{
    internal class GetAllImpactValuationsByCompanyIdQuery : IGetAllImpactValuationsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllImpactValuationsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from impactValuation in _databaseService.ImpactValuation
                                      where ((impactValuation.isDeleted == null || impactValuation.isDeleted == false) && impactValuation.companyId == companyId)
                                      select new ImpactValuationEntity
                                      {
                                          impactValuationId = impactValuation.impactValuationId,
                                          abbreviation = impactValuation.abbreviation,
                                          name = impactValuation.name,
                                          minimumValue = impactValuation.minimumValue,
                                          maximumValue = impactValuation.maximumValue,
                                          defaultValue = impactValuation.defaultValue,
                                          companyId = impactValuation.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllImpactValuationsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllImpactValuationsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllImpactValuationsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

