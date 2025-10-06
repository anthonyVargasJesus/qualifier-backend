using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlanStatus.Queries.GetActionPlanStatussByCompanyId
{
    public class GetActionPlanStatussByCompanyIdQuery : IGetActionPlanStatussByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActionPlanStatussByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from actionPlanStatus in _databaseService.ActionPlanStatus
                                      where ((actionPlanStatus.isDeleted == null || actionPlanStatus.isDeleted == false) && actionPlanStatus.companyId == companyId)
                                      && (actionPlanStatus.name.ToUpper().Contains(search.ToUpper()))
                                      select new ActionPlanStatusEntity
                                      {
                                          actionPlanStatusId = actionPlanStatus.actionPlanStatusId,
                                          name = actionPlanStatus.name,
                                          description = actionPlanStatus.description,
                                          abbreviation = actionPlanStatus.abbreviation,
                                          value = actionPlanStatus.value,
                                          color = actionPlanStatus.color,
                                          companyId = actionPlanStatus.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetActionPlanStatussByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetActionPlanStatussByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetActionPlanStatussByCompanyIdDto>>(entities);
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
            var total = await (from actionPlanStatus in _databaseService.ActionPlanStatus
                               where ((actionPlanStatus.isDeleted == null || actionPlanStatus.isDeleted == false) && actionPlanStatus.companyId == companyId)
                               && (actionPlanStatus.name.ToUpper().Contains(search.ToUpper()))
                               select new ActionPlanStatusEntity
                               {
                                   actionPlanStatusId = actionPlanStatus.actionPlanStatusId,
                               }).CountAsync();
            return total;
        }

    }
}

