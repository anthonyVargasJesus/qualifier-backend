using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId
{
    internal class GetAllActionPlanStatussByCompanyIdQuery : IGetAllActionPlanStatussByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllActionPlanStatussByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from actionPlanStatus in _databaseService.ActionPlanStatus
                                      where ((actionPlanStatus.isDeleted == null || actionPlanStatus.isDeleted == false) && actionPlanStatus.companyId == companyId)
                                      select new ActionPlanStatusEntity
                                      {
                                          actionPlanStatusId = actionPlanStatus.actionPlanStatusId,
                                          name = actionPlanStatus.name,
                                          description = actionPlanStatus.description,
                                          abbreviation = actionPlanStatus.abbreviation,
                                          value = actionPlanStatus.value,
                                          color = actionPlanStatus.color,
                                          companyId = actionPlanStatus.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllActionPlanStatussByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllActionPlanStatussByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllActionPlanStatussByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

