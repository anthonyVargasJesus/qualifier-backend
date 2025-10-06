using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlanPriority.Queries.GetActionPlanPrioritiesByCompanyId
{
    public class GetActionPlanPrioritiesByCompanyIdQuery : IGetActionPlanPrioritiesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActionPlanPrioritiesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from actionPlanPriority in _databaseService.ActionPlanPriority
                                      where ((actionPlanPriority.isDeleted == null || actionPlanPriority.isDeleted == false) && actionPlanPriority.companyId == companyId)
                                      && (actionPlanPriority.name.ToUpper().Contains(search.ToUpper()))
                                      select new ActionPlanPriorityEntity
                                      {
                                          actionPlanPriorityId = actionPlanPriority.actionPlanPriorityId,
                                          name = actionPlanPriority.name,
                                          description = actionPlanPriority.description,
                                          abbreviation = actionPlanPriority.abbreviation,
                                          value = actionPlanPriority.value,
                                          color = actionPlanPriority.color,
                                          companyId = actionPlanPriority.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetActionPlanPrioritiesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetActionPlanPrioritiesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetActionPlanPrioritiesByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from actionPlanPriority in _databaseService.ActionPlanPriority
                               where ((actionPlanPriority.isDeleted == null || actionPlanPriority.isDeleted == false) && actionPlanPriority.companyId == companyId)
                               && (actionPlanPriority.name.ToUpper().Contains(search.ToUpper()))
                               select new ActionPlanPriorityEntity
                               {
                                   actionPlanPriorityId = actionPlanPriority.actionPlanPriorityId,
                               }).CountAsync();
            return total;
        }

    }
}

