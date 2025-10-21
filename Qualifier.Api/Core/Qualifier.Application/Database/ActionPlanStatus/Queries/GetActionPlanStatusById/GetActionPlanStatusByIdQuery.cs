using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlanStatus.Queries.GetActionPlanStatusById
{
    public class GetActionPlanStatusByIdQuery : IGetActionPlanStatusByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetActionPlanStatusByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int actionPlanStatusId)
        {
            try
            {
                var entity = await (from item in _databaseService.ActionPlanStatus
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.actionPlanStatusId == actionPlanStatusId)
                                    select new ActionPlanStatusEntity()
                                    {
                                        actionPlanStatusId = item.actionPlanStatusId,
                                        name = item.name,
                                        description = item.description,
                                        abbreviation = item.abbreviation,
                                        value = item.value,
                                        color = item.color,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetActionPlanStatusByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

