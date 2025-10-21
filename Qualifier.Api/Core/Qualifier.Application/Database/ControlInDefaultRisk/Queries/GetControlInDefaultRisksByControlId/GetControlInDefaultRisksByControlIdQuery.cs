using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetControlInDefaultRisksByControlId
{
    public class GetControlInDefaultRisksByControlIdQuery : IGetControlInDefaultRisksByControlIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlInDefaultRisksByControlIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int controlId)
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
                                          defaultRisk = new DefaultRiskEntity
                                          {
                                              name = defaultRisk.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetControlInDefaultRisksByControlIdDto> baseResponseDto = new BaseResponseDto<GetControlInDefaultRisksByControlIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetControlInDefaultRisksByControlIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, controlId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int controlId)
        {
            var total = await (from controlInDefaultRisk in _databaseService.ControlInDefaultRisk
                               join defaultRisk in _databaseService.DefaultRisk on controlInDefaultRisk.defaultRisk equals defaultRisk
                               where ((controlInDefaultRisk.isDeleted == null || controlInDefaultRisk.isDeleted == false) && controlInDefaultRisk.controlId == controlId)
                               select new ControlInDefaultRiskEntity
                               {
                                   controlInDefaultRiskId = controlInDefaultRisk.controlInDefaultRiskId,
                               }).CountAsync();
            return total;
        }

    }
}

