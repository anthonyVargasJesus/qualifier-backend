using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetAllControlInDefaultRisksByControlId
{
    internal class GetAllControlInDefaultRisksByControlIdQuery : IGetAllControlInDefaultRisksByControlIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllControlInDefaultRisksByControlIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int controlId)
        {
            try
            {
                var entities = await (from controlInDefaultRisk in _databaseService.ControlInDefaultRisk
                                      join defaultRisk in _databaseService.DefaultRisk on controlInDefaultRisk.defaultRisk equals defaultRisk
                                      where ((controlInDefaultRisk.isDeleted == null || controlInDefaultRisk.isDeleted == false) && controlInDefaultRisk.controlId == controlId)
                                      select new ControlInDefaultRiskEntity
                                      {
                                          controlInDefaultRiskId = controlInDefaultRisk.controlInDefaultRiskId,
                                          defaultRiskId = controlInDefaultRisk.defaultRiskId,
                                          controlId = controlInDefaultRisk.controlId,
                                          isActive = controlInDefaultRisk.isActive,
                                          companyId = controlInDefaultRisk.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllControlInDefaultRisksByControlIdDto> baseResponseDto = new BaseResponseDto<GetAllControlInDefaultRisksByControlIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllControlInDefaultRisksByControlIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

