using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.BreachSeverity.Queries.GetAllBreachSeveritiesByCompanyId
{
    internal class GetAllBreachSeveritiesByCompanyIdQuery : IGetAllBreachSeveritiesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllBreachSeveritiesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from breachSeverity in _databaseService.BreachSeverity
                                      where ((breachSeverity.isDeleted == null || breachSeverity.isDeleted == false) && breachSeverity.companyId == companyId)
                                      select new BreachSeverityEntity
                                      {
                                          breachSeverityId = breachSeverity.breachSeverityId,
                                          name = breachSeverity.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllBreachSeveritiesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllBreachSeveritiesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllBreachSeveritiesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

