using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlImplementation.Queries.GetControlImplementationsByRiskId
{
    public class GetControlImplementationsByRiskIdQuery : IGetControlImplementationsByRiskIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlImplementationsByRiskIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int riskId)
        {
            try
            {
                var entities = await (from controlImplementation in _databaseService.ControlImplementation
                                      join responsible in _databaseService.Responsible on controlImplementation.responsible equals responsible
                                      join risk in _databaseService.Risk on controlImplementation.risk equals risk
                                      where ((controlImplementation.isDeleted == null || controlImplementation.isDeleted == false) && controlImplementation.riskId == riskId)
                                      && (controlImplementation.activities.ToUpper().Contains(search.ToUpper()))
                                      select new ControlImplementationEntity
                                      {
                                          controlImplementationId = controlImplementation.controlImplementationId,
                                          activities = controlImplementation.activities,
                                          startDate = controlImplementation.startDate,
                                          verificationDate = controlImplementation.verificationDate,
                                          responsibleId = controlImplementation.responsibleId,
                                          observation = controlImplementation.observation,
                                          isImplemented = controlImplementation.isImplemented == null ? false : controlImplementation.isImplemented,
                                          isEffective = controlImplementation.isEffective == null ? false : controlImplementation.isEffective,
                                          responsible = new ResponsibleEntity
                                          {
                                              name = responsible.name,
                                          },
                                          risk = new RiskEntity
                                          {
                                              name = risk.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetControlImplementationsByRiskIdDto> baseResponseDto = new BaseResponseDto<GetControlImplementationsByRiskIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetControlImplementationsByRiskIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, riskId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int riskId)
        {
            var total = await (from controlImplementation in _databaseService.ControlImplementation
                               join responsible in _databaseService.Responsible on controlImplementation.responsible equals responsible
                               join risk in _databaseService.Risk on controlImplementation.risk equals risk
                               where ((controlImplementation.isDeleted == null || controlImplementation.isDeleted == false) && controlImplementation.riskId == riskId)
                               && (controlImplementation.activities.ToUpper().Contains(search.ToUpper()))
                               select new ControlImplementationEntity
                               {
                                   controlImplementationId = controlImplementation.controlImplementationId,
                               }).CountAsync();
            return total;
        }

    }
}

