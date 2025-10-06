using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlanPriority.Queries.GetAllActionPlanPrioritiesByCompanyId
{
    internal class GetAllActionPlanPrioritiesByCompanyIdQuery : IGetAllActionPlanPrioritiesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllActionPlanPrioritiesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from actionPlanPriority in _databaseService.ActionPlanPriority
                                      where ((actionPlanPriority.isDeleted == null || actionPlanPriority.isDeleted == false) && actionPlanPriority.companyId == companyId)
                                      select new ActionPlanPriorityEntity
                                      {
                                          actionPlanPriorityId = actionPlanPriority.actionPlanPriorityId,
                                          name = actionPlanPriority.name,
                                          description = actionPlanPriority.description,
                                          abbreviation = actionPlanPriority.abbreviation,
                                          value = actionPlanPriority.value,
                                          color = actionPlanPriority.color,
                                          companyId = actionPlanPriority.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllActionPlanPrioritiesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllActionPlanPrioritiesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllActionPlanPrioritiesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

